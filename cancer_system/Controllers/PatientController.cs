using cancer_system.Data;
using cancer_system.DTOs.CalendarDTO;
using cancer_system.DTOs.ChatDTO;
using cancer_system.DTOs.PatientDTO;
using cancer_system.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace cancer_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Patient")]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

      
        [HttpGet("doctors")]
        public IActionResult GetDoctors()
        {
            var doctors = _context.Doctors
                .Join(_context.Users,
                    d => d.AspNetUserId,
                    u => u.Id,
                    (d, u) => new
                    {
                        d.DoctorId,
                        DoctorUserId = u.Id,
                        Name = u.FullName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        d.Specialization
                    })
                .ToList();

            return Ok(doctors);
        }


        [HttpPost("chat/send")]
        public async Task<IActionResult> SendMessage(SendMessageDto dto)
        {
            var senderId = GetUserId();


            var receiverExists = _context.Users.Any(u => u.Id == dto.ReceiverId);
            if (!receiverExists)
                return BadRequest("Receiver user does not exist");

            var chat = new Chat
            {
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                MessageText = dto.MessageText,
                SentAt = DateTime.Now
            };

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return Ok("Message sent");
        }


        [HttpGet("chat/{otherUserId}")]
        public IActionResult GetChat(string otherUserId)
        {
            var myId = GetUserId();

            var messages = _context.Chats
                .Where(c =>
                    (c.SenderId == myId && c.ReceiverId == otherUserId) ||
                    (c.SenderId == otherUserId && c.ReceiverId == myId))
                .AsNoTracking()
                .OrderBy(c => c.SentAt)
                .Select(c => new
                {
                    c.MessageText,
                    c.SentAt,
                    IsMe = c.SenderId == myId
                })
                .ToList();

            return Ok(messages);
        }

        [HttpGet("chat/list")]
        public IActionResult GetChatList()
        {
            var myId = GetUserId();


            var chats = _context.Chats
                .Where(c => c.SenderId == myId || c.ReceiverId == myId)
                .AsNoTracking()
                .Select(c => new
                {
                    c.SenderId,
                    c.ReceiverId,
                    c.MessageText,
                    c.SentAt
                })
                .ToList();


            var otherUserIds = chats
                .Select(c => c.SenderId == myId ? c.ReceiverId : c.SenderId)
                .Distinct()
                .ToList();


            var users = _context.Users
                .Where(u => otherUserIds.Contains(u.Id))
                .Select(u => new { u.Id, u.FullName, u.UserName })
                .ToDictionary(u => u.Id);


            var result = chats
                .GroupBy(c => c.SenderId == myId ? c.ReceiverId : c.SenderId)
                .Select(g =>
                {
                    var last = g.OrderByDescending(x => x.SentAt).First();
                    var otherId = last.SenderId == myId ? last.ReceiverId : last.SenderId;

                    users.TryGetValue(otherId, out var user);

                    return new ChatListDto
                    {
                        OtherUserId = otherId,
                        OtherUserName = user?.FullName ?? user?.UserName ?? "Unknown",
                        LastMessage = last.MessageText,
                        LastSentAt = last.SentAt
                    };
                })
                .OrderByDescending(x => x.LastSentAt)
                .ToList();

            return Ok(result);
        }


        [HttpGet("calendar/today")]
        public IActionResult TodaySchedule()
        {
            var userId = GetUserId();
            var patientId = _context.Patients
                .Where(p => p.AspNetUserId == userId)
                .Select(p => p.PatientId)
                .First();

            var today = DateTime.Today;
            var result = new List<TodayScheduleDto>();

           
            var appointments = _context.Appointments
                .Where(a => a.PatientId == patientId &&
                            a.AppointmentDate.Date == today)
                .Select(a => new TodayScheduleDto
                {
                    Type = "Appointment",
                    Description = a.AppointmentType,
                    Time = a.AppointmentDate
                });

            
            var chemo = _context.Chemotherapys
                .Where(c => c.PatientId == patientId &&
                            c.Date.Date == today)
                .Select(c => new TodayScheduleDto
                {
                    Type = "Chemotherapy",
                    Description = c.Drug_Name,
                    Time = c.Date
                });

            result.AddRange(appointments);
            result.AddRange(chemo);

            if (!result.Any())
                return Ok("لا يوجد مواعيد اليوم");

            return Ok(result);
        }


        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = GetUserId();

            var profile = _context.Patients
                .Where(p => p.AspNetUserId == userId)
                .Join(_context.Users,
                    p => p.AspNetUserId,
                    u => u.Id,
                    (p, u) => new PatientProfileDto
                    {
                        Name = p.FullName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        DateOfBirth = p.DateOfBirth,
                        Gender = p.Gender,
                      
                    })
                .FirstOrDefault();

            if (profile == null)
                return NotFound("Patient profile not found");

            return Ok(profile);
        }



        [Authorize(Roles = "Patient")]
        [HttpPatch("profile/phone")]
        public async Task<IActionResult> UpdatePhone([FromBody] UpdatePhoneDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound("User not found");

            user.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok("Phone number updated successfully");
        }

        [Authorize(Roles = "Patient")]
        [HttpPatch("profile/name")]
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.AspNetUserId == userId);

            if (patient == null)
                return NotFound("Patient profile not found");

            patient.FullName = dto.FullName;

            await _context.SaveChangesAsync();
            return Ok("Name updated successfully");
        }



        [Authorize(Roles = "Patient")]
        [HttpPatch("profile/gender")]
        public async Task<IActionResult> UpdateGender([FromBody] UpdateGenderDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.AspNetUserId == userId);

            if (patient == null)
                return NotFound("Patient profile not found");

            patient.Gender = dto.Gender;

            await _context.SaveChangesAsync();
            return Ok("Gender updated successfully");
        }


        [Authorize(Roles = "Patient")]
        [HttpPatch("profile/birthdate")]
        public async Task<IActionResult> UpdateDateOfBirth([FromBody] UpdateDateOfBirthDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.AspNetUserId == userId);

            if (patient == null)
                return NotFound("Patient profile not found");

            patient.DateOfBirth = dto.DateOfBirth;

            await _context.SaveChangesAsync();
            return Ok("Date of birth updated successfully");
        }




    }
}

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
                        Name = u.UserName,
                        Email = u.Email,
                        d.Specialization
                    })
                .ToList();

            return Ok(doctors);
        }

       
        [HttpPost("chat/send")]
        public async Task<IActionResult> SendMessage(SendMessageDto dto)
        {
            var chat = new Chat
            {
                SenderId = GetUserId(),
                ReceiverId = dto.ReceiverId,
                MessageText = dto.MessageText,
                SentAt = DateTime.Now
            };

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return Ok("Message sent");
        }

        [HttpGet("chat/{doctorUserId}")]
        public IActionResult GetChat(string doctorUserId)
        {
            var myId = GetUserId();

            var chat = _context.Chats
                .Where(c =>
                    (c.SenderId == myId && c.ReceiverId == doctorUserId) ||
                    (c.SenderId == doctorUserId && c.ReceiverId == myId))
                .OrderBy(c => c.SentAt)
                .ToList();

            return Ok(chat);
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
                        CancerType = p.CancerType
                    })
                .FirstOrDefault();

            if (profile == null)
                return NotFound("Patient profile not found");

            return Ok(profile);
        }



        [Authorize(Roles = "Patient")]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UpdatePatientProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

          
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found");

            user.PhoneNumber = dto.PhoneNumber;

        
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.AspNetUserId == userId);

            if (patient == null)
                return NotFound("Patient profile not found");

            patient.FullName = dto.Name;
            patient.Gender = dto.Gender;
            patient.CancerType = dto.CancerType;
            patient.DateOfBirth = dto.DateOfBirth;

            await _context.SaveChangesAsync();

            return Ok("Patient profile updated successfully");
        }


    }
}

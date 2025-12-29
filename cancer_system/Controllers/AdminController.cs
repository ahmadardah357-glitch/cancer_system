using cancer_system.Data;
using cancer_system.DTOs.AdminDTO;
using cancer_system.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cancer_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] 
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        [HttpGet("doctors")]
        public IActionResult GetAllDoctors()
        {
            var doctors = _context.Doctors
                .Join(
                    _context.Users,
                    d => d.AspNetUserId,
                    u => u.Id,
                    (d, u) => new
                    {
                        d.DoctorId,
                        Name = u.UserName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        d.Specialization
                    }
                )
                .ToList();

            return Ok(doctors);
        }

     
        [HttpGet("patients")]
        public IActionResult GetAllPatients()
        {
            var patients = _context.Patients
                .Join(
                    _context.Users,
                    p => p.AspNetUserId,
                    u => u.Id,
                    (p, u) => new
                    {
                        p.PatientId,
                        Name = u.UserName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        p.Gender,
                        p.CancerType,
                        p.DateOfBirth
                    }
                )
                .ToList();

            return Ok(patients);
        }

     
        [HttpPost("assign")]
        public async Task<IActionResult> AssignDoctorPatient(
            AssignDoctorPatientDto dto)
        {
            bool exists = await _context.DoctorPatients.AnyAsync(dp =>
                dp.DoctorId == dto.DoctorId &&
                dp.PatientId == dto.PatientId);

            if (exists)
                return BadRequest("Already assigned");

            _context.DoctorPatients.Add(new DoctorPatient
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId
            });

            await _context.SaveChangesAsync();
            return Ok("Patient assigned to doctor");
        }

       
        [HttpDelete("unassign/{doctorId}/{patientId}")]
        public async Task<IActionResult> RemoveDoctorPatient(
    int doctorId,
    int patientId)
        {
            var link = await _context.DoctorPatients.FirstOrDefaultAsync(dp =>
                dp.DoctorId == doctorId &&
                dp.PatientId == patientId);

            if (link == null)
                return NotFound("Link not found");

            _context.DoctorPatients.Remove(link);
            await _context.SaveChangesAsync();

            return Ok("Link removed");
        }

        [HttpDelete("doctor/{doctorId}")]
        public async Task<IActionResult> DeleteDoctor(int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null)
                return NotFound();

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return Ok("Doctor deleted");
        }

        [HttpDelete("patient/{patientId}")]
        public async Task<IActionResult> DeletePatient(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null)
                return NotFound();

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return Ok("Patient deleted");
        }
    }
}

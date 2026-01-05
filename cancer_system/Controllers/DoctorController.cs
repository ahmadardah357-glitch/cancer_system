using cancer_system.Data;
using cancer_system.DTOs.AppointmentDTO;
using cancer_system.DTOs.ChatDTO;
using cancer_system.DTOs.DiagnosisDTO;
using cancer_system.DTOs.DoctorDTO;
using cancer_system.DTOs.PatientDTO;
using cancer_system.DTOs.RadiationTherapyDTO;
using cancer_system.DTOs.RadiologyImageDTO;
using cancer_system.DTOs.TestDTO;
using cancer_system.DTOs.TreatmentPlanDTO;
using cancer_system.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace cancer_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        private int GetDoctorIdSafe()
        {
            return _context.Doctors
                .Where(d => d.AspNetUserId == UserId)
                .Select(d => d.DoctorId)
                .FirstOrDefault();
        }

        private bool IsMyPatient(int patientId)
        {
            var doctorId = GetDoctorIdSafe();
            if (doctorId == 0) return false;

            return _context.DoctorPatients.Any(dp =>
                dp.DoctorId == doctorId &&
                dp.PatientId == patientId);
        }


        [HttpGet("profile")]
        [Authorize(Roles = "Doctor")]
        public IActionResult GetDoctorProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = _context.Doctors
                .Where(d => d.AspNetUserId == userId)
                .Select(d => new DoctorProfileDto
                {
                    Name = d.FullName,
                    Specialization = d.Specialization,
                    YearsOfExperience = d.YearsOfExperience,
                    PatientsCount = _context.DoctorPatients
                        .Count(dp => dp.DoctorId == d.DoctorId),
                    PhoneNumber = _context.Users
                        .Where(u => u.Id == d.AspNetUserId)
                        .Select(u => u.PhoneNumber)
                        .FirstOrDefault()
                })
                .FirstOrDefault();

            return Ok(profile);
        }
        [HttpGet("doctors")]
        public IActionResult GetDoctors()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var doctors = _context.Doctors
                .Where(d => d.AspNetUserId != currentUserId) 
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


        [Authorize(Roles = "Doctor")]
        [HttpPatch("profile/phone")]
        public async Task<IActionResult> UpdatePhone([FromBody] UpdateDoctorPhoneDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found");

            user.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok("Phone number updated successfully");
        }



        [Authorize(Roles = "Doctor")]
        [HttpPatch("profile/name")]
        public async Task<IActionResult> UpdateName([FromBody] UpdateDoctorNameDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.AspNetUserId == userId);

            if (doctor == null)
                return NotFound("Doctor profile not found");

            doctor.FullName = dto.FullName;

            await _context.SaveChangesAsync();
            return Ok("Name updated successfully");
        }



        [Authorize(Roles = "Doctor")]
        [HttpPatch("profile/specialization")]
        public async Task<IActionResult> UpdateSpecialization([FromBody] UpdateDoctorSpecializationDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.AspNetUserId == userId);

            if (doctor == null)
                return NotFound("Doctor profile not found");

            doctor.Specialization = dto.Specialization;

            await _context.SaveChangesAsync();
            return Ok("Specialization updated successfully");
        }




        [Authorize(Roles = "Doctor")]
        [HttpPatch("profile/experience")]
        public async Task<IActionResult> UpdateExperience([FromBody] UpdateDoctorExperienceDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.AspNetUserId == userId);

            if (doctor == null)
                return NotFound("Doctor profile not found");

            doctor.YearsOfExperience = dto.YearsOfExperience;

            await _context.SaveChangesAsync();
            return Ok("Years of experience updated successfully");
        }




        [HttpGet("my-patients")]
        public IActionResult GetMyPatients()
        {
            var doctorId = GetDoctorIdSafe();
            if (doctorId == 0) return Unauthorized("Doctor profile not found");

            var patients = _context.DoctorPatients
                .Where(dp => dp.DoctorId == doctorId)
                .Join(_context.Patients, dp => dp.PatientId, p => p.PatientId, (dp, p) => p)
                .Include(p => p.User)
                .AsNoTracking()
                .Select(p => new
                {
                    p.PatientId,
                    Name = p.User != null ? p.User.FullName : null,
                    Email = p.User != null ? p.User.Email : null,
                    p.Gender,
                    p.CancerType,
                    Age = DateTime.Now.Year - p.DateOfBirth.Year
                })
                .ToList();

            return Ok(patients);
        }

        [HttpGet("patients/{patientId}/info")]
        public IActionResult GetPatientInfo(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var patient = _context.Patients
                .Include(p => p.User)
                .AsNoTracking()
                .FirstOrDefault(p => p.PatientId == patientId);

            if (patient == null) return NotFound();

            return Ok(new
            {
                patient.PatientId,
                Name = patient.FullName,
                Email = patient.User?.Email,
                patient.Gender,
                patient.CancerType,
                Age = DateTime.Now.Year - patient.DateOfBirth.Year
            });
        }

        [HttpGet("patients/{patientId}/diagnoses")]
        public IActionResult GetDiagnoses(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var list = _context.Diagnoses
                .Where(d => d.PatientId == patientId)
                .AsNoTracking()
                .OrderByDescending(d => d.DiagnosisDate)
                .Select(d => new DiagnosisDto
                {
                    DiagnosisId = d.DiagnosisId,
                    DiagnosisDate = d.DiagnosisDate,
                    Stage = d.Stage,
                    TumorGrade = d.TumorGrade,
                    TumorLocation = d.TumorLocation,
                    Notes = d.Notes
                })
                .ToList();

            return Ok(list);
        }

        [HttpGet("diagnoses/{id}")]
        public IActionResult GetDiagnosisById(int id)
        {
            var d = _context.Diagnoses.AsNoTracking().FirstOrDefault(x => x.DiagnosisId == id);
            if (d == null) return NotFound();
            if (!IsMyPatient(d.PatientId)) return Forbid();

            return Ok(new DiagnosisDto
            {
                DiagnosisId = d.DiagnosisId,
                DiagnosisDate = d.DiagnosisDate,
                Stage = d.Stage,
                TumorGrade = d.TumorGrade,
                TumorLocation = d.TumorLocation,
                Notes = d.Notes
            });
        }

        [HttpPost("patients/{patientId}/diagnoses")]
        public async Task<IActionResult> CreateDiagnosis(int patientId, CreateDiagnosisDto dto)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var entity = new Diagnosis
            {
                PatientId = patientId,
                DiagnosisDate = dto.DiagnosisDate,
                Stage = dto.Stage,
                TumorGrade = dto.TumorGrade,
                TumorLocation = dto.TumorLocation,
                Notes = dto.Notes
            };

            _context.Diagnoses.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new { entity.DiagnosisId });
        }

        [HttpPut("diagnoses/{id}")]
        public async Task<IActionResult> UpdateDiagnosis(int id, UpdateDiagnosisDto dto)
        {
            var entity = await _context.Diagnoses.FindAsync(id);
            if (entity == null) return NotFound();
            if (!IsMyPatient(entity.PatientId)) return Forbid();

            entity.DiagnosisDate = dto.DiagnosisDate;
            entity.Stage = dto.Stage;
            entity.TumorGrade = dto.TumorGrade;
            entity.TumorLocation = dto.TumorLocation;
            entity.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return Ok();
        }

        
        [HttpGet("patients/{patientId}/cbc")]
        public IActionResult GetCbc(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var list = _context.CBCs
                .Where(x => x.PatientId == patientId)
                .AsNoTracking()
                .OrderByDescending(x => x.CbcId)
                .Select(x => new CbcDto
                {
                    CbcId = x.CbcId,
                    Hemoglobin = x.Hemoglobin,
                    Platelets = x.Platelets,
                    WBC = x.WBC
                })
                .ToList();

            return Ok(list);
        }

        [HttpGet("cbc/{id}")]
        public IActionResult GetCbcById(int id)
        {
            var x = _context.CBCs.AsNoTracking().FirstOrDefault(c => c.CbcId == id);
            if (x == null) return NotFound();
            if (!IsMyPatient(x.PatientId)) return Forbid();

            return Ok(new CbcDto
            {
                CbcId = x.CbcId,
                Hemoglobin = x.Hemoglobin,
                Platelets = x.Platelets,
                WBC = x.WBC
            });
        }

        [HttpPost("patients/{patientId}/cbc")]
        public async Task<IActionResult> CreateCbc(int patientId, CreateCbcDto dto)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            _context.CBCs.Add(new CBC
            {
                PatientId = patientId,
                Hemoglobin = dto.Hemoglobin,
                Platelets = dto.Platelets,
                WBC = dto.WBC
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("cbc/{id}")]
        public async Task<IActionResult> UpdateCbc(int id, UpdateCbcDto dto)
        {
            var entity = await _context.CBCs.FindAsync(id);
            if (entity == null) return NotFound();
            if (!IsMyPatient(entity.PatientId)) return Forbid();

            entity.Hemoglobin = dto.Hemoglobin;
            entity.Platelets = dto.Platelets;
            entity.WBC = dto.WBC;

            await _context.SaveChangesAsync();
            return Ok();
        }

       
        [HttpGet("patients/{patientId}/chemotherapy")]
        public IActionResult GetChemotherapy(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var list = _context.Chemotherapys
                .Where(x => x.PatientId == patientId)
                .AsNoTracking()
                .OrderByDescending(x => x.ChemotherapyId)
                .Select(x => new ChemotherapyDto
                {
                    ChemotherapyId = x.ChemotherapyId,
                    Drug_Name = x.Drug_Name,
                    Dose = x.Dose,
                    Date = x.Date
                })
                .ToList();

            return Ok(list);
        }

        [HttpGet("chemotherapy/{id}")]
        public IActionResult GetChemotherapyById(int id)
        {
            var x = _context.Chemotherapys.AsNoTracking().FirstOrDefault(c => c.ChemotherapyId == id);
            if (x == null) return NotFound();
            if (!IsMyPatient(x.PatientId)) return Forbid();

            return Ok(new ChemotherapyDto
            {
                ChemotherapyId = x.ChemotherapyId,
                Drug_Name = x.Drug_Name,
                Dose = x.Dose,
                Date = x.Date
            });
        }

        [HttpPost("patients/{patientId}/chemotherapy")]
        public async Task<IActionResult> CreateChemotherapy(int patientId, CreateChemotherapyDto dto)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            _context.Chemotherapys.Add(new Chemotherapy
            {
                PatientId = patientId,
                Drug_Name = dto.Drug_Name,
                Dose = dto.Dose,
                Date = dto.Date
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("chemotherapy/{id}")]
        public async Task<IActionResult> UpdateChemotherapy(int id, UpdateChemotherapyDto dto)
        {
            var entity = await _context.Chemotherapys.FindAsync(id);
            if (entity == null) return NotFound();
            if (!IsMyPatient(entity.PatientId)) return Forbid();

            entity.Drug_Name = dto.Drug_Name;
            entity.Dose = dto.Dose;
            entity.Date = dto.Date;

            await _context.SaveChangesAsync();
            return Ok();
        }

        
        [HttpGet("patients/{patientId}/kidney")]
        public IActionResult GetKidney(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var list = _context.kidney_Functions
                .Where(x => x.PatientId == patientId)
                .AsNoTracking()
                .OrderByDescending(x => x.KidneyFunctionId)
                .Select(x => new KidneyFunctionDto
                {
                    KidneyFunctionId = x.KidneyFunctionId,
                    Creatinine = x.Creatinine,
                    BUN = x.BUN
                })
                .ToList();

            return Ok(list);
        }

        [HttpGet("kidney/{id}")]
        public IActionResult GetKidneyById(int id)
        {
            var x = _context.kidney_Functions.AsNoTracking().FirstOrDefault(k => k.KidneyFunctionId == id);
            if (x == null) return NotFound();
            if (!IsMyPatient(x.PatientId)) return Forbid();

            return Ok(new KidneyFunctionDto
            {
                KidneyFunctionId = x.KidneyFunctionId,
                Creatinine = x.Creatinine,
                BUN = x.BUN
            });
        }

        [HttpPost("patients/{patientId}/kidney")]
        public async Task<IActionResult> CreateKidney(int patientId, CreateKidneyFunctionDto dto)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            _context.kidney_Functions.Add(new kidney_Function
            {
                PatientId = patientId,
                Creatinine = dto.Creatinine,
                BUN = dto.BUN
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("kidney/{id}")]
        public async Task<IActionResult> UpdateKidney(int id, UpdateKidneyFunctionDto dto)
        {
            var entity = await _context.kidney_Functions.FindAsync(id);
            if (entity == null) return NotFound();
            if (!IsMyPatient(entity.PatientId)) return Forbid();

            entity.Creatinine = dto.Creatinine;
            entity.BUN = dto.BUN;

            await _context.SaveChangesAsync();
            return Ok();
        }

      
        [HttpGet("patients/{patientId}/liver")]
        public IActionResult GetLiver(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var list = _context.Liver_Functions
                .Where(x => x.PatientId == patientId)
                .AsNoTracking()
                .OrderByDescending(x => x.LiverFunctionId)
                .Select(x => new LiverFunctionDto
                {
                    LiverFunctionId = x.LiverFunctionId,
                    ALT = x.ALT,
                    AST = x.AST,
                    AlP = x.AlP,
                    Bilirubin = x.Bilirubin
                })
                .ToList();

            return Ok(list);
        }

        [HttpGet("liver/{id}")]
        public IActionResult GetLiverById(int id)
        {
            var x = _context.Liver_Functions.AsNoTracking().FirstOrDefault(l => l.LiverFunctionId == id);
            if (x == null) return NotFound();
            if (!IsMyPatient(x.PatientId)) return Forbid();

            return Ok(new LiverFunctionDto
            {
                LiverFunctionId = x.LiverFunctionId,
                ALT = x.ALT,
                AST = x.AST,
                AlP = x.AlP,
                Bilirubin = x.Bilirubin
            });
        }

        [HttpPost("patients/{patientId}/liver")]
        public async Task<IActionResult> CreateLiver(int patientId, CreateLiverFunctionDto dto)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            _context.Liver_Functions.Add(new Liver_Function
            {
                PatientId = patientId,
                ALT = dto.ALT,
                AST = dto.AST,
                AlP = dto.AlP,
                Bilirubin = dto.Bilirubin
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("liver/{id}")]
        public async Task<IActionResult> UpdateLiver(int id, UpdateLiverFunctionDto dto)
        {
            var entity = await _context.Liver_Functions.FindAsync(id);
            if (entity == null) return NotFound();
            if (!IsMyPatient(entity.PatientId)) return Forbid();

            entity.ALT = dto.ALT;
            entity.AST = dto.AST;
            entity.AlP = dto.AlP;
            entity.Bilirubin = dto.Bilirubin;

            await _context.SaveChangesAsync();
            return Ok();
        }

      
        [HttpGet("patients/{patientId}/radiation")]
        public IActionResult GetRadiation(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var list = _context.Radiation_Therapys
                .Where(x => x.PatientId == patientId)
                .AsNoTracking()
                .OrderByDescending(x => x.RadiationTherapyId)
                .Select(x => new RadiationTherapyDto
                {
                    RadiationTherapyId = x.RadiationTherapyId,
                    Sessions_Count = x.Sessions_Count,
                    Targeted_Area = x.Targeted_Area
                })
                .ToList();

            return Ok(list);
        }

        [HttpGet("radiation/{id}")]
        public IActionResult GetRadiationById(int id)
        {
            var x = _context.Radiation_Therapys.AsNoTracking().FirstOrDefault(r => r.RadiationTherapyId == id);
            if (x == null) return NotFound();
            if (!IsMyPatient(x.PatientId)) return Forbid();

            return Ok(new RadiationTherapyDto
            {
                RadiationTherapyId = x.RadiationTherapyId,
                Sessions_Count = x.Sessions_Count,
                Targeted_Area = x.Targeted_Area
            });
        }

        [HttpPost("patients/{patientId}/radiation")]
        public async Task<IActionResult> CreateRadiation(int patientId, CreateRadiationTherapyDto dto)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            _context.Radiation_Therapys.Add(new Radiation_Therapy
            {
                PatientId = patientId,
                Sessions_Count = dto.Sessions_Count,
                Targeted_Area = dto.Targeted_Area
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("radiation/{id}")]
        public async Task<IActionResult> UpdateRadiation(int id, UpdateRadiationTherapyDto dto)
        {
            var entity = await _context.Radiation_Therapys.FindAsync(id);
            if (entity == null) return NotFound();
            if (!IsMyPatient(entity.PatientId)) return Forbid();

            entity.Sessions_Count = dto.Sessions_Count;
            entity.Targeted_Area = dto.Targeted_Area;

            await _context.SaveChangesAsync();
            return Ok();
        }

       
        [HttpGet("patients/{patientId}/surgery")]
        public IActionResult GetSurgery(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var list = _context.surgerys
                .Where(x => x.PatientId == patientId)
                .AsNoTracking()
                .OrderByDescending(x => x.SurgeryId)
                .Select(x => new SurgeryDto
                {
                    SurgeryId = x.SurgeryId,
                    Operation_Type = x.Operation_Type,
                    Surgery_Report = x.Surgery_Report
                })
                .ToList();

            return Ok(list);
        }

        [HttpGet("surgery/{id}")]
        public IActionResult GetSurgeryById(int id)
        {
            var x = _context.surgerys.AsNoTracking().FirstOrDefault(s => s.SurgeryId == id);
            if (x == null) return NotFound();
            if (!IsMyPatient(x.PatientId)) return Forbid();

            return Ok(new SurgeryDto
            {
                SurgeryId = x.SurgeryId,
                Operation_Type = x.Operation_Type,
                Surgery_Report = x.Surgery_Report
            });
        }

        [HttpPost("patients/{patientId}/surgery")]
        public async Task<IActionResult> CreateSurgery(int patientId, CreateSurgeryDto dto)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            _context.surgerys.Add(new surgery
            {
                PatientId = patientId,
                Operation_Type = dto.Operation_Type,
                Surgery_Report = dto.Surgery_Report
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("surgery/{id}")]
        public async Task<IActionResult> UpdateSurgery(int id, UpdateSurgeryDto dto)
        {
            var entity = await _context.surgerys.FindAsync(id);
            if (entity == null) return NotFound();
            if (!IsMyPatient(entity.PatientId)) return Forbid();

            entity.Operation_Type = dto.Operation_Type;
            entity.Surgery_Report = dto.Surgery_Report;

            await _context.SaveChangesAsync();
            return Ok();
        }

        
        [HttpGet("patients/{patientId}/treatment-plans")]
        public IActionResult GetTreatmentPlans(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var list = _context.TreatmentPlans
                .Include(tp => tp.Diagnosis)
                .Where(tp => tp.Diagnosis != null && tp.Diagnosis.PatientId == patientId)
                .AsNoTracking()
                .OrderByDescending(tp => tp.TreatmentPlanId)
                .Select(tp => new TreatmentPlanDto
                {
                    TreatmentPlanId = tp.TreatmentPlanId,
                    TreatmentType = tp.TreatmentType,
                    StartDate = tp.StartDate,
                    Duration = tp.Duration,
                    Status = tp.Status
                })
                .ToList();

            return Ok(list);
        }

        [HttpGet("treatment-plans/{id}")]
        public IActionResult GetTreatmentPlanById(int id)
        {
            var tp = _context.TreatmentPlans
                .Include(x => x.Diagnosis)
                .AsNoTracking()
                .FirstOrDefault(x => x.TreatmentPlanId == id);

            if (tp == null) return NotFound();
            if (tp.Diagnosis == null) return BadRequest("TreatmentPlan has no Diagnosis");
            if (!IsMyPatient(tp.Diagnosis.PatientId)) return Forbid();

            return Ok(new TreatmentPlanDto
            {
                TreatmentPlanId = tp.TreatmentPlanId,
                TreatmentType = tp.TreatmentType,
                StartDate = tp.StartDate,
                Duration = tp.Duration,
                Status = tp.Status
            });
        }

        [HttpPost("patients/{patientId}/treatment-plans")]
        public async Task<IActionResult> CreateTreatmentPlan(int patientId, CreateTreatmentPlanDto dto)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            
            var diagnosis = await _context.Diagnoses.AsNoTracking()
                .FirstOrDefaultAsync(d => d.DiagnosisId == dto.DiagnosisId);

            if (diagnosis == null) return NotFound("Diagnosis not found");
            if (diagnosis.PatientId != patientId) return BadRequest("Diagnosis does not belong to this patient");

            _context.TreatmentPlans.Add(new TreatmentPlan
            {
                DiagnosisId = dto.DiagnosisId,
                TreatmentType = dto.TreatmentType,
                StartDate = dto.StartDate,
                Duration = dto.Duration,
                Status = dto.Status
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("treatment-plans/{id}")]
        public async Task<IActionResult> UpdateTreatmentPlan(int id, UpdateTreatmentPlanDto dto)
        {
            var tp = await _context.TreatmentPlans
                .Include(x => x.Diagnosis)
                .FirstOrDefaultAsync(x => x.TreatmentPlanId == id);

            if (tp == null) return NotFound();
            if (tp.Diagnosis == null) return BadRequest("TreatmentPlan has no Diagnosis");
            if (!IsMyPatient(tp.Diagnosis.PatientId)) return Forbid();

            tp.TreatmentType = dto.TreatmentType;
            tp.Duration = dto.Duration;
            tp.Status = dto.Status;

            await _context.SaveChangesAsync();
            return Ok();
        }

       
        [HttpGet("patients/{patientId}/radiology")]
        public IActionResult GetRadiology(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var list = _context.RadiologyImages
                .Where(x => x.PatientId == patientId)
                .AsNoTracking()
                .OrderByDescending(x => x.RadiologyImageId)
                .Select(x => new RadiologyImageDto
                {
                    RadiologyImageId = x.RadiologyImageId,
                    ImagePath = x.ImagePath,
                    CreatedAt = x.CreatedAt
                })
                .ToList();

            return Ok(list);
        }

        [HttpGet("radiology/{id}")]
        public IActionResult GetRadiologyById(int id)
        {
            var x = _context.RadiologyImages.AsNoTracking().FirstOrDefault(r => r.RadiologyImageId == id);
            if (x == null) return NotFound();
            if (!IsMyPatient(x.PatientId)) return Forbid();

            return Ok(new RadiologyImageDto
            {
                RadiologyImageId = x.RadiologyImageId,
                ImagePath = x.ImagePath,
                CreatedAt = x.CreatedAt
            });
        }

        [HttpPost("patients/{patientId}/radiology/upload")]
        public async Task<IActionResult> UploadRadiologyImage(int patientId, [FromForm] CreateRadiologyImageDto dto)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            if (dto.Image == null || dto.Image.Length == 0)
                return BadRequest("No image uploaded");

            var ext = Path.GetExtension(dto.Image.FileName).ToLower();
            var allowed = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowed.Contains(ext))
                return BadRequest("Invalid file type");

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "radiology");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            var entity = new Radiology_Image
            {
                PatientId = patientId,
                ImagePath = $"/uploads/radiology/{fileName}",
                CreatedAt = DateTime.Now
            };

            _context.RadiologyImages.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new { entity.RadiologyImageId, entity.ImagePath });
        }

        [HttpPut("radiology/{id}/replace")]
        public async Task<IActionResult> ReplaceRadiologyImage(int id, [FromForm] CreateRadiologyImageDto dto)
        {
            var entity = await _context.RadiologyImages.FindAsync(id);
            if (entity == null) return NotFound();
            if (!IsMyPatient(entity.PatientId)) return Forbid();

            if (dto.Image == null || dto.Image.Length == 0)
                return BadRequest("No image uploaded");

            var ext = Path.GetExtension(dto.Image.FileName).ToLower();
            var allowed = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowed.Contains(ext))
                return BadRequest("Invalid file type");

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "radiology");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            
            try
            {
                if (!string.IsNullOrEmpty(entity.ImagePath))
                {
                    var oldPath = entity.ImagePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
                    var fullOld = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldPath);
                    if (System.IO.File.Exists(fullOld))
                        System.IO.File.Delete(fullOld);
                }
            }
            catch { }

            entity.ImagePath = $"/uploads/radiology/{fileName}";
            entity.CreatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { entity.RadiologyImageId, entity.ImagePath });
        }

       
        [HttpGet("patients/{patientId}/appointments")]
        public IActionResult GetAppointments(int patientId)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var list = _context.Appointments
                .Where(a => a.PatientId == patientId)
                .AsNoTracking()
                .OrderByDescending(a => a.AppointmentDate)
                .Select(a => new AppointmentDto
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentType = a.AppointmentType,
                    Status = a.Status,
                    Notes = a.Notes,
                    Recommendations = a.Recommendations
                })
                .ToList();

            return Ok(list);
        }

        [HttpGet("appointments/{id}")]
        public IActionResult GetAppointmentById(int id)
        {
            var a = _context.Appointments.AsNoTracking().FirstOrDefault(x => x.AppointmentId == id);
            if (a == null) return NotFound();
            if (!IsMyPatient(a.PatientId)) return Forbid();

            return Ok(new AppointmentDto
            {
                AppointmentId = a.AppointmentId,
                AppointmentDate = a.AppointmentDate,
                AppointmentType = a.AppointmentType,
                Status = a.Status,
                Notes = a.Notes,
                Recommendations = a.Recommendations
            });
        }

        [HttpPost("patients/{patientId}/appointments")]
        public async Task<IActionResult> CreateAppointment(int patientId, CreateAppointmentDto dto)
        {
            if (!IsMyPatient(patientId)) return Forbid();

            var doctorId = GetDoctorIdSafe();
            if (doctorId == 0) return Unauthorized("Doctor profile not found");

            var entity = new Appointment
            {
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentType = dto.AppointmentType,
                Notes = dto.Notes,
                Status = "Scheduled"
            };

            _context.Appointments.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new { entity.AppointmentId });
        }

        [HttpPut("appointments/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, UpdateAppointmentDto dto)
        {
            var entity = await _context.Appointments.FindAsync(id);
            if (entity == null) return NotFound();
            if (!IsMyPatient(entity.PatientId)) return Forbid();

            entity.AppointmentDate = dto.AppointmentDate;
            entity.Status = dto.Status;
            entity.Notes = dto.Notes;
            entity.Recommendations = dto.Recommendations;

            await _context.SaveChangesAsync();
            return Ok();
        }

       
        [HttpPost("chat/send")]
        public async Task<IActionResult> SendMessage(SendMessageDto dto)
        {
            
            var chat = new Chat
            {
                SenderId = UserId,
                ReceiverId = dto.ReceiverId,
                MessageText = dto.MessageText,
                SentAt = DateTime.Now
            };

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("chat/{otherUserId}")]
        public IActionResult GetChat(string otherUserId)
        {
            var myId = UserId;

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
    }

}


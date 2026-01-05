using cancer_system.Models.Entities;
using cancer_system.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cancer_system.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Diagnosis> Diagnosiss { get; set; }
        public DbSet<TreatmentPlan> TreatmentPlans { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Attends> Attendss { get; set; }
        public DbSet<ChatbotSession> ChatbotSessions { get; set; }
        public DbSet<Cancer_information> Cancer_informations { get; set; }
        public DbSet<Chemotherapy> Chemotherapys { get; set; }
        public DbSet<Hormonal> Hormonals { get; set; }
        public DbSet<kidney_Function> kidney_Functions { get; set; }
        public DbSet<Liver_Function> Liver_Functions { get; set; }
        public DbSet<Radiation_Therapy> Radiation_Therapys { get; set; }
        public DbSet<surgery> surgerys { get; set; }
        public DbSet<CBC> CBCs { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Radiology_Image> RadiologyImages { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Attends>()
                .HasKey(a => new { a.AppointmentId, a.DoctorId });

            modelBuilder.Entity<Attends>()
                .HasOne(a => a.Appointment)
                .WithMany()
                .HasForeignKey(a => a.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attends>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
    .HasOne(p => p.User)
    .WithOne(u => u.Patient)
    .HasForeignKey<Patient>(p => p.AspNetUserId);
        }

    }
}

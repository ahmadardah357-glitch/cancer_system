using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cancer_system.models
{
    public class ApplicationDbContext : IdentityDbContext
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
        public DbSet<cancer_information> cancer_informations { get; set; }
        public DbSet<Chemotherapy> Chemotherapys { get; set; }
        public DbSet<Hormonal> Hormonals { get; set; }
        public DbSet<kidney_Function> kidney_Functions { get; set; }
        public DbSet<Liver_Function> Liver_Functions { get; set; }
        public DbSet<Radiation_Therapy> Radiation_Therapys { get; set; }
        public DbSet<surgery> surgerys { get; set; }

    }
}

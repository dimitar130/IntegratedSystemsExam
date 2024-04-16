using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.Identity_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntegratedSystems.Repository
{
    public class ApplicationDbContext : IdentityDbContext<IntegratedSystemsUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Vaccine> Vaccines { get; set; }
        public virtual DbSet<VaccinationCenter> VaccinationCenters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Patient>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<VaccinationCenter>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Vaccine>()
                .HasKey(z => new { z.PatientId, z.VaccinationCenter });

            builder.Entity<Vaccine>()
                .HasOne(z => z.PatientFor)
                .WithMany(z => z.VaccinationSchedule)
                .HasForeignKey(z => z.VaccinationCenter);

            builder.Entity<Vaccine>()
                .HasOne(z => z.Center)
                .WithMany(z => z.Vaccines)
                .HasForeignKey(z => z.PatientId);


        }
    }
}

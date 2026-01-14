using Microsoft.EntityFrameworkCore;
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindomHospital.Infrastructure.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder) 
        {
            builder.HasKey(p => p.PatientId);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.BirthDate)
                .IsRequired();

            builder.HasIndex(p => new { p.LastName, p.FirstName, p.BirthDate });

            builder.HasMany(p => p.Consultations)
                .WithOne(c => c.Patient)
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Ordonnances)
                .WithOne(o => o.Patient)
                .HasForeignKey(o => o.PatientId);
        }
    }
}

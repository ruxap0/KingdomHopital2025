using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindomHospital.Infrastructure.Configurations
{
    public class ConsultationConfiguration : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            builder.HasKey(c => c.ConsultationId);

            builder.Property(c => c.DoctorId)
                .IsRequired();

            builder.Property(c => c.PatientId)
                .IsRequired();

            builder.Property(c => c.Date)
                .IsRequired();

            builder.Property(c => c.Hour)
                .IsRequired();

            builder.Property(c => c.Reason)
                .HasMaxLength(100);

            builder.HasIndex(c => new { c.DoctorId, c.Date, c.Hour });

            builder.HasOne(c => c.Doctor)
                .WithMany(d => d.Consultations)
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Patient)
                .WithMany(p => p.Consultations)
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Ordonnances)
                .WithOne(o => o.Consultation)
                .HasForeignKey(o => o.ConsultationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

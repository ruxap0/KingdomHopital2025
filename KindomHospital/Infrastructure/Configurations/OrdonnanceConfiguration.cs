using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KindomHospital.Domain.Entities;

namespace KindomHospital.Infrastructure.Data.Configurations
{
    public class OrdonnanceConfiguration : IEntityTypeConfiguration<Ordonnance>
    {
        public void Configure(EntityTypeBuilder<Ordonnance> builder)
        {
            builder.HasKey(o => o.OrdonnanceId);

            builder.Property(o => o.DoctorId)
                .IsRequired();

            builder.Property(o => o.PatientId)
                .IsRequired();

            builder.Property(o => o.Date)
                .IsRequired();

            builder.Property(o => o.Notes)
                .HasMaxLength(255);

            builder.HasOne(o => o.Doctor)
                .WithMany(d => d.Ordonnances)
                .HasForeignKey(o => o.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Patient)
                .WithMany(p => p.Ordonnances)
                .HasForeignKey(o => o.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Consultation)
                .WithMany(c => c.Ordonnances)
                .HasForeignKey(o => o.ConsultationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(o => o.OrdonnanceLignes)
                .WithOne(ol => ol.Ordonnance)
                .HasForeignKey(ol => ol.OrdonnanceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
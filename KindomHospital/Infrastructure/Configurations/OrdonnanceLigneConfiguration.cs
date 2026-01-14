using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KindomHospital.Domain.Entities;

namespace KindomHospital.Infrastructure.Data.Configurations
{
    public class OrdonnanceLigneConfiguration : IEntityTypeConfiguration<OrdonnanceLigne>
    {
        public void Configure(EntityTypeBuilder<OrdonnanceLigne> builder)
        {
            builder.HasKey(ol => ol.OrdonnanceLigneId);

            builder.Property(ol => ol.OrdonnanceId)
                .IsRequired();

            builder.Property(ol => ol.MedicamentId)
                .IsRequired();

            builder.Property(ol => ol.Dosage)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ol => ol.Frequency)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ol => ol.Duration)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(ol => ol.Quantity)
                .IsRequired();

            builder.Property(ol => ol.Instructions)
                .HasMaxLength(255);

            builder.HasOne(ol => ol.Ordonnance)
                .WithMany(o => o.OrdonnanceLignes)
                .HasForeignKey(ol => ol.OrdonnanceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ol => ol.Medicament)
                .WithMany(m => m.OrdonnanceLignes)
                .HasForeignKey(ol => ol.MedicamentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KindomHospital.Infrastructure.Configurations
{
    public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(m => m.MedicamentId);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.DosageForm)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(m => m.Strength)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(m => m.AtcCode)
                .HasMaxLength(20);

            builder.HasIndex(m => m.Name)
                .IsUnique();

            builder.HasMany(m => m.OrdonnanceLignes)
                .WithOne(ol => ol.Medicament)
                .HasForeignKey(ol => ol.MedicamentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindomHospital.Infrastructure.Configurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.HasKey(s => s.SpecialtyId);
            
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.HasIndex(s => s.Name)
                   .IsUnique();

            builder.HasMany(s => s.Doctors)
                   .WithOne(d => d.Specialty)
                   .HasForeignKey(d => d.SpecialtyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

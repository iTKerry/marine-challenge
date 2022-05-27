using DataAccess.EF.Metadata;
using DataAccess.EF.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EF.Configurations;

public sealed class VesselConfiguration : IEntityTypeConfiguration<VesselDbo>
{
    public void Configure(EntityTypeBuilder<VesselDbo> builder)
    {
        builder
            .ToTable(Tables.Vessel, Schemas.Dbo)
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasConversion(imo => imo.Value, id => new IMO(id))
            .HasColumnName("IMO")
            .ValueGeneratedNever();

        builder
            .Property(p => p.Name)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder
            .HasMany(p => p.Traces)
            .WithOne(p => p.Vessel)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(p => p.Created)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GetDate()");
        
        builder
            .Property(p => p.Modified)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("GetDate()");
    }
}
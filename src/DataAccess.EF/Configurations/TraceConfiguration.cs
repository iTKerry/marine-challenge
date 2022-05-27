using DataAccess.EF.Metadata;
using DataAccess.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EF.Configurations;

public sealed class TraceConfiguration : IEntityTypeConfiguration<TraceDbo>
{
    public void Configure(EntityTypeBuilder<TraceDbo> builder)
    {
        builder
            .ToTable(Tables.Trace, Schemas.Dbo)
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasColumnName("TraceId");

        builder
            .HasOne(p => p.Vessel)
            .WithMany(p => p.Traces)
            .HasForeignKey("IMO");
        
        builder
            .Property(p => p.TimeStamp)
            .IsRequired();

        builder
            .Property(p => p.Latitude)
            .IsRequired();
        
        builder
            .Property(p => p.Longitude)
            .IsRequired();
        
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
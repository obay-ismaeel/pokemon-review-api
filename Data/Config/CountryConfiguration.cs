using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Config;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        
        builder.HasMany(x => x.Owners)
            .WithOne(x => x.Country)
            .HasForeignKey(x => x.CountryId)
            .IsRequired();
    }
}

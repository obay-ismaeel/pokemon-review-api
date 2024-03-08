using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Config;

public class PokemonCategoryConfiguration : IEntityTypeConfiguration<PokemonCategory>
{
    public void Configure(EntityTypeBuilder<PokemonCategory> builder)
    {
        builder.HasKey(x => new { x.CategoryId, x.PokemonId });
    }
}

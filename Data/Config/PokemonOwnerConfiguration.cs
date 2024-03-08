using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Config;

public class PokemonOwnerConfiguration : IEntityTypeConfiguration<PokemonOwner>
{
    public void Configure(EntityTypeBuilder<PokemonOwner> builder)
    {
        builder.HasKey(x => new {x.OwnerId, x.PokemonId});
    }
}

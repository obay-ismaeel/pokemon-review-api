using AutoMapper;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles() 
    {
        CreateMap<Pokemon, PokemonDto>();
        CreateMap<PokemonDto, Pokemon>();
        CreateMap<CategoryDto, Category>();
        CreateMap<Category, CategoryDto>();
    }
}

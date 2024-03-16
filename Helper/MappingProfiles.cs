using AutoMapper;
using PokemonReviewApp.Controllers;
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
        
        CreateMap<CountryDto, Country>();
        CreateMap<Country, CountryDto>();

        CreateMap<OwnerDto, Owner>();
        CreateMap<Owner, OwnerDto>();

        CreateMap<ReviewDto, Review>();
        CreateMap<Review, ReviewDto>();

        CreateMap<ReviewerDto, Reviewer>();
        CreateMap<Reviewer, ReviewerDto>();

        CreateMap<UserRegisterRequest, User>();
    }
}

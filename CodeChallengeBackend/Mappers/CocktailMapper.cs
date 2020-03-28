using api.Models.Response;
using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace api.Mappers
{
    public class CocktailMapper : Profile
    {
        public CocktailMapper()
        {
            RecognizePostfixes("Drink");
            RecognizePrefixes("Str");           

            CreateMap<CocktailAllProperties, Cocktail>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StrDrink))
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.StrDrinkThumb))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => MapIngredients(src)));

            CreateMap<CocktailResponse, Cocktail>()
            .AfterMap((src, dest, context) => context.Mapper.Map(src.Drinks.First(), dest));

        }

        private List<string> MapIngredients(CocktailAllProperties source)
        {                   
            var pattern = new Regex(@"^StrIngredient\d+$");
            var ingredientsList = source.GetType().
                GetProperties()
                .Where(p => pattern.IsMatch(p.Name) && !string.IsNullOrEmpty((string)p.GetValue(source)))
                .Select(p => (string)p.GetValue(source))
                .ToList(); 

            return ingredientsList;
        }

        
    }
}

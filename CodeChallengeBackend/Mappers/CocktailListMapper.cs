using CodeChallengeBackend.Models.Response;
using AutoMapper;
using Models;
using System.Collections.Generic;
using System.Linq;


namespace CodeChallengeBackend.Mappers
{
    public class CocktailListMapper : Profile
    {
        public CocktailListMapper()
        {
            CreateMap<IEnumerable<CocktailResponse>, CocktailList>()
           .AfterMap((src, dest, context) =>
           {
               dest.Cocktails = src.Select(s => context.Mapper.Map<Cocktail>(s.Drinks.First())).ToList();
               dest.meta = new ListMeta()
               {
                   count = dest.Cocktails.Count,
                   firstId = src.First().Drinks.First().IdDrink,
                   lastId = src.Last().Drinks.First().IdDrink,
                   medianIngredientCount = (int)dest.Cocktails.Average(c => c.Ingredients.Count)
               };
           });
        }       
    }
}

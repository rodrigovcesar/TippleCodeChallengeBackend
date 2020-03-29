using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Response;
using AutoMapper;
using CocktailDBApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api")]
    [ApiController]
    public class BoozeController : ControllerBase
    {
        private readonly DBApi _dbApi = new DBApi();
        private readonly IMapper _mapper;
        public BoozeController(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        // We will use the public CocktailDB API as our backend
        // https://www.thecocktaildb.com/api.php
        //
        // Bonus points
        // - Speed improvements
        // - Unit Tests
        
        [HttpGet]
        [Route("search-ingredient/{ingredient}")]
        [ResponseCache(Duration = 120)]
        public async Task<IActionResult> GetIngredientSearch([FromRoute] string ingredient)
        {            
            var filteredResponse = await _dbApi.GetCocktailsByIngredient(ingredient);
            if(filteredResponse.Drinks.Count() > 0)
            {
                var drinkIds = from item in filteredResponse.Drinks select item.IdDrink;                

                var tasksToFetchCocktails = from id in drinkIds select _dbApi.FetchCocktailById(id);                
                var cocktails = await Task.WhenAll(tasksToFetchCocktails);

                var dto = _mapper.Map<CocktailList>(cocktails);
                return Ok(dto);
            }
            
            return Ok(new CocktailList());
        }

        [HttpGet]
        [Route("random")]
        public async Task<IActionResult> GetRandom()
        {
            var cocktail = await _dbApi.GetRandom(); ;
            var dto = _mapper.Map<Cocktail>(cocktail);
            return Ok(dto);
        }
    }
}
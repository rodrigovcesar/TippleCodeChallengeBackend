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

        [HttpGet]
        [Route("search-ingredient/{ingredient}")]
        [ResponseCache(Duration = 120)]
        public async Task<IActionResult> GetIngredientSearch([FromRoute] string ingredient)
        {
            if (string.IsNullOrEmpty(ingredient))
                return BadRequest();

            var filteredResponse = await _dbApi.GetCocktailsByIngredient(ingredient);
            var dto = new CocktailList();

            if (filteredResponse.Count() > 0)
                dto = _mapper.Map<CocktailList>(filteredResponse);

            return Ok(dto);
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
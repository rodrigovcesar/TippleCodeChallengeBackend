using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        private readonly DBApi _dbApi;
        private readonly IMapper _mapper;       
        public BoozeController(IMapper mapper, DBApi dbApi)
        {
            _mapper = mapper;
            _dbApi = dbApi;
        }

        [HttpGet]
        [Route("search-ingredient/{ingredient}")]
        [ResponseCache(Duration = 120)]
        public async Task<IActionResult> GetIngredientSearch([FromRoute] string ingredient)
        {
            if (string.IsNullOrEmpty(ingredient))
                return BadRequest();

            var filteredResponse = await _dbApi.GetCocktailsByIngredient(ingredient);
            

            if (filteredResponse.Count() > 0)
                 return Ok(_mapper.Map<CocktailList>(filteredResponse));

            return NotFound();
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
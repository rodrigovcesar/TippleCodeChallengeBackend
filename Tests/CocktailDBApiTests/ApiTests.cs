using CocktailDBApi;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CocktailDBApiTests
{
    public class ApiTests
    {
        private readonly ITestOutputHelper _output;
        private DBApi _cocktailDBApi;

        public ApiTests(ITestOutputHelper outputHelper)
        {
            _output = outputHelper;
            _cocktailDBApi = new DBApi();

        }
        [Fact]
        public async Task GetIngredientSearch()
        {
            var ingredient = "Gin";

            var cocktails = await _cocktailDBApi.GetCocktailsByIngredient(ingredient);


            Assert.Equal(95, cocktails.Drinks.Count());
            _output.WriteLine(cocktails.Drinks.First().StrDrink);
        }

        [Fact]
        public async Task FetchCocktailById()
        {
            var id = 11007;
            var nonExistingId = 0;

            var cocktail = await _cocktailDBApi.FetchCocktailById(id);
            var nullResponse = await _cocktailDBApi.FetchCocktailById(nonExistingId);


            Assert.Equal("Margarita", cocktail.Drinks.First().StrDrink);
            Assert.Null(nullResponse.Drinks);
        }


    }
}

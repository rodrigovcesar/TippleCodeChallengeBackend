using CocktailDBApi;
using Models;
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


            Assert.Equal(95, cocktails.Count());
            _output.WriteLine(cocktails.First().Drinks.First().StrDrink);
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

        [Fact]
        public async Task GetRandonCocktail()
        {
            var cocktail1 = await _cocktailDBApi.GetRandom();
            var cocktail2 = await _cocktailDBApi.GetRandom();
            var id1 = cocktail1.Drinks.First().IdDrink;
            var id2 = cocktail2.Drinks.First().IdDrink;            

            Assert.NotEqual(id1, id2);
        }


    }
}

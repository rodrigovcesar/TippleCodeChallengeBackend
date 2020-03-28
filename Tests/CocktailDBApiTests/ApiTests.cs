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

            var cocktails = await _cocktailDBApi.GetIngredientSearch(ingredient);


            Assert.Equal(95, cocktails.Drinks.Count());
            _output.WriteLine(cocktails.Drinks.First().StrDrink);
        }

        [Fact]
        public async Task FetchCocktailById()
        {
            var ingredient = "Gin";

            var cocktails = await _cocktailDBApi.GetIngredientSearch(ingredient);


            Assert.Equal(95, cocktails.Drinks.Count());
        }


    }
}

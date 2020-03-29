using CocktailDBApi;
using CocktailDBApi.Exceptions;
using CocktailDBApiTests.Infrastructure;
using NSubstitute;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CocktailDBApiTests
{
    [Collection("InfrastructureCollection")]
    public class ApiTests
    {
        private readonly ITestOutputHelper _output;
        private readonly InfraFixture _infrastructure;
        private readonly string _fakeurl;

        public ApiTests(ITestOutputHelper outputHelper, InfraFixture infrastructure)
        {
            _output = outputHelper;
            _infrastructure = infrastructure;
            _fakeurl = "http://fake.url";

        }
        [Fact]
        public async Task GetIngredientSearch()
        {            
            var fakeHttpClient = new HttpClient(_infrastructure.OKHttpHandlerFilteredResponse);
            _infrastructure.MockHttpClientFactory.CreateClient().Returns(fakeHttpClient);            
            var ingredient = "Gin";
            var cocktails = await new DBApi(_infrastructure.MockHttpClientFactory, _fakeurl).GetCocktailsListSummary(ingredient);

            Assert.Equal(5, cocktails.Drinks.Count());            
        }

        [Fact]
        public async Task NotFoundResponseOnGet()
        {
            var fakeHttpClient = new HttpClient(_infrastructure.NotFoundRequestHandler);
            _infrastructure.MockHttpClientFactory.CreateClient().Returns(fakeHttpClient);
            var ingredient = "Gin";
            var db = new DBApi(_infrastructure.MockHttpClientFactory, _fakeurl);

            await Assert.ThrowsAsync<NotOkResponseException>(async () => await db.GetCocktailsListSummary(ingredient));
            
        }

        [Fact]
        public async Task FetchCocktailById()
        {
            var fakeHttpClient = new HttpClient(_infrastructure.OKHttpHandlerCocktail);
            _infrastructure.MockHttpClientFactory.CreateClient().Returns(fakeHttpClient);
            var db = new DBApi(_infrastructure.MockHttpClientFactory, _fakeurl);
            var id = 11007;            

            var cocktail = await db.FetchCocktailById(id);

            Assert.Equal("Margarita", cocktail.Drinks.First().StrDrink);
            
        }

        [Fact]
        public async Task GetRandonCocktail()
        {
            var fakeHttpClient = new HttpClient(_infrastructure.OkRandomdRequestHandler);
            _infrastructure.MockHttpClientFactory.CreateClient().Returns(fakeHttpClient);
            var db = new DBApi(_infrastructure.MockHttpClientFactory, _fakeurl);            

            var cocktail = await db.GetRandom();

            Assert.Equal("Margarita", cocktail.Drinks.First().StrDrink);
        }


    }
}

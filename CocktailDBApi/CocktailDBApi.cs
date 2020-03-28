using CocktailDBApi.Exceptions;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CocktailDBApi
{
    public class CocktailDBApi
    {
        private const string BASE_URL = "https://www.thecocktaildb.com/api/json/v1/1/";

        public async Task<IEnumerable<SearchResponse>> GetIngredientSearch(string ingredient)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(BASE_URL + "filter.php?i=" + ingredient);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new NotOkResponseException(NotOkMessage(response.StatusCode));

            string apiFilterResponseJson = await response.Content.ReadAsStringAsync();

            if (apiFilterResponseJson.Length < 1)
                return new List<SearchResponse>();

            return JsonConvert.DeserializeObject<List<SearchResponse>>(apiFilterResponseJson);
        }

        public async Task<CocktailResponse> FetchCocktailById(int id)
        {
            var urlToFetch = BASE_URL + "lookup.php?i=" + id;
            using var httpClient = new HttpClient();

            using var response = await httpClient.GetAsync(urlToFetch);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new NotOkResponseException(NotOkMessage(response.StatusCode));

            string apiLookupResponseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CocktailResponse>(apiLookupResponseJson);
        }

        private string NotOkMessage(System.Net.HttpStatusCode statusCode) => $"Cocktails DB returned {(int)statusCode} status code";
    }
}

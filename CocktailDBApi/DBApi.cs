using CocktailDBApi.Exceptions;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CocktailDBApi
{
    public class DBApi
    {
        private const string BASE_URL = "https://www.thecocktaildb.com/api/json/v1/1/";


        public async Task<CocktailResponse[]> GetCocktailsByIngredient(string ingredient)
        {
            var filteredResponse = await GetCocktailsListSummary(ingredient);
            var drinkIds = from item in filteredResponse.Drinks select item.IdDrink;

            if(drinkIds.Count() > 0)
            {
                var tasksToFetchCocktails = from id in drinkIds select FetchCocktailById(id);
                return await Task.WhenAll(tasksToFetchCocktails);
            }

            return new CocktailResponse[0];            
        }

        private async Task<FilteredResponse> GetCocktailsListSummary(string ingredient)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(BASE_URL + "filter.php?i=" + ingredient);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new NotOkResponseException(NotOkMessage(response.StatusCode));
            
            string apiFilterResponseJson = await response.Content.ReadAsStringAsync();

            if (apiFilterResponseJson.Length < 1)
                return new FilteredResponse();

            return JsonConvert.DeserializeObject<FilteredResponse>(apiFilterResponseJson);            
        }

        public Task<CocktailResponse> FetchCocktailById(int id)
        {
            var urlToFetch = BASE_URL + "lookup.php?i=" + id;
            return GetObjectResponse<CocktailResponse>(urlToFetch);            
        }

        public Task<CocktailResponse> GetRandom()
        {
            var urlToFetch = BASE_URL + "random.php";
            return GetObjectResponse<CocktailResponse>(urlToFetch);
        }       

        private async Task<T> GetObjectResponse<T>(string urlToFtech)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(urlToFtech);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new NotOkResponseException(NotOkMessage(response.StatusCode));

            string apiLookupResponseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(apiLookupResponseJson);
        }

        private string NotOkMessage(System.Net.HttpStatusCode statusCode) => $"Cocktails DB returned {(int)statusCode} status code";
    }
}

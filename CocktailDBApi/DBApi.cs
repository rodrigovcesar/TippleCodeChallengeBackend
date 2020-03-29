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
        private IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public DBApi(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = baseUrl;
        }


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

        public async Task<FilteredResponse> GetCocktailsListSummary(string ingredient)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            using var response = await httpClient.GetAsync(_baseUrl + "filter.php?i=" + ingredient);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new NotOkResponseException(NotOkMessage(response.StatusCode));
            
            string apiFilterResponseJson = await response.Content.ReadAsStringAsync();

            if (apiFilterResponseJson.Length < 1)
                return new FilteredResponse();

            return JsonConvert.DeserializeObject<FilteredResponse>(apiFilterResponseJson);            
        }

        public Task<CocktailResponse> FetchCocktailById(int id)
        {
            var urlToFetch = _baseUrl + "lookup.php?i=" + id;
            return GetObjectResponse<CocktailResponse>(urlToFetch);            
        }

        public Task<CocktailResponse> GetRandom()
        {
            var urlToFetch = _baseUrl + "random.php";
            return GetObjectResponse<CocktailResponse>(urlToFetch);
        }       

        private async Task<T> GetObjectResponse<T>(string urlToFtech)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            using var response = await httpClient.GetAsync(urlToFtech);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new NotOkResponseException(NotOkMessage(response.StatusCode));

            string apiLookupResponseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(apiLookupResponseJson);
        }

        private string NotOkMessage(System.Net.HttpStatusCode statusCode) => $"Cocktails DB returned {(int)statusCode} status code";
    }
}

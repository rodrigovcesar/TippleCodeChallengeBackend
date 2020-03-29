using NSubstitute;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Xunit;

namespace CocktailDBApiTests.Infrastructure
{
    public class InfraFixture : IDisposable
    {
        public string DirectoryName { get; private set; }
        public MockHttpMessageHandler OKHttpHandlerFilteredResponse { get; private set; }
        public MockHttpMessageHandler OKHttpHandlerCocktail { get; private set; }
        public MockHttpMessageHandler NotFoundRequestHandler { get; set; }
        public MockHttpMessageHandler OkRandomdRequestHandler { get; set; }
        

        public IHttpClientFactory MockHttpClientFactory { get; private set; }
        
        public InfraFixture()
        {
            GetDirPath();
            SetupMockHttpHandler();
        }

        public void GetDirPath()
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            DirectoryName = Path.GetDirectoryName(codeBasePath);
        }

        public void SetupMockHttpHandler()
        {   
            var filteredJson = File.ReadAllText(Path.Combine(DirectoryName, @"FilteredResponseExample.json"));
            var oneCocktailJson = File.ReadAllText(Path.Combine(DirectoryName, @"CocktailExample.json"));

            OKHttpHandlerFilteredResponse = new MockHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(filteredJson)
            });

            OKHttpHandlerCocktail = new MockHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(oneCocktailJson)
            });

            NotFoundRequestHandler = new MockHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound
            });

            OkRandomdRequestHandler = new MockHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(oneCocktailJson)
            });



            MockHttpClientFactory = Substitute.For<IHttpClientFactory>();
        }

        public void Dispose()
        {
            OKHttpHandlerFilteredResponse.Dispose();
            OKHttpHandlerCocktail.Dispose();
            NotFoundRequestHandler.Dispose();
        }
    }

    [CollectionDefinition("InfrastructureCollection")]
    public class InfraCollection : ICollectionFixture<InfraFixture> { }
}

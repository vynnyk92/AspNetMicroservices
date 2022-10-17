using Catalog.API;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Catalog.IntegrationTests.Infrastructure
{
    public class TestFixture : IDisposable
    {
        private readonly WebApplicationFactory<Program> _appFactory;
        private readonly HttpClient _httpClient;

        public TestFixture()
        {
            _appFactory ??= new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });
            _httpClient ??= _appFactory.CreateClient();
        }

        public HttpClient GetClient() =>
            _httpClient;

        public void Dispose()
        {
            _appFactory.Dispose();
            _httpClient?.Dispose();
        }
    }
}

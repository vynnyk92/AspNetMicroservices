using System.Net;
using System.Runtime.Intrinsics;
using Catalog.IntegrationTests.Infrastructure;
using Shouldly;
using Xunit.Abstractions;

namespace Catalog.IntegrationTests
{
    [Collection(nameof(TestCollectionFixture))]
    public class UnitTest1 
    {
        private readonly TestFixture _fixture;
        private readonly ITestOutputHelper output;

        public UnitTest1(TestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            this.output = output;
        }

        [Fact]
        public async Task Test1()
        {
            //Arrange
            var client = _fixture.GetClient();
            
            //Act
            var response = await client.GetAsync("/api/v1/Catalog");

            //Assert
            output.WriteLine(response.StatusCode.ToString());
            response.StatusCode.ShouldBe(HttpStatusCode.Ambiguous);
        }
    }
}
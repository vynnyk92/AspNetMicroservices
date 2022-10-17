using System.Net;
using System.Runtime.Intrinsics;
using Catalog.IntegrationTests.Infrastructure;
using Shouldly;

namespace Catalog.IntegrationTests
{
    [Collection(nameof(TestCollectionFixture))]
    public class UnitTest1 
    {
        private readonly TestFixture _fixture;

        public UnitTest1(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Test1()
        {
            //Arrange
            var client = _fixture.GetClient();
            
            //Act
            var response = await client.GetAsync("/api/v1/Catalog");

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
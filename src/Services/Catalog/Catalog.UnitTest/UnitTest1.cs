using Catalog.API.Helpers;
using Shouldly;
using Xunit;

namespace Catalog.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            var a = 11;
            var b = 12;

            //Act
            var res = HelperForUnitTest.Add(a, b);

            //Assert
            res.ShouldBe(23);
        }
    }
}
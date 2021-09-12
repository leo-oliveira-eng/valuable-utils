using FluentAssertions;
using Valuables.Utils;
using Xunit;

namespace Valuable.Utils.Tests.Cnpj
{
    public class IsValidUnitTests
    {
        [Fact]
        public void ValidateCNPJ_ValidParameters_ShouldReturnValidResponse()
        {
            var text = "25.452.866/0001-64";

            var response = CNPJ.IsValid(text);

            response.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1813")]
        [InlineData("22.222.222/2222-22")]
        [InlineData("any text")]
        [InlineData("61.199.881/0001-32")]
        [InlineData("504.165.829/0001-03")]
        public void ValidateCNPJ_InvalidParameters_ShouldReturnValidResponseWithBusinessError(string text)
        {
            var response = CNPJ.IsValid(text);

            response.Should().BeFalse();
        }
    }
}

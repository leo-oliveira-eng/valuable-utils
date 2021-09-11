using FluentAssertions;
using Valuables.Utils;
using Xunit;

namespace Valuable.Utils.Tests.Cpf
{
    public class IsValidUnitTests
    {
        [Fact]
        public void CreateCPF_ValidParameters_ShouldReturnValidResponse()
        {
            var text = "696.249.670-24";

            var response = CPF.IsValid(text);

            response.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1813")]
        [InlineData("12345678909")]
        [InlineData("222.222.222-22")]
        [InlineData("any text")]
        [InlineData("036.688.790-00")]
        [InlineData("036.688.790-00.1234")]
        public void CreateCPF_InvalidParameters_ShouldReturnValidResponseWithBusinessError(string text)
        {
            var response = CPF.IsValid(text);

            response.Should().BeFalse();
        }
    }
}

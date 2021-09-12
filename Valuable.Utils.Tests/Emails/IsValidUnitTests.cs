using FluentAssertions;
using Valuables.Utils;
using Xunit;

namespace Valuable.Utils.Tests.Emails
{
    public class IsValidUnitTests
    {
        [Fact]
        public void ValidateEmail_ValidParameters_ShouldReturnValidResponse()
        {
            var text = "none@nothing.com";

            var response = Email.IsValid(text);

            response.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1813")]
        [InlineData("@nothing.com")]
        [InlineData("any.text")]
        [InlineData("44.35.com@;:\\") ]
        [InlineData("nothing.com.br")]
        public void ValidateEmail_InvalidParameters_ShouldReturnValidResponseWithBusinessError(string text)
        {
            var response = Email.IsValid(text);

            response.Should().BeFalse();
        }
    }
}

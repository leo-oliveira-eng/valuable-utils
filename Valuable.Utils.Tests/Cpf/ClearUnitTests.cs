using FluentAssertions;
using Valuables.Utils;
using Xunit;

namespace Valuable.Utils.Tests.Cpf
{
    public class ClearUnitTests
    {
        [Fact]
        public void ClearCPF_ValidParameters_ShouldReturnValidResponse()
        {
            var text = "696.249.670-24";

            var response = CPF.Clear(text);

            response.Should().Be("69624967024");
        }
    }
}

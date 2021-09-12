using FluentAssertions;
using Valuables.Utils;
using Xunit;

namespace Valuable.Utils.Tests.Cnpj
{
    public class ClearUnitTests
    {
        [Fact]
        public void ClearCNPJ_ValidParameters_ShouldReturnValidResponse()
        {
            var text = "04.165.829/0001-03";

            var response = CNPJ.Clear(text);

            response.Should().Be("04165829000103");
        }
    }
}

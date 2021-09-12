using FluentAssertions;
using Messages.Core.Enums;
using Valuables.Utils;
using Xunit;

namespace Valuable.Utils.Tests.Cnpj
{
    public class CreateUnitTests
    {
        [Fact]
        public void CreateCNPJ_ValidParameters_ShouldReturnValidResponse()
        {
            var text = "04.165.829/0001-03";

            var cpfResponse = CNPJ.Create(text);

            cpfResponse.HasError.Should().BeFalse();
            cpfResponse.Data.Should().NotBeNull();
            cpfResponse.Data.HasValue.Should().BeTrue();
            cpfResponse.Data.Value.Should().BeOfType(typeof(CNPJ));
            cpfResponse.Data.Value.Text.Should().Be(CNPJ.Clear(text));
            cpfResponse.Data.Value.IsValid().Should().BeTrue();
        }

        [Fact]
        public void CreateCPF_InvalidParameters_ShouldReturnValidResponseWithBusinessError()
        {
            var cpfResponse = CNPJ.Create(null);

            cpfResponse.HasError.Should().BeTrue();
            cpfResponse.Data.HasValue.Should().BeFalse();
            cpfResponse.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
        }
    }
}

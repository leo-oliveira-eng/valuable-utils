using FluentAssertions;
using Messages.Core.Enums;
using Valuables.Utils;
using Xunit;

namespace Valuable.Utils.Tests.Cpf
{
    public class CreateUnitTests
    {
        [Fact]
        public void CreateCPF_ValidParameters_ShouldReturnValidResponse()
        {
            var text = "696.249.670-24";

            var cpfResponse = CPF.Create(text);

            cpfResponse.HasError.Should().BeFalse();
            cpfResponse.Data.Should().NotBeNull();
            cpfResponse.Data.HasValue.Should().BeTrue();
            cpfResponse.Data.Value.Should().BeOfType(typeof(CPF));
            cpfResponse.Data.Value.Text.Should().Be(CPF.Clear(text));
            cpfResponse.Data.Value.IsValid().Should().BeTrue();
        }

        [Fact]
        public void CreateCPF_InvalidParameters_ShouldReturnValidResponseWithBusinessError()
        {
            var cpfResponse = CPF.Create(null);

            cpfResponse.HasError.Should().BeTrue();
            cpfResponse.Data.HasValue.Should().BeFalse();
            cpfResponse.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
        }
    }
}

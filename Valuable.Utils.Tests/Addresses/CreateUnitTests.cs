using FluentAssertions;
using Messages.Core.Enums;
using Valuables.Utils;
using Xunit;

namespace Valuable.Utils.Tests.Addresses
{
    public class CreateUnitTests
    {
        [Fact]
        public void CreateAddress_ValidParameters_ShouldReturnValidResponse()
        {
            var response = Address.Create("20021-290", "Rua do Passeio", "Centro", "38", "Rio de Janeiro", "RJ");

            response.HasError.Should().BeFalse();
            response.Data.Should().NotBeNull();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(Address));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("12.212-431")]
        [InlineData("120732198")]
        [InlineData("abc123")]
        public void CreateAddress_InvalidCEP_ShouldReturnResponseWithBusinessError(string cep)
        {
            var response = Address.Create(cep, "Rua do Passeio", "Centro", "38", "Rio de Janeiro", "RJ");

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("cep"));
        }

        [Fact]
        public void CreateAddress_InvalidStreet_ShouldReturnResponseWithBusinessError()
        {
            var response = Address.Create("20021-290", string.Empty, "Centro", "38", "Rio de Janeiro", "RJ");

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("street"));
        }

        [Fact]
        public void CreateAddress_InvalidNeighborhood_ShouldReturnResponseWithBusinessError()
        {
            var response = Address.Create("20021-290", "Rua do Passeio", string.Empty, "38", "Rio de Janeiro", "RJ");

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("neighborhood"));
        }

        [Fact]
        public void CreateAddress_InvalidNumber_ShouldReturnResponseWithBusinessError()
        {
            var response = Address.Create("20021-290", "Rua do Passeio", "Centro", string.Empty, "Rio de Janeiro", "RJ");

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("number"));
        }

        [Fact]
        public void CreateAddress_InvalidCity_ShouldReturnResponseWithBusinessError()
        {
            var response = Address.Create("20021-290", "Rua do Passeio", "Centro", "38", string.Empty, "RJ");

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("city"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("ANY")]
        public void CreateAddress_InvalidUF_ShouldReturnResponseWithBusinessError(string uf)
        {
            var response = Address.Create("20021-290", "Rua do Passeio", "Centro", "38", "Rio de Janeiro", uf);

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("uf"));
        }
    }
}

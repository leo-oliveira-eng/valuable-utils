using FluentAssertions;
using Messages.Core.Enums;
using Valuables.Utils;
using Xunit;

namespace Valuable.Utils.Tests.Addresses
{
    public class UpdateUnitTests
    {
        [Fact]
        public void UpdateAddress_ValidParameters_ShouldReturnValidResponse()
        {
            var resposenAddress = Address.Create("22222-222", "Any Street", "Any Neighborhood", "Any Number", "Any City", "SP");

            var address = resposenAddress.Data.Value;

            var response = address.Update("20021-290", "Rua do Passeio", "Centro", "38", "Rio de Janeiro", "RJ");

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
        public void UpdateAddress_InvalidCEP_ShouldReturnResponseWithBusinessError(string cep)
        {
            var resposenAddress = Address.Create("22222-222", "Any Street", "Any Neighborhood", "Any Number", "Any City", "SP");

            var address = resposenAddress.Data.Value;

            var response = address.Update(cep, "Rua do Passeio", "Centro", "38", "Rio de Janeiro", "RJ");

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("cep"));
        }

        [Fact]
        public void UpdateAddress_InvalidStreet_ShouldReturnResponseWithBusinessError()
        {
            var resposenAddress = Address.Create("22222-222", "Any Street", "Any Neighborhood", "Any Number", "Any City", "SP");

            var address = resposenAddress.Data.Value;

            var response = address.Update("20021-290", string.Empty, "Centro", "38", "Rio de Janeiro", "RJ");

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("street"));
        }

        [Fact]
        public void UpdateAddress_InvalidNeighborhood_ShouldReturnResponseWithBusinessError()
        {
            var resposenAddress = Address.Create("22222-222", "Any Street", "Any Neighborhood", "Any Number", "Any City", "SP");

            var address = resposenAddress.Data.Value;

            var response = address.Update("20021-290", "Rua do Passeio", string.Empty, "38", "Rio de Janeiro", "RJ");

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("neighborhood"));
        }

        [Fact]
        public void UpdateAddress_InvalidNumber_ShouldReturnResponseWithBusinessError()
        {
            var resposenAddress = Address.Create("22222-222", "Any Street", "Any Neighborhood", "Any Number", "Any City", "SP");

            var address = resposenAddress.Data.Value;

            var response = address.Update("20021-290", "Rua do Passeio", "Centro", string.Empty, "Rio de Janeiro", "RJ");

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("number"));
        }

        [Fact]
        public void UpdateAddress_InvalidCity_ShouldReturnResponseWithBusinessError()
        {
            var resposenAddress = Address.Create("22222-222", "Any Street", "Any Neighborhood", "Any Number", "Any City", "SP");

            var address = resposenAddress.Data.Value;

            var response = address.Update("20021-290", "Rua do Passeio", "Centro", "38", string.Empty, "RJ");

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("city"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("ANY")]
        public void UpdateAddress_InvalidUF_ShouldReturnResponseWithBusinessError(string uf)
        {
            var resposenAddress = Address.Create("22222-222", "Any Street", "Any Neighborhood", "Any Number", "Any City", "SP");

            var address = resposenAddress.Data.Value;

            var response = address.Update("20021-290", "Rua do Passeio", "Centro", "38", "Rio de Janeiro", uf);

            response.HasError.Should().BeTrue();
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(message => message.Property.Equals("uf"));
        }
    }
}

using FluentAssertions;
using Messages.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Valuables.Utils;
using Xunit;

namespace Valuable.Utils.Tests.Emails
{
    public class CreateUnitTests
    {
        [Fact]
        public void CreateEmail_ValidParameters_ShouldReturnValidResponse()
        {
            var text = "any@anything.com";

            var cpfResponse = Email.Create(text);

            cpfResponse.HasError.Should().BeFalse();
            cpfResponse.Data.Should().NotBeNull();
            cpfResponse.Data.HasValue.Should().BeTrue();
            cpfResponse.Data.Value.Should().BeOfType(typeof(Email));
            cpfResponse.Data.Value.Address.Should().Be(text);
            cpfResponse.Data.Value.IsValid().Should().BeTrue();
        }

        [Fact]
        public void CreateEmail_InvalidParameters_ShouldReturnValidResponseWithBusinessError()
        {
            var cpfResponse = Email.Create(null);

            cpfResponse.HasError.Should().BeTrue();
            cpfResponse.Data.HasValue.Should().BeFalse();
            cpfResponse.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
        }
    }
}

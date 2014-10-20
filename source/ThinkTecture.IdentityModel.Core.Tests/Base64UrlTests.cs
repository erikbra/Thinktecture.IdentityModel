using System;
using Thinktecture.IdentityModel;
using Xunit;
using FluentAssertions;

namespace ThinkTecture.IdentityModel.Core.Tests
{
    public class Base64UrlTests
    {
        [Fact]
        public void UrlEncoded_String_Does_Not_Contain_Trailing_Equal_Signs()
        {
            var encoded = Base64Url.Encode(GetTestData());
            encoded.Should().NotContain("=");
        }

        [Fact]
        public void UrlEncoded_String_Does_Not_Contain_Plus_Signs()
        {
            var encoded = Base64Url.Encode(GetTestData());
            encoded.Should().NotContain("+");
        }

        [Fact]
        public void UrlEncoded_String_Does_Not_Contain_Forward_Slash()
        {
            var encoded = Base64Url.Encode(GetTestData());
            encoded.Should().NotContain("/");
        }

        [Fact]
        public void UrlEncoded_String_Replaces_All_Special_Characters()
        {
            byte[] testData = GetTestData();

            var encoded = Base64Url.Encode(testData);
            var base64Encoded = Convert.ToBase64String(testData);

            var expected =
                base64Encoded
                    .Replace("+", "-")
                    .Replace("/", "_")
                    .TrimEnd('=');

            encoded.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void UrlDecoded_Is_Equal_To_Original()
        {
            byte[] testData = GetTestData();

            var encoded = Base64Url.Encode(testData);
            var decoded = Base64Url.Decode(encoded);

            decoded.Should().BeEquivalentTo(testData);
        }

        private byte[] GetTestData()
        {
            var bytes = new byte[256];
            for (var x = 0; x < bytes.Length; x++)
            {
                bytes[x] = (byte)x;
                //Console.Write("{0:X2} ", bytes[x]);
                //if (((x + 1) % 20) == 0) Console.WriteLine();
            }

            return bytes;
        }
    }

}

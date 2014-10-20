/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see LICENSE
 */

using System;
using System.Security.Principal;
using System.Text;

namespace Thinktecture.IdentityModel
{
    public static class Base64Url
    {
        public static string Encode(byte[] arg)
        {
            string s = Convert.ToBase64String(arg); // Standard base64 encoder

            var builder = new StringBuilder(s);

            builder.Replace('+', '-');
            builder.Replace('/', '_');

            return builder.ToString().TrimEnd('=');
        }

        public static byte[] Decode(string arg)
        {
            var builder = new StringBuilder(arg);

            builder.Replace('-', '+');
            builder.Replace('_', '/');

            switch (builder.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: builder.Append("=="); break; // Two pad chars
                case 3: builder.Append("="); break; // One pad char
                default: throw new Exception("Illegal base64url string!");
            }

            return Convert.FromBase64String(builder.ToString()); // Standard base64 decoder
        }
    }
}

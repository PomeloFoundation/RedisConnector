using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Commands.Parsers
{
    internal static class ByteArrayHelper
    {
        public static readonly byte[] Empty = new byte[0];

        public static readonly byte[] ParamPrefix = new byte[] { 64 };
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Commands.Parsers
{
    internal class StringToken : Token
    {
        private readonly byte[] _value;

        public StringToken(TokenType type, byte[] value) : base(type)
        {
            _value = value;
        }

        public override string ToString()
        {
            return $"${_value.Length}{Environment.NewLine}{Encoding.UTF8.GetString(_value)}{Environment.NewLine}";
        }
    }
}

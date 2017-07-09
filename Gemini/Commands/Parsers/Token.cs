using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Commands.Parsers
{
    public class Token
    {
        public TokenType Type { get; }

        public Token(TokenType type)
        {
            Type = type;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
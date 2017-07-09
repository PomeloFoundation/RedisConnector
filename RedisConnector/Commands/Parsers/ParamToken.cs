using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Commands.Parsers
{
    internal class ParamToken : Token
    {
        public ParamToken(TokenType type) : base(type)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Commands.Parsers
{
    public enum TokenType
    {
        STRING,
        PARAM,
        EOF
    }
}
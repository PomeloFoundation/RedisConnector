using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Commands
{
    public enum CommandParameterType
    {
        SimpleString,
        BulkString,
        Hash,
        Array,
    }
}

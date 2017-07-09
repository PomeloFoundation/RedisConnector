using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Expressions
{
    public enum ProtocolExpressionType
    {
        SimpleString,
        Error,
        Integer,
        BulkString,
        Array
    }
}
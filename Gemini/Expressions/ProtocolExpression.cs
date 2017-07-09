using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Expressions
{
    public abstract class ProtocolExpression
    {
        public abstract int Length { get; protected set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

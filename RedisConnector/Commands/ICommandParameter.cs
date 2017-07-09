using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Commands
{
    public interface ICommandParameter
    {
        CommandParameterType CommandParameterType { get; set; }
    }
}

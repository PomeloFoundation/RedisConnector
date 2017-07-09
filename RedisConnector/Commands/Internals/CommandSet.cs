using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Commands.Internals
{
    internal sealed class CommandSet
    {
        internal static CommandSet Default { get; } = new CommandSet();

        private readonly HashSet<string> _set;

        private CommandSet()
        {
            _set = new HashSet<string>();
            //_set.Add("APPEND");
            //_set.Add("AUTH");
            //_set.Add("BGREWRITEAOF");
            //_set.Add("BGSAVE");
            //_set.Add("BITCOUNT");
            //_set.Add("BITFIELD");
            //_set.Add("BITOP");
        }
    }
}

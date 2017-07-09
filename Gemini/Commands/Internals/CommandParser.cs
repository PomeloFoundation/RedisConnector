using RedisConnector.Protocols;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RedisConnector.Commands.Internals
{
    public sealed class CommandParser
    {
        private readonly static char[] SpaceSeparator = new char[] { ' ' };
        private readonly static string NewLine = Environment.NewLine;
        private readonly static char ParameterPrefix = '@';

        [Obsolete("Use RedisConnector.Commands.Parsers.Parser ^*^")]
        public string Parse(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                throw new ArgumentNullException(nameof(command));
            }
            var commandItems = command.Split(separator: SpaceSeparator, options: StringSplitOptions.RemoveEmptyEntries);
            if (commandItems.Length <= 0)
            {
                throw new ArgumentException("Invalid command.", nameof(command));
            }
            if (command.Length == 1)
            {
                var commandItem = commandItems[0];
                return $"*1{NewLine}${commandItem.Length}{NewLine}{commandItem}{NewLine}";
            }
            if (commandItems.Any(item => item[0] == ParameterPrefix))
            {

            }

            var stringBuilder = new StringBuilder();
            var length = commandItems.Length;
            stringBuilder.Append(PrefixCharacter.Arrays);
            stringBuilder.Append(length);
            stringBuilder.Append(NewLine);
            for (var i = 0; i < length; i++)
            {
                var commandItem = commandItems[i];
                stringBuilder.Append(PrefixCharacter.BulkStrings);
                stringBuilder.Append(commandItem.Length);
                stringBuilder.Append(NewLine);
                stringBuilder.Append(commandItem);
                stringBuilder.Append(NewLine);
            }
            return stringBuilder.ToString();
        }
    }
}

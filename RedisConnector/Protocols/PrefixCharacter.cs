using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Protocols
{
    public static class PrefixCharacter
    {
        public const Char SimpleStrings = '+';

        public const Char Errors = '-';

        public const Char Integers = ':';

        public const Char BulkStrings = '$';

        public const Char Arrays = '*';
    }
}
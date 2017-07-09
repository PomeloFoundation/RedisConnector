using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConnector.Commands.Parsers
{
    public class Lexer
    {
        private readonly Queue<Token> _tokens;
        private readonly byte[] _document;
        private readonly int _documentSize;
        private bool _lexState;
        private int _index;

        public Queue<Token> Tokens => _tokens;

        public Lexer(string document)
        {
            if (string.IsNullOrEmpty(document))
            {
                throw new ArgumentNullException(nameof(document));
            }
            _index = 0;
            _lexState = true;
            _document = Encoding.UTF8.GetBytes(document);
            _documentSize = _document.Length;
            _tokens = new Queue<Token>();
        }

        public bool Lex()
        {
            while (_lexState && _index < _documentSize)
            {
                var token = GetNextToken();
                if (token != null)
                {
                    _tokens.Enqueue(token);
                }
            }
            return _lexState;
        }

        public Token GetNextToken()
        {
            switch (_document[_index])
            {
                //  '\n'
                case 10:
                //  '\r'
                case 13:
                //  ' '
                case 32:
                    _index++;
                    return null;
                // '"'
                case 34:
                    var start = ++_index;
                    while (_index < _documentSize) if (_document[++_index] == 34) break;
                    if (_index >= _documentSize) goto Error;
                    _index++;
                    var value = new byte[_index - start - 1];
                    if (value.Length == 0) return new StringToken(TokenType.STRING, ByteArrayHelper.Empty);
                    Array.Copy(_document, start, value, 0, value.Length);
                    return new StringToken(TokenType.STRING, value);
                //  '
                case 39:
                    start = ++_index;
                    while (_index < _documentSize) if (_document[++_index] == 39) break;
                    if (_index >= _documentSize) goto Error;
                    _index++;
                    value = new byte[_index - start - 1];
                    if (value.Length == 0) return new StringToken(TokenType.STRING, ByteArrayHelper.Empty);
                    Array.Copy(_document, start, value, 0, value.Length);
                    return new StringToken(TokenType.STRING, value);
                // @
                case 64:
                    start = ++_index;
                    while (_index < _documentSize && _document[_index] != 32)
                    {
                        _index++;
                    }
                    value = new byte[_index - start];
                    if (value.Length == 0) return new StringToken(TokenType.STRING, ByteArrayHelper.ParamPrefix);
                    Array.Copy(_document, start, value, 0, value.Length);
                    return new StringToken(TokenType.STRING, value);
                default:
                    start = _index++;
                    while (_index < _documentSize && _document[_index] != 32)
                    {
                        if (_document[_index] == 34 || _document[_index] == 39) goto Error;
                        _index++;
                    }
                    value = new byte[_index - start];
                    if (value.Length == 0) return new StringToken(TokenType.STRING, ByteArrayHelper.Empty);
                    Array.Copy(_document, start, value, 0, value.Length);
                    return new StringToken(TokenType.STRING, value);
            }
            Error:
            _lexState = false;
            return null;
        }
    }
}
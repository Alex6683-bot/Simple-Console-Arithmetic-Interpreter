using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexerTest.src.Parser
{
    internal class Token
    {
        public string value;
        public TokenType type;
        public Token(string _value, TokenType _type)
        {
            value = _value;
            type = _type;
        }
    }
}

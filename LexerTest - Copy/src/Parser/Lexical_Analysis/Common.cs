using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexerTest.src.Parser
{
    public enum TokenType
    {
        NUMBER,
        ADD,
        SUB,
        MUL,
        DIV,
        STRING,
        LEFTPAR,
        RIGHTPAR,
        EXP,
        NONE,
        FUNC
    }
}

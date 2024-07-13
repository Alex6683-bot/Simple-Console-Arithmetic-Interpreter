using LexerTest.src.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LexerTest.src.Syntax
{
    internal static class ParenthesisAnalyzer
    {
        public static (Error, bool) Validate(List<Token> tokens)
        {
            if (tokens.Any(token => token.type == TokenType.LEFTPAR || token.type == TokenType.RIGHTPAR))
            {
                int parenthesesState = ReturnParenthesisState(tokens);
                if (parenthesesState < 0)
                {
                    return (new Error("ERROR : Expecting '('"), true);
                }
                else if (parenthesesState > 0)
                {
                    return (new Error("ERROR : Expecting ')'"), true);
                }
                else
                {
                    return (new Error(""), true);
                }
            }
            else
                return (new Error(""), false);
        }

        private static int ReturnParenthesisState(List<Token> tokens) // -> -1 : Unbalanced, 1 : Balanced
        {
            int count = 0;

            foreach (Token token in tokens)
            {
                if (token.type == TokenType.LEFTPAR)
                {
                    count++;
                }
                else if (token.type == TokenType.RIGHTPAR)
                {
                    count--;
                    if (count < 0)
                    {
                        return -1;
                    }
                }
            }

            return count;
        }
    }
}

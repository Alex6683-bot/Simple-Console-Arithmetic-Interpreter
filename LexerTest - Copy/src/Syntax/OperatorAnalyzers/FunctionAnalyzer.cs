using LexerTest.src.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexerTest.src.Syntax
{
    internal static class FunctionAnalyzer
    {
        public static (Error, bool) ValidateFunctions(List<Token> tokens)
        {
            bool containsOperator = false;
            if (tokens.Any(x => x.type == TokenType.FUNC))
            {
                containsOperator = true;
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i].type == TokenType.FUNC)
                    {
                        if (
                            i == tokens.Count - 1 ||
                            (tokens[i + 1].type != TokenType.NUMBER &&
                            tokens[i + 1].type != TokenType.LEFTPAR)
                           )
                            return (new Error($"ERROR : Invalid Syntax for type {tokens[i].type}"), containsOperator);
            }

                }
            }
            return (new Error(""), containsOperator);
        }
    }
}

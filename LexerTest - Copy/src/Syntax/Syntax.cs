using LexerTest.src.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexerTest.src.Syntax
{
    internal static class Syntax
    {
        public static Error[] ValidateTokens(List<Token> tokens)
        {
            List<Error> errors = new List<Error>();
            (Error, bool) parenthesis = ParenthesisAnalyzer.Validate(tokens);
            (Error, bool) arithmetic = ArithmeticOperatorAnalyzer.ValidateOperators(tokens);
            //(Error, bool) function = FunctionAnalyzer.ValidateFunctions(tokens);

            //Checking each analyzers
            if (parenthesis.Item1.message != "" && parenthesis.Item2 ) errors.Add(parenthesis.Item1);
            if (arithmetic.Item1.message != "" && arithmetic.Item2) errors.Add(arithmetic.Item1);
            //if (function.Item1.message != "" && function.Item2) errors.Add(function.Item1);

            //Checking for invalid string (can be further manipulated later)
            if (tokens.Any(token => token.type == TokenType.STRING)) errors.Add(new Error("ERROR: Invalid Expression"));
            return errors.ToArray();
        }
    }
}

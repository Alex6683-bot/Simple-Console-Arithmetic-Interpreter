using LexerTest.src.Evaluation;
using LexerTest.src.Parser;
using LexerTest.src.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexerTest
{
    class Interpreter
    {
        List<Token> currentTokens = new List<Token>();
        public void Evaluate(string input)
        {
            List<Token> stringTokens = new Tokenizer().Tokenize(input);
            ExpressionSimplifier.NormalizeExpression(stringTokens);
            //Validation
            Error[] errors = Syntax.ValidateTokens(stringTokens);

            if (errors.Length > 0)
                foreach (var error in errors) Error.DisplayMessage(error);
            else
            {
                Evaluator.Evaluate(stringTokens);
                currentTokens = stringTokens;
            }

        }

        public void DisplayCurrentResult()
        {
            foreach (var tokens in currentTokens) Console.Write(tokens.value);
            Console.WriteLine();
        }
    }
}

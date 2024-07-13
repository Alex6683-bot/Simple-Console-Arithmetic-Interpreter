using LexerTest.src.Parser;

namespace LexerTest.src.Syntax
{
    internal static class ArithmeticOperatorAnalyzer
    {
        static TokenType[] validArithmeticTypes =
        {
            TokenType.ADD,
            TokenType.SUB,
            TokenType.MUL,
            TokenType.DIV,
            TokenType.EXP,
        };

        public static (Error, bool) ValidateOperators(List<Token> tokens)
        {
            bool containsOperator = false;
            if (tokens.Any(x => validArithmeticTypes.Contains(x.type)))
            {
                containsOperator = true;
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (validArithmeticTypes.Contains(tokens[i].type))
                    {
                        if (tokens[i].type == TokenType.DIV)
                        {
                            if (tokens[i + 1].type == TokenType.NUMBER && tokens[i + 1].value == 0.ToString()) 
                                return (new Error($"ERROR : Division by 0 detected"), containsOperator);
                        }
                        if (CheckOperatorSyntax())
                            return (new Error($"ERROR : {tokens[i].type} takes two values on both sides"), containsOperator);    
                    }

                    bool CheckOperatorSyntax()
                    {
                        return (i == 0 || i == tokens.Count - 1 ||
                            (tokens[i + 1].type != TokenType.NUMBER && tokens[i + 1].type != TokenType.LEFTPAR && tokens[i + 1].type != TokenType.FUNC) ||
                            (tokens[i - 1].type != TokenType.NUMBER && tokens[i - 1].type != TokenType.RIGHTPAR) && tokens[i + 1].type != TokenType.FUNC);
                    }
                    
                }
            }
            return (new Error(""), containsOperator);
        }
    }
}

using LexerTest.src.Parser;

namespace LexerTest.src.Syntax
{
    internal static class ExpressionSimplifier
    {
        public static void NormalizeExpression(List<Token> tokens)
        {
            NormalizeParenthesis(tokens);
            NormalizeOperators(tokens);
        }

        public static void NormalizeOperators(List<Token> tokens)
        {
            TokenType[] possibleArithmeticTypes =
            {
                TokenType.ADD,
                TokenType.SUB,
                TokenType.MUL,
                TokenType.DIV,
                TokenType.EXP,
                //TokenType.FUNC Functions are currently diabled due to issues.
            };

            if (tokens.Any(x => possibleArithmeticTypes.Contains(x.type)))
            {
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i].type == TokenType.ADD && i != tokens.Count - 1)
                    {
                        if (tokens[i + 1].type == TokenType.SUB) ReplaceOperators(new Token("-", TokenType.SUB));
                        else if (tokens[i + 1].type == TokenType.ADD) ReplaceOperators(new Token("+", TokenType.ADD));
                        if (tokens[i + 1].type == TokenType.NUMBER) ResolveAdjacentNumberOperator(tokens[i].type);
                    }
                    else if (tokens[i].type == TokenType.SUB && i != tokens.Count - 1)
                    {
                        if (tokens[i + 1].type == TokenType.SUB) ReplaceOperators(new Token("+", TokenType.ADD));
                        else if (tokens[i + 1].type == TokenType.ADD) ReplaceOperators(new Token("-", TokenType.SUB));
                        if (tokens[i + 1].type == TokenType.NUMBER) ResolveAdjacentNumberOperator(tokens[i].type);
                    }
                    /*else if (tokens[i].type == TokenType.NUMBER && i != tokens.Count - 1)  
                    {
                        if (tokens[i + 1].type == TokenType.FUNC) this code is omitted due to issues with functions
                        {
                            tokens.Insert(i + 1, new Token("*", TokenType.FUNC));
                        }
                    }*/

                    void ReplaceOperators(Token newToken)
                    {
                        tokens.RemoveAt(i);
                        tokens.RemoveAt(i);
                        tokens.Insert(i, newToken);
                        i--;
                    }

                    void ResolveAdjacentNumberOperator(TokenType operatorType)
                    {
                        if (i == 0 || tokens[i - 1].type != TokenType.NUMBER && tokens[i - 1].type != TokenType.RIGHTPAR)
                        {
                            if (tokens[i + 1].type == TokenType.NUMBER)
                            {
                                float number = 0;
                                if (operatorType == TokenType.ADD) number = float.Parse(tokens[i + 1].value) * 1;
                                else if (operatorType == TokenType.SUB) number = float.Parse(tokens[i + 1].value) * -1;

                                ReplaceOperators(new Token(number.ToString(), TokenType.NUMBER));
                            }
                        }
                    }
                }
            }
        }

        private static void NormalizeParenthesis(List<Token> tokens)
        {
            if (tokens.Any(x => x.type == TokenType.LEFTPAR || x.type == TokenType.RIGHTPAR))
            {
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (i != 0 && i != tokens.Count - 1)
                    {
                        if (tokens[i].type == TokenType.LEFTPAR)
                        {
                            if (tokens[i - 1].type == TokenType.NUMBER || tokens[i - 1].type == TokenType.RIGHTPAR) tokens.Insert(i, new Token("*", TokenType.MUL));
                        }
                        else if (tokens[i].type == TokenType.RIGHTPAR)
                        {
                            if (tokens[i + 1].type == TokenType.NUMBER || tokens[i + 1].type == TokenType.LEFTPAR) tokens.Insert(i + 1, new Token("*", TokenType.MUL));
                        }
                    }
                    if (i != tokens.Count - 1)
                    {
                        if (tokens[i].type == TokenType.LEFTPAR && tokens[i+1].type == TokenType.RIGHTPAR)
                        {
                            tokens.RemoveAt(i + 1);
                            tokens.RemoveAt(i);
                            tokens.Insert(i, new Token(0.ToString(), TokenType.NUMBER));
                        }
                    }
                }
            }
        }
    }
}

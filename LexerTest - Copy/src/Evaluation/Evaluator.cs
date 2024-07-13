using LexerTest.src.Parser;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace LexerTest.src.Evaluation
{
    internal static class Evaluator
    {

        public static void Evaluate(List<Token> tokens)
        {
            EvaluateParenthesis(tokens);
            //EvaluateFunctions(tokens);
            EvaluateOperators(tokens);
        }

        private static void EvaluateOperators(List<Token> tokens)
        {
            EvaluateOperators(tokens, TokenType.EXP); //Indices
            EvaluateOperators(tokens, TokenType.MUL); //Multiplication
            EvaluateOperators(tokens, TokenType.DIV); //Division
            EvaluateOperators(tokens, TokenType.ADD); //Addition
            EvaluateOperators(tokens, TokenType.SUB); //Subtraction
        }

        #region EvaluateOperators
        private static void EvaluateOperators(List<Token> tokens, TokenType type)
        {
            float result = 0;
            if (tokens.Any(x => x.type == type))
            {
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i].type == type)
                    {
                        float.TryParse(tokens[i - 1].value, out float left);
                        float.TryParse(tokens[i + 1].value, out float right);

                        if (type == TokenType.ADD)
                        {
                            result = left + right;
                            ReplaceTokens(tokens, i, new Token(result.ToString(), TokenType.NUMBER));

                            i--;
                        }
                        if (type == TokenType.MUL)
                        {
                            result = left * right;
                            ReplaceTokens(tokens, i, new Token(result.ToString(), TokenType.NUMBER));
                            i--;
                        }
                        if (type == TokenType.SUB)
                        {
                            result = left - right;
                            ReplaceTokens(tokens, i, new Token(result.ToString(), TokenType.NUMBER));
                            i--;
                        }
                        if (type == TokenType.DIV)
                        {
                            result = left / right;
                            ReplaceTokens(tokens, i, new Token(result.ToString(), TokenType.NUMBER));
                            i--;
                        }
                        if (type == TokenType.EXP)
                        {
                            result = (float)Math.Pow(Convert.ToDouble(left), Convert.ToDouble(right));
                            ReplaceTokens(tokens, i, new Token(result.ToString(), TokenType.NUMBER));
                            i--;
                        }
                        
                    }
                }
            }
        }
        #endregion
        private static void ReplaceTokens(List<Token> tokens, int index, Token token)
        {
            tokens.RemoveAt(index);
            tokens.Insert(index, token);
            tokens.RemoveAt(index + 1);
            tokens.RemoveAt(index - 1);
        }

        #region Parenthesis

        private static void EvaluateParenthesis(List<Token> subTokens)
        {
            List<Token> accumulatedTokens = new List<Token>();
            int parenthesisValue = 0;

            int startRange = 0;
            int endRange = 0;

            for (int i = 0; i < subTokens.Count; i++)
            {
                if (subTokens[i].type == TokenType.LEFTPAR)
                {
                    if (parenthesisValue == 0)
                    {
                        startRange = i;
                        parenthesisValue++;
                        continue;
                    }
                    parenthesisValue++;
                }
                if (subTokens[i].type == TokenType.RIGHTPAR)
                {
                    parenthesisValue--;
                    if (parenthesisValue == 0) endRange = i;
                }
                if (parenthesisValue != 0) accumulatedTokens.Add(subTokens[i]);
                else
                { 
                    if (accumulatedTokens.Count > 0)
                    {
                        if (accumulatedTokens.Any(x => x.type == TokenType.LEFTPAR || x.type == TokenType.RIGHTPAR)) EvaluateParenthesis(accumulatedTokens);
                        EvaluateOperators(accumulatedTokens);


                        //Replace tokens
                        subTokens.RemoveRange(startRange, (endRange - startRange) + 1);
                        i -= (endRange - startRange);
                        if (subTokens.Count > 0) subTokens.Insert(i, accumulatedTokens[0]);
                        else subTokens.Add(accumulatedTokens[0]);

                        accumulatedTokens.Clear();
                    }
                }
            }

        }

        #endregion

        #region Functions
        private static void EvaluateFunctions(List<Token> tokens)
        {
            for (int i = 0; i < tokens.Count; i++) 
            {
                if (tokens[i].type == TokenType.FUNC)
                {
                    switch (tokens[i].value)
                    {
                        case "sin":
                            ReplaceTokens((float)Math.Sin(double.Parse(tokens[i + 1].value)));
                            break;
                        case "cos":
                            ReplaceTokens((float)Math.Cos(double.Parse(tokens[i + 1].value)));
                            break;
                        case "tan":
                            ReplaceTokens((float)Math.Tan(double.Parse(tokens[i + 1].value)));
                            break;
                    }
                }
                void ReplaceTokens(float value)
                {
                    tokens.RemoveAt(i);
                    tokens.RemoveAt(i);
                    tokens.Insert(i, new Token(value.ToString(), TokenType.NUMBER));
                }
            }

        }
        #endregion
    }
}

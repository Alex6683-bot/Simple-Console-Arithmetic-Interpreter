using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexerTest.src.Parser
{
    internal class Tokenizer
    {
        private string validOperators = "+-/*^()";
        static string[] Functions =
        {
            "sin", "cos", "tan"
        };
        public List<Token> Tokenize(string _input)
        {
            string input = RefineInput(_input);
            string accumulatedNumbers = "";
            string accumulatedStrings = "";

            List<Token> tokens = new();
            TokenType previousType = TokenType.NONE;
            TokenType currentType = TokenType.NONE;

            foreach (char character in input + " ") { //adding one more character to include all characters ..

                previousType = currentType;

                if (float.TryParse(character.ToString(), out _) || character == '.') { //if current character is a number
                    accumulatedNumbers += character;
                    ClearAccumulatedString(tokens, ref accumulatedStrings); //Tokenizing strings when it come to an end
                    currentType = TokenType.NUMBER;
                }
                else {
                    ClearAccumulatedNumbers(tokens, ref accumulatedNumbers);
                    if (validOperators.Contains(character) || character == ' ') { 
                        Token token;

                        if (character != ' ')
                        {
                            ClearAccumulatedString(tokens, ref accumulatedStrings);
                            token = TokenizeOperators(character);
                            tokens.Add(token);
                        }
                        else
                        {
                            token = TokenizeOperators(character);
                            ClearAccumulatedString(tokens, ref accumulatedStrings);
                        }

                        
                        currentType = token.type; 
                        //Console.WriteLine($"{previousType}, {currentType}");
                    }
                    else
                    {
                        accumulatedStrings += character;
                        currentType = TokenType.STRING;
                    }

                }
            }

            return tokens;
        }


        private Token TokenizeOperators(char input)
        {
            return new Token(input.ToString(), input switch
            { 
                '+' => TokenType.ADD,
                '-' => TokenType.SUB,
                '*' => TokenType.MUL,
                '/' => TokenType.DIV,
                '(' => TokenType.LEFTPAR,
                ')' => TokenType.RIGHTPAR,
                '^' => TokenType.EXP,
                _ => TokenType.STRING,

            });
        }
        private string RefineInput(string input)
        {
            return string.Concat(input.Where(x => !char.IsWhiteSpace(x)));
        }

        private Token ParseNumberValues(string input)
        {
            if (float.TryParse(input, out float result))
            {
                return new Token(result.ToString(), TokenType.NUMBER);
            }
            return new Token(input, TokenType.STRING);
        }

        private void ClearAccumulatedNumbers(List<Token> tokenList, ref string value)
        {
            if (value != "")
            {
                tokenList.Add(ParseNumberValues(value));
                value = "";
            }
        }
        private void ClearAccumulatedString(List<Token> tokenList, ref string value)
        {
            if (value != "")
            {
                //if (Functions.Contains(value.ToLower())) tokenList.Add(new Token(value.ToLower(), TokenType.FUNC)); FUNCTIONS ARE TEMPORARILY COMMENTED DUE TO ISSUES
                tokenList.Add(new Token(value, TokenType.STRING));
                value = "";
            }
        }
    }
}

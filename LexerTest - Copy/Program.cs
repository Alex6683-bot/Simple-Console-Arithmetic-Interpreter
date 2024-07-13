using LexerTest;
using LexerTest.src.Evaluation;
using LexerTest.src.Parser;
using LexerTest.src.Syntax;

namespace Lexer
{
    class Program
    {
        static Interpreter interpreter;
        public static void Main()
        {
            interpreter = new Interpreter();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("LexerTest>");
            Console.ResetColor();
            string input = Console.ReadLine();

            interpreter.Evaluate(input);
            interpreter.DisplayCurrentResult();
            //DisplayTypes(new Tokenizer().Tokenize(input));


            Console.ReadKey(true);
            Console.Clear();
            Main();
        }

        static void DisplayTypes(List<Token> tokens)
        {
            foreach (Token token in tokens)
            {
                Console.Write($"{token.type} : {token.value} \n");
            }
        }
    }
}
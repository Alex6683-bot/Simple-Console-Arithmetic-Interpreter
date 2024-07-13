using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexerTest.src.Parser
{
    struct Error
    {
        public readonly string message;
        Token token;
        
        public Error(string _message, Token _token = null)
        {
            message = _message;
            token = _token;
        }

        public static void DisplayMessage(Error error)
        {
            if (error.message != "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error.message);
                Console.ResetColor();
            }

        }
    }
}

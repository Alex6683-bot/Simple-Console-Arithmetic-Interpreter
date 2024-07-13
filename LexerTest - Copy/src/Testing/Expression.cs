using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexerTest.src.Testing
{
    class Expression
    {
        private string _expression = "";
        private string _answer = "";

        public string expression { get => _expression; set => _expression = value;}
        public string answer { get => _answer; set => _answer = value;}
    }
}

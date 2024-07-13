using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LexerTest.src.Testing
{
    public enum TestingMode
    {
        ShowStatusOnly,
        ShowStatusAndTest
    }
    static class Tester
    {
        public static string delimitter = "->";
        public static TestingMode currentTestingMode = TestingMode.ShowStatusOnly;

        //File Info
        public static string path = @"LexerTest\src\Testing\Test_Expressions.txt";

        private static string[] ReadFromFile(string path)
        {
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                foreach (var line in reader.ReadToEnd().Split('\n')) lines.Add(string.Concat(line.Where(x => !char.IsWhiteSpace(x))));
            }
            return [.. lines];
        }
        /*private static Expression[] ParseExpressions(string[] lines)
        {

        }*/
    }
}

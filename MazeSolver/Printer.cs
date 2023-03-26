using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    internal class Printer
    {
        public Printer() { }

        public void Print(char c)
        {
            Console.Write(c);
        }
        public void PrintLine(string text)
        {
            Console.WriteLine(text);
        }

        public void PrintLines(string[] text) 
        {
            foreach (var line in text)
                Console.WriteLine(line);
        }
    }
}

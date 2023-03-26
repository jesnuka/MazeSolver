using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    internal class FileReader
    {
        // Read a specific text file path and return the lines if a file exists
        public string[] ReadTextFileLines(string filePath)
        {
            string[] fileLines;
            try
            {
                fileLines = File.ReadAllLines(filePath);
                return fileLines;
            }
            catch(FileNotFoundException exception)
            {
                Console.WriteLine("The filepath: '" + filePath + "' is invalid!");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return null;
        }
    }

}

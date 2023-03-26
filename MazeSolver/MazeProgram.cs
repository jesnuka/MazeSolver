using System;
using System.Runtime.InteropServices;

namespace MazeSolver
{
    class MazeProgram
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            FileReader fileReader = new FileReader();
            MapGenerator mapGenerator = new MapGenerator(printer);
            Solver solver = new Solver();
            Map map;

            while (true)
            {
                // Read Player Input
                printer.PrintLine("\nPlease enter the full path of a map text file, with '.txt' included:");
                string? mapPath = Console.ReadLine();

                if (mapPath != null && mapPath.Length > 0)
                {
                    // Read the file at the specified path, if it exists, line by line
                    string[] mapString = fileReader.ReadTextFileLines(mapPath);

                    if(mapString != null && mapString.Length > 0)
                    {
                        // Attempt to create the Map using the string array
                        map = mapGenerator.CreateMap(mapString);
                        if(map != null)
                            break;
                    }
                    else if(mapString == null || mapString.Length == 0)
                        printer.PrintLine("\nMap is empty or invalid!");

                }
            }

            // Draw the Map to the console
            printer.PrintLine("\nMap successfully found:");
            printer.PrintLines(map.GetMapStringArray());

            // Add move limits 20, 150 and 200 to queue, as they are all tested in order
            Queue<int> moveLimits = new Queue<int>();
            moveLimits.Enqueue(20);
            moveLimits.Enqueue(150);
            moveLimits.Enqueue(200);
            Solution solution;

            // Loop through each moveLimits and attempt to solve the map with that moveLimit
            while(moveLimits.Count > 0)
            {
                int moveLimit = moveLimits.Dequeue();
                printer.PrintLine("\nAttempting to solve the map with " + moveLimit + " moves.");
                solution = solver.SolveMap(map, moveLimit);
                if (solution.Solved)
                {
                    printer.PrintLine("\nMap solved with " + solution.MoveAmount + " moves, with " + (moveLimit - solution.MoveAmount) + " moves remaining. The solution path is printed below.");
                    map.AddPath(solution.solutionPath);
                    printer.PrintLines(map.GetMapStringArray());
                    map.ClearAllPaths();
                    break;
                }
                else
                    printer.PrintLine("\nSolution not found within " + moveLimit + " moves.");
            }


            printer.PrintLine("\nApplication finished. Press enter to exit.");
            Console.ReadLine();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    internal class Solution
    {
        public Queue<Tile> solutionPath;
        public int MoveAmount { get; private set; }

        public bool Solved { get; private set; }

        // Stores a solution from start to exit, if it exists
        public Solution(Queue<Tile> newSolution, int moveAmount, bool solved) 
        {
            solutionPath = newSolution;
            MoveAmount = moveAmount;
            Solved = solved;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    internal class MapPosition
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        // Stores the row and column position within the Map
        public MapPosition(int row, int column) 
        {
            Row = row;
            Column = column;
        }

    }
}

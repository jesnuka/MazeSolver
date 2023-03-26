using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    internal class MapGenerator
    {
        Printer _printer;
        public MapGenerator(Printer printer) {
            _printer = printer;
        }

        public Map CreateMap(string[] mapTextLines)
        {
            // Reads through the given string array with lines containing the map information
            // In the map file a ‘#’ represents a block, ‘ ‘ (whitespace) represents movable space
            // band ‘E’ means an exit. ‘^’ is the starting position. Anything else is ignored as whitespace.

            if (mapTextLines == null || mapTextLines.Length == 0 || mapTextLines[0].Length == 0)
                throw new Exception("The map string array is empty.");

            Tile startTile = null;

            // Columns and Rows that are created for the MapGrid
            Tile[][] mapGrid = new Tile[mapTextLines.Length][];
            int y = 0;

            foreach (string line in mapTextLines)
            {

                // Map should have each row be the same length, and each column be the same height
                // The first line is used to check if this applies to each row and column
                if (line.Length != mapTextLines[0].Length)
                {
                    _printer.PrintLine("\nThe row " + y + " is differing length compared to the rest of the map, therefore the map is invalid!");
                    return null;
                }

                int x = 0;
                Tile[] mapRow = new Tile[line.Length];
                foreach(char c in line)
                {
                    MapPosition tilePosition = new MapPosition(y, x);
                    mapRow[x] = new Tile(Tile.CheckTileType(c), tilePosition);
                    if (mapRow[x].Type == Tile.TileType.Start)
                    {
                         // The first found startPosition is used, the rest are turned to whitespace to avoid confusion
                        if(startTile == null)
                            startTile = mapRow[x];
                        else 
                            mapRow[x].ChangeType(Tile.TileType.Empty);
                    }

                    x++;
                }

                mapGrid[y] = mapRow;
                y++;
            }

            // If no start position was found on the map, it is invalid
            if (startTile == null)
            {
                _printer.PrintLine("\nThe map is missing a start position, and is therefore invalid!");
                return null;
            }

            Map map = new Map(mapGrid);
            map.SetStart(startTile);

            return map;
        }
    }
}

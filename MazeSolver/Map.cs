using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    internal class Map
    {
        // The whole map as a grid of Tiles (Y, X)
        public Tile[][] MapGrid { get; private set; }

        public int RowAmount { get; private set; }
        public int ColumnAmount { get; private set; }

        // Only one starting tile can be used per Map
        public Tile StartingTile { get; private set; }

        public Map(Tile[][] map)
        {
            MapGrid = map;
            RowAmount = map.Length;
            if (map[0] != null)
                ColumnAmount = map[0].Length;
            else
                throw new Exception("Map has no columns.");

        }

        public void SetStart(Tile startingTile)
        {
            StartingTile = startingTile;
        }

        // Change tileType of a tile
        private void SetTile(Tile.TileType newType, int row, int column)
        {
            CheckTileValidity(row, column);

            MapGrid[row][column].ChangeType(newType);

        }

        // Add the path to solution if it exists
        public void AddPath(Queue<Tile> solutionPath)
        {
            if (solutionPath != null && solutionPath.Count == 0)
                throw new Exception("Solution queue is empty");

            while(solutionPath.Count > 0)
            {
                Tile tile = solutionPath.Dequeue();
                if(tile.Type == Tile.TileType.Empty)
                    GetTile(tile.MapPosition.Row, tile.MapPosition.Column).ChangeType(Tile.TileType.Path);
            }
        }

        // Removes all Path tiles from the Map, if they exist
        public void RemovePath(int row, int column)
        {
            if (GetTile(row, column).Type == Tile.TileType.Path)
                SetTile(Tile.TileType.Empty, row, column);
        }

        // Remove all paths from the Map if they exist
        public void ClearAllPaths()
        {
            if (MapGrid == null)
                throw new Exception("Map has not been created.");

            for(int y = 0; y < MapGrid.Length; y++)
            {
                for (int x = 0; x < MapGrid[y].Length; x++)
                {
                    RemovePath(y, x);
                }
            }
        }

        // Set the Explored status of all the Tiles on the Map to false
        public void ClearTileExploration()
        {
            if (MapGrid == null)
                throw new Exception("Map has not been created.");

            for (int y = 0; y < MapGrid.Length; y++)
            {
                for (int x = 0; x < MapGrid[y].Length; x++)
                {
                    MapGrid[y][x].SetExplored(false);
                }
            }
        }

        // Check if Row and Column are within MapGrid boundaries
        private bool CheckTileValidity(int row, int column)
        {
            if (MapGrid == null)
                throw new Exception("Map has not been created.");

            if (row >= MapGrid.Length || row < 0 ||
                column >= MapGrid[row].Length || column < 0)
                return false;

            return true;
        }

        // Returns a Tile at Row,Column on the MapGrid if it exists
        public Tile GetTile(int row, int column)
        {
            if (!CheckTileValidity(row, column))
                return null;
            else
                return MapGrid[row][column];
        }

        // Checks if a specific tile allows movement to
        public bool CanMove(int row, int column)
        {
            if (!CheckTileValidity(row, column))
                return false;

            bool canMove = false;
            Tile.TileType type = GetTile(row, column).Type;

            if (type == Tile.TileType.Empty ||
                type == Tile.TileType.Start ||
                type == Tile.TileType.Exit) 
            { 
                canMove = true; 
            }

            return canMove;
        }

        // Return the map as a string array
        public string[] GetMapStringArray()
        {
            string[] mapLines = new string[MapGrid.Length];

            for (int y = 0; y < MapGrid.Length; y++)
            {
                for (int x = 0; x < MapGrid[y].Length; x++)
                {
                    mapLines[y] += Tile.CheckTileChar(MapGrid[y][x].Type);
                }
            }

            return mapLines;
        }
    }
}

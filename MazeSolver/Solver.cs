using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    internal class Solver
    {
        public Solver() { }

        public Solution SolveMap(Map map, int moveLimit)
        {
            // Find the shortest path to a goal, if one exists
            Solution shortestSolution = GetShortestPath(map, moveLimit);

            // Clear the Map of Explored status on Tiles
            map.ClearTileExploration();

            return shortestSolution;
        }

        private Solution GetShortestPath(Map map, int moveLimit)
        {
            // The queue that contains the tile steps that are being checked
            Queue<Tile> pathQueue = new Queue<Tile>(); 

            // Store tiles that connect to each other to later form the final path 
            Dictionary<Tile,Tile> tilePaths = new Dictionary<Tile,Tile>();

            // Distance from starting point is also stored for each Tile to keep within moveLimit
            Dictionary<Tile, int> tileDistances = new Dictionary<Tile, int>();

            // Start by adding the starting tile on the map to the queue,
            // setting the path as null, as starting point is already at the starting point,
            // and also setting the distance to starting point as 0
            pathQueue.Enqueue(map.StartingTile);
            map.StartingTile.SetExplored(true);
            tilePaths[map.StartingTile] = null;
            tileDistances[map.StartingTile] = 0;

            while(pathQueue.Count > 0)
            {
                // Go through the whole queue, adding more as more tiles are discovered using a BFS
                // Perform this until moveLimit is reached, no moves can be taken, or if the exit is found
                    
                // Current tile to look at
                Tile tile = pathQueue.Dequeue();

                // Set the distance for the Tile
                int distance = tileDistances[tile];

                // If movelimit is reached, stop search
                if (distance > moveLimit)
                    return new Solution(null, moveLimit, false);

                // Check if an exit has been reached
                if (tile.Type == Tile.TileType.Exit)
                {
                    // Construct path up to this point using the stored tile paths from tile to tile
                    Queue<Tile> solutionPath = new Queue<Tile>();
                    Tile pathTile = tile;
                    while(pathTile != null)
                    {
                        solutionPath.Enqueue(pathTile);
                        pathTile = tilePaths[pathTile];
                    }

                    // Return the solved path and count, minus the starting tile
                    return new Solution(solutionPath, solutionPath.Count-1, true);
                }

                // Go through each available tile next to this one and add them to the queue for searching
                Tile leftTile = map.GetTile(tile.MapPosition.Row, tile.MapPosition.Column - 1);
                Tile rightTile = map.GetTile(tile.MapPosition.Row, tile.MapPosition.Column + 1);
                Tile upTile = map.GetTile(tile.MapPosition.Row - 1, tile.MapPosition.Column);
                Tile downTile = map.GetTile(tile.MapPosition.Row + 1, tile.MapPosition.Column);

                Queue<Tile> neighborTiles = new Queue<Tile>();
                if(leftTile != null)
                    neighborTiles.Enqueue(leftTile);
                if (rightTile != null)
                    neighborTiles.Enqueue(rightTile);
                if (upTile != null)
                    neighborTiles.Enqueue(upTile);
                if (downTile != null)
                    neighborTiles.Enqueue(downTile);

                // Search each available neighbor of this tile
                while(neighborTiles.Count > 0)
                {
                    Tile neighborTile = neighborTiles.Dequeue();
                    if(CheckTile(neighborTile, map))
                    {
                        // If neighbor exists,
                        // set it as explored,
                        // add it to the queue for searching,
                        // Connect the neighbor to the current tile in the tilePaths,
                        // And set the distance from start of the neighboring tile to distance + 1.
                        neighborTile.SetExplored(true);
                        pathQueue.Enqueue(neighborTile);
                        tilePaths[neighborTile] = tile;
                        tileDistances[neighborTile] = distance + 1;
                    }
                }
            }

            // No solution was found in the map
            return new Solution(null, moveLimit, false);

        }


        // Check if Tile has been visited and if it can be moved to
        private bool CheckTile(Tile tile, Map map)
        {
            if (tile.Explored ||
                !map.CanMove(tile.MapPosition.Row, tile.MapPosition.Column))
                return false;
            else 
                return true;
        }
    }
}

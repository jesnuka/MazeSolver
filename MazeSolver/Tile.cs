using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    internal class Tile
    {
        // Different types of tiles found on the map
        public enum TileType
        {
            Block, // '#'
            Empty, // ' ' (whitespace)
            Exit, // 'E'
            Start, // '^'
            Path // 'X' (Solved path)
        }

        // The type of the Tile on the map
        public TileType Type { get; private set;  }

        // The coordinates of the Tile on the map
        public MapPosition MapPosition { get; private set; }

        // Used during solving so same Tiles are not checked twice
        public bool Explored { get; private set; }

        public Tile(TileType type, MapPosition mapPosition)
        {
            Type = type;
            MapPosition = mapPosition;
            Explored = false;
        }

        public void SetExplored(bool value)
        {
            Explored = value;
        }

        public void ChangeType(TileType newType)
        {
            Type = newType;
        }

        // Check what TileType corresponds with a specific char
        static public TileType CheckTileType(char tileChar)
        {
            switch(tileChar)
            {
                case '#':
                    return TileType.Block;
                case ' ':
                    return TileType.Empty;
                case 'E':
                    return TileType.Exit;
                case '^':
                    return TileType.Start;
                case 'X':
                    return TileType.Path;
                default:
                    return TileType.Empty;
            }
        }

        // Check what char corresponds with a specific TileType
        static public char CheckTileChar(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Block:
                    return '#';
                case TileType.Empty:
                    return ' ';
                case TileType.Exit:
                    return 'E';
                case TileType.Start:
                    return '^';
                case TileType.Path:
                    return 'X';
                default:
                    return ' ';
            }
        }
    }
}

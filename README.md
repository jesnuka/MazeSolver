# MazeSolver
A simple C# Console application that finds the shortest path from start to exit in simple text file mazes.

## File Structure
#### MazeProgram.cs
- Contains the Main function. Starts up the program, creates the required objects for the application, and handles the game loop.
#### Map.cs
- Contains the Map class, which includes reference to all the Tiles, amount of rows and columns, and the starting position on the map, and needed methods.
#### MapPosition.cs
- Used to contain map coordinates on Tiles for easier access.
#### Tile.cs 
- Contains the Tile class, that contains the type of a Tile and the MapPosition coordinates of a tile on the map, as well as an explored flag used during solving, and also some needed methods.
#### Printer.cs
- Contains simple methods for printing strings to the console.
#### MapGenerator.cs
- This class is used to generate a map when given a string array as input. It reads each character and attempts to turn them into Tiles for the map.
#### FileReader.cs
- Handles reading a map path location and generates a string array if the location is valid.
#### Solution.cs
- Used to return a solution to a given map, and contains a path to an exit from the start if the solution was found, with the amount of moves used.
#### Solves.cs
- Contains the algorithm used to solve the maze in the given map by finding the shortest path from a start tile to an exit tile using breadth-first search. The algorithm is given the current move limit and returns a Solution from the exit to the start if one is found within the limit.

## Usage
- The application opens up in the Console, and asks for the path of a text map file as input. Two of these maps are given in the Maps folder.
- The full path needs to be given, with the file extension (.txt) included. 
- The used maps have to be symmetrical to work, and must include a start tile, and should also include an exit tile.
- Once given, the application attempts to find the shortest path from a start position to an exit position by moving 1 tile vertically or horizontally per round.
- The Solver attempts three times, once with a limit of 20 moves, then with 150, and finally with 200 moves. 
- If a solution from start to exit is found, the solution is printed to the console as a path on the map, with no further Solving attempts done.

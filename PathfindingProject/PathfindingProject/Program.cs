using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathfindingProject
{
    class Program
    {
        // Grid that the AI will go through
        static int mazeLength = 10;
        static int mazeHeight = 10;
        static int[,] maze = new int[mazeLength, mazeHeight];
        static int numOfWalls = 10;
        // List of points that the AI has been at
        static List<Point> pastCoordinates = new List<Point>();

        // Mazes given by the teacher to test the Ai
        static int[,] testMaze1 = {
            { 0 , 3 , 0 , 1 , 0 , 1 , 0 , 0 , 1 , 1 },
            { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 1 },
            { 0 , 0 , 1 , 0 , 1 , 0 , 0 , 0 , 0 , 0 },
            { 0 , 1 , 1 , 1 , 1 , 0 , 1 , 1 , 0 , 0 },
            { 0 , 1 , 1 , 1 , 1 , 0 , 1 , 1 , 0 , 0 },
            { 0 , 1 , 1 , 0 , 1 , 1 , 1 , 1 , 1 , 0 },
            { 0 , 1 , 0 , 1 , 1 , 1 , 1 , 1 , 1 , 0 },
            { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
            { 0 , 0 , 0 , 1 , 0 , 0 , 0 , 0 , 1 , 0 },
            { 0 , 0 , 0 , 1 , 0 , 0 , 0 , 4 , 0 , 0 },
        };

        static int[,] testMaze2 = {
            { 0 , 0 , 0 , 0 , 0 , 1 , 1 , 1 , 0 , 4 },
            { 0 , 0 , 1 , 1 , 0 , 0 , 1 , 0 , 0 , 1 },
            { 0 , 1 , 0 , 1 , 1 , 0 , 1 , 1 , 0 , 1 },
            { 0 , 0 , 1 , 0 , 0 , 0 , 0 , 1 , 0 , 0 },
            { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 1 , 1 , 0 },
            { 0 , 0 , 1 , 0 , 0 , 0 , 0 , 1 , 0 , 0 },
            { 0 , 0 , 1 , 1 , 0 , 1 , 0 , 0 , 0 , 0 },
            { 1 , 0 , 0 , 1 , 1 , 0 , 0 , 1 , 0 , 0 },
            { 1 , 0 , 1 , 1 , 0 , 0 , 0 , 0 , 0 , 1 },
            { 3 , 0 , 1 , 1 , 0 , 0 , 1 , 0 , 0 , 1 },
        };


      

        static void Main(string[] args)
        {
            FillMaze();
            PrintMaze(testMaze1);
            RunMaze(testMaze1);

            Console.WriteLine("\nPress any key to continue ...");
            Console.ReadKey();
        }

        // Algorithm that randomly fills in blocks for the maze (0 = clear, 1 = wall, 3 = start, 4 = finish)
        static void FillMaze()
        {
            for (int i = 0; i < mazeLength; i++)
            {
                for (int j = 0; j < mazeHeight; j++)
                {
                    //if (GetRandomInt() < 2 && numOfWalls > 0)
                    //{
                    //    maze[i, j] = 1;
                    //    numOfWalls--;
                    //}
                    //else
                    //{
                    //    maze[i, j] = 0;
                    //}
                    maze[i, j] = 0;
                }
            }
            // marks end point of maze
            maze[mazeLength - 1, mazeHeight - 1] = 4;
        }

        static int GetRandomInt()
        {
            Random random = new Random();
            int i = random.Next(1, 10);
            return i;
        }

        static void RunMaze(int[,] _maze)
        {
            bool cantMove = false;

            // Required to see if the Ai is retracing stepsw
            bool lastMoveDown = false;
            bool lastMoveRight = false;

            int x = 0;
            int y = 1;

            // Keeps looking for next point to move to
            while (cantMove == false)
            {
                // Visually shows where the Ai has been
                _maze[x, y] = 8;

                //Checks if there the Ai can move down
                if (y < mazeHeight - 1 && _maze[x, y + 1] != 1 && _maze[x, y + 1] != 9)
                {
                    y++;
                    // adds the new coordinates to the List
                    pastCoordinates.Add(new Point(x, y));
                    // Checks if the AI is at the end
                    cantMove = IsFinished(x, y, _maze);
                    lastMoveDown = true;
                    lastMoveRight = false;
                }
                // Checks if the Ai can move right
                else if (x < mazeLength - 1 && maze[x + 1, y] != 1)
                {
                    x++;
                    pastCoordinates.Add(new Point(x, y));
                    cantMove = IsFinished(x, y, _maze);
                    lastMoveDown = false;
                    lastMoveRight = true;
                }
                // Allows the AI to go up
                else if (y > 0 && _maze[x, y - 1] != 1)
                {
                    if (lastMoveDown)
                    {
                        _maze[x, y] = 9;
                        y--;
                        // Removes the last step it took
                        pastCoordinates.RemoveAt(pastCoordinates.Count - 1);
                    }
                    else
                    {
                        y--;
                        pastCoordinates.Add(new Point(x, y));
                    }
                    cantMove = IsFinished(x, y, _maze);
                    lastMoveRight = false;
                    lastMoveDown = false;
                }
                // Allows the AI to go left
                else if (x > 0 && _maze[x - 1, y] != 1)
                {
                    if (lastMoveRight)
                    {
                        maze[x, y] = 9;
                        x--;
                        pastCoordinates.RemoveAt(pastCoordinates.Count - 1);
                    }
                    else
                    {
                        x--;
                        pastCoordinates.Add(new Point(x, y));
                    }
                    cantMove = IsFinished(x, y, _maze);
                    lastMoveRight = false;
                    lastMoveDown = false;
                }
            }

        }

        // Checks if Ai is at the end of the maze
        static bool IsFinished(int _x, int _y, int[,] _maze)
        {
            if (_maze[_x, _y] == 4)
            {
                Console.Clear();
                PrintMaze(_maze);
                Console.Write("Ai's Path: ");
                for (int i = 0; i < pastCoordinates.Count; i++)
                {
                    Console.Write(pastCoordinates[i] + " ");
                }
                Console.WriteLine("\nEnd Reached. Ai reached the end in " + pastCoordinates.Count + " moves");
                return true;
            }
            else
            {
                Console.Clear();
                PrintMaze(_maze);
                Console.WriteLine("\nNot there yet. Press any key to continue to next step ...\n");
                Console.ReadKey();
                return false;
            }
        }

        // Not required but easier to visualize
        static void PrintMaze(int[,] _maze)
        {
            Console.WriteLine("'0' Empty Space\n'1' Wall\n'4' Goal\n'8' Past Coordinate of Ai\n'9' Dead End\n\n");

            // Constructs Board
            int n = 0;
            Console.Write("  ");
            // Prints the x axis number line
            for (int i = 1; i < mazeLength + 1; i++)
            {
                Console.Write("  " + i);
            }
            // Prints the rest of the board
            for (int i = 0; i < mazeHeight; i++)
            {
                n++;
                Console.Write("\n" + n + " ");
                Console.Write("|");
                ColorCoding(0, i, _maze);
                Console.Write("|");
                for (int j = 1; j < mazeLength; j++)
                {
                    Console.Write("|");
                    ColorCoding(j, i, _maze);
                    Console.Write("|");
                }
            }
            Console.WriteLine();
        }

        static void ColorCoding(int _x, int _y, int[,] _maze)
        {
            if (_maze[_x, _y] == 1)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(_maze[_x, _y]);
                Console.ResetColor();
            }
            else if (_maze[_x, _y] == 3 || maze[_x, _y] == 4)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(_maze[_x, _y]);
                Console.ResetColor();
            }
            else if (_maze[_x, _y] == 8)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(_maze[_x, _y]);
                Console.ResetColor();
            }
            else if (_maze[_x, _y] == 9)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(_maze[_x, _y]);
                Console.ResetColor();
            }
            else
            {
                Console.Write(_maze[_x, _y]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathfindingProject
{
    class Program
    {
        // number of moves to easily compare which path is more efficient
        int numOfMoves = 0;

        // Grid that the AI will go through
        static int mazeLength = 10;
        static int mazeHeight = 10;
        static int[,] maze = new int[mazeLength, mazeHeight];
        static Point currentPoint = new Point(0,0);
        // List of points that the AI has been at
        static List<Point> pastCoordinates = new List<Point>();

        static void Main(string[] args)
        {
            //FillMaze();
            PrintMaze();

            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        // Algorithm that randomly fills in blocks for the maze (0 = clear, 1 = wall, 3 = start, 4 = finish)
        static void FillMaze()
        {
            for (int i = 0; i < mazeLength; i++)
            {
                for (int j = 0; j < mazeHeight; j++)
                {
                    maze[i,j] = 0;
                }
            }
            // marks start and end point of maze
            maze[0, 0] = 3;
            maze[mazeLength, mazeHeight] = 4;
        }

        static void RunMaze()
        {
            bool canMove = true;
            int x = 0;
            int y = 0;
            
            // Keeps looking for next point to move to
            while (canMove)
            {
                currentPoint.X = x;
                currentPoint.Y = y;

                // Checks if there is a wall, if not moves AI to that point
                if (y < mazeHeight && y+1 != 1)
                {
                    y++;
                    currentPoint.Y = y;
                    // adds the new coordinates to the List
                    pastCoordinates.Add(new Point(x, y));
                    // Checks if the AI is at the end
                    canMove = IsFinished(x, y);
                }
                if (x < mazeLength && y+1 != 1)
                {
                    x++;
                    currentPoint.X = x;
                    pastCoordinates.Add(new Point(x, y));
                    canMove = IsFinished(x, y);
                }                
            }

        }

        // Checks if Ai is at the end of the maze
        static bool IsFinished(int _x, int _y)
        {
            if (maze[_x,_y] == 4)
            {
                Console.WriteLine("End Reached");
                return false;
            }
            else
            {
                return true;
            }
        }

        // Not required but easier to visualize
        static void PrintMaze()
        {
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
                Console.Write("|" + maze[0, i] + "|");
                for (int j = 1; j < mazeLength; j++)
                {
                    Console.Write("|" + maze[j, i] + "|");
                }
            }
            Console.WriteLine();
        }
    }
}

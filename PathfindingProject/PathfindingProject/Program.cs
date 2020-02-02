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
        // List of points that the AI has been at
        List<Point> pastCoordinates = new List<Point>();

        static void Main(string[] args)
        {
            // Algorithm that randomly fills in blocks for the maze (0 = clear, 1 = wall, 3 = start, 4 = finish)
            FillMaze();
            PrintMaze();

            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        static void FillMaze()
        {
            for (int i = 0; i < mazeLength; i++)
            {
                for (int j = 0; j < mazeHeight; j++)
                {
                    maze[i,j] = 0;
                }
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

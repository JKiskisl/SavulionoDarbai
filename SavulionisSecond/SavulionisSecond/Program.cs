
//bfs algo

using System;
using System.Collections.Generic;

namespace SavulionisSecond
{
    class Program
    {
        static int[,] board = new int[10, 10];
        static int solutions = 0;

        static void Main(string[] args)
        {
            PlaceQueens();
            Console.WriteLine("Found " + solutions + " solutions.");
        }

        static void PlaceQueens()
        {
            Queue<int[]> stateQueue = new Queue<int[]>();
            int[] initialState = new int[10];
            stateQueue.Enqueue(initialState);

            while (stateQueue.Count>0)
            {
                int[] state = stateQueue.Dequeue();
                int row = 0;

                while (row < 10 && state[row] != 0)
                {
                    row++;
                }
                if (row==10)
                {
                    solutions++;
                    PrintSolution(state);
                    continue;
                }

                for (int col = 0; col < 10; col++)
                {
                    if (IsValid(state, row, col))
                    {
                        int[] newState = new int[10];
                        Array.Copy(state, newState, state.Length);
                        newState[row] = col + 1;
                        stateQueue.Enqueue(newState);
                    }
                }
            }
        }

        static bool IsValid(int[] state, int row, int col)
        {
            for (int i = 0; i < row; i++)
            {
                int qRow = i;
                int qCol = state[i] - 1;

                if (qCol == col || qRow- qCol == row - col || qRow + qCol == row + col)
                {
                    return false;
                }
            }
            return true;
        }

        static void PrintSolution(int[] state)
        {
            Console.WriteLine("Solution: ");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (state[i] - 1 == j)
                    {
                        Console.Write("Q ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
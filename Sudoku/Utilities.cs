using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Sudoku
{
    // Utilities class for all the utilities variables and functions in the project
    public static class Utilities
    {
   
        public static int maxCellValue;
        
        public const int minCellValue = 1;

        // List of all legal sizes that a board can be
        public static readonly List<int> Legal_Sizes = new List<int>()
        {(int)Math.Pow(1,2),(int)Math.Pow(4,2),(int)Math.Pow(9,2),(int)Math.Pow(16,2),(int)Math.Pow(25,2)};

        // utility function that print the board and call for the solver
        private static bool ShowSolutionHelper(SudokuBoard board)
        {
            Console.WriteLine("The sudoku puzzle is: ");
            board.PrintBoard();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Here is the solution for this sudoku puzzle");

            // Running time measurement
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            bool solved = false; // check if the puzzle solved
            if (IsSolvable(board))
            {
                solved = true;
                stopwatch.Stop();
                board.PrintBoard();
                // print running time    
                Console.WriteLine("Time: " + stopwatch.ElapsedMilliseconds + " ms");
            }
            else
                Console.WriteLine("The puzzle cannot be solved");
            return solved;
        }
        // Check if the puzzle is solveable
        private static bool IsSolvable(SudokuBoard board)
        {
            if (SudokuSolver.SolveSudoku(board))
                return true;
            else
                return false;
        }

        // Solve the given sudoku and print it to the user
        public static void ShowSolution(string data)
        {
            try
            {
                SudokuBoard board = new SudokuBoard(data);
                ShowSolutionHelper(board);
            }
            catch (NotLegalStringException nlt)
            {
                Console.WriteLine(nlt.Message);
            }
            catch (NotLegalDataSizeException nlds)
            {
                Console.WriteLine(nlds.Message);
            }
        }

        // Solve the given sudoku and print it to the user and write it in the given file
        public static void ShowSolution(string data, string path)
        {
            try
            {
                SudokuBoard board = new SudokuBoard(data);
                if(ShowSolutionHelper(board))
                    WriteSolutionToFile(board, path); // if the puzzle is solvable write it solution the the file
            }
            catch (NotLegalStringException nlt)
            {
                Console.WriteLine(nlt.Message);
            }
            catch (NotLegalDataSizeException nlds)
            {
                Console.WriteLine(nlds.Message);
            }
        }


        // Write the solution string of the board to soultion text file
        private static void WriteSolutionToFile(SudokuBoard board,string path)
        {
            string solution = board.ConvertBoardToString(); // get solution string from the board
            File.WriteAllText(path, solution, Encoding.UTF8); // write the solution to the text file

        }
    }
}

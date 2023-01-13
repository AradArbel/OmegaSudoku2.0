using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class UserMenu
    {
        // Handle manu for the user in which he can enter many sudoku puzzles in a row
        public static void Menu()
        {
            // Infinity loop that ends only when the user decide to quit
            while (true)
            {
                // Main menu
                Console.WriteLine("Sudoku Menu:");
                Console.WriteLine("1. Enter a new puzzle");
                Console.WriteLine("2. Quit");


                Console.Write("Enter your selection: ");
                string selection = Console.ReadLine();

                switch (selection)
                {
                    // New puzzle
                    case "1":
                        Console.WriteLine("1. To enter a new puzzzle from the console choose 1");
                        Console.WriteLine("2. To enter a new puzzzle from a text file choose 2");
                        Console.Write("Enter your selection: ");
                        selection = Console.ReadLine();
                        EnterMethod(selection);

                        break;
                    // EXIT
                    case "2":
                        Console.WriteLine("Bye bye");
                        return;

                    default:
                        Console.WriteLine("Invalid selection. Please select again \n");
                        break;
                }
            }

        }
        // Sub menu that handle the user choise between console or text file
        private static void EnterMethod(string selection)
        {
            string puzzleData;
            switch (selection)
            {
                // Enter a new puzzle from the console
                case "1":
                    Console.WriteLine("Enter the puzzle");
                    puzzleData = Console.ReadLine();
                    //Utilities.boardSize = puzzleData.Length;
                    Utilities.maxCellValue = (int)Math.Sqrt(puzzleData.Length);
                    ShowSolution(puzzleData);
                    break;
                // Enter a new puzzle from a text file
                case "2":
                    Console.Write("Enter the name of the text file: ");
                    string filename = Console.ReadLine();
                    try
                    {
                        puzzleData = File.ReadAllText(filename);
                        ShowSolution(puzzleData);
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("Error: The file was not found.");
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.WriteLine("Error: The directory was not found.");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("Error: You do not have permission to access the file.");
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Error: An I/O error occurred while reading the file.");
                    }
                    break;


                default:
                    Console.WriteLine("Invalid selection. \n Please select again \n");

                    break;
            }
        }

        // Solve the given sudoku and print it to the user
        private static void ShowSolution(string data)
        {
            try
            {
                SudokuBoard board = new SudokuBoard(data);
                Console.WriteLine("The sudoku puzzle is: ");
                board.PrintBoard();
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Here is the solution for this sudoku puzzle");

                // Running time measurement
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                if (SudokuSolver.SolveSudoku(board))
                {
                    board.PrintBoard();
                    // print running time
                    stopwatch.Stop();
                    Console.WriteLine("Time: " + stopwatch.ElapsedMilliseconds + " ms");
                }

                // In case that the sudoku in unsolveable
                else
                    Console.WriteLine("The puzzle cannot be solved");
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
    }
}

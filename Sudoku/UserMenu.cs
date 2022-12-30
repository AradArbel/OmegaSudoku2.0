using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class UserMenu
    {
        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Sudoku Menu:");
                Console.WriteLine("1. Enter a new puzzle");
                Console.WriteLine("2. Quit");


                Console.Write("Enter your selection: ");
                int selection = int.Parse(Console.ReadLine());

                switch (selection)
                {
                    case 1:
                        Console.WriteLine("1. To enter a new puzzzle from the console choose 1");
                        Console.WriteLine("2. To enter a new puzzzle from a text file choose 2");
                        selection = int.Parse(Console.ReadLine());

                        switch (selection)
                        {
                            // Enter a new puzzle from the console
                            case 1:
                                Console.WriteLine("Enter the puzzle");
                                string puzzleData = Console.ReadLine();
                                break;
                            // Enter a new puzzle from a text file
                            case 2:
                                Console.Write("Enter the name of the text file: ");
                                string filename = Console.ReadLine();
                                int[,] puzzle = new int[9, 9];
                                using (StreamReader reader = new StreamReader(filename))
                                {
                                    for (int i = 0; i < 9; i++)
                                    {
                                        string line = reader.ReadLine();
                                        string[] numbers = line.Split(' ');
                                        for (int j = 0; j < 9; j++)
                                        {
                                            puzzle[i, j] = int.Parse(numbers[j]);
                                        }
                                    }
                                }
                                break;

                            default:
                                SudokuBoard board = new SudokuBoard(puzzleData);
                                Console.WriteLine("The sudoku puzzle is: ");
                                board.PrintBoard();
                                Console.WriteLine("----------------------------------------");
                                Console.WriteLine("Here is the solution for this sudoku puzzle");
                                SudokuSolver.solveSudoku(board);
                                board.PrintBoard();
                                break;




                        }

                        break;
                    case 2:
                        return;

                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }

        }
    }
}

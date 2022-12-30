using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class Program
    {
        public static void Main()
        {
            SudokuBoard b = new SudokuBoard("831529600096810250540630180159083020483090715027045038365071890274958301000362547");
            b.PrintBoard();
            SudokuSolver.solveSudoku(b);
            Console.WriteLine("----------------------------------------");
            b.PrintBoard();
        }
    }
}

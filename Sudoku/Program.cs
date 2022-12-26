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
            SudokuBoard b = new SudokuBoard("1003040101002010");
            b.PrintBoard();
            SudokuSolver.solveSudoku(b, 0, 0);
            b.PrintBoard();
        }
    }
}

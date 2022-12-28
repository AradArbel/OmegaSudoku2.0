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
            SudokuBoard b = new SudokuBoard("800000070006010053040600000000080400003000700020005038000000800004050061900002000\r\n100000027000304015500170683430962001900007256006810000040600030012043500058001000");
            b.PrintBoard();
            SudokuSolver.solveSudoku(b, 0, 0);
            b.PrintBoard();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    static class Constants
    {
        // Constants class for all the constants variables in the project
        public const int maxCellValue = 4;
        public const int minCellValue = 1;

        public const int boardRange = maxCellValue - 1;

        public static readonly int boxRange = (int)Math.Sqrt(maxCellValue);

        // List of all legal items that can be a value in a cell
        public static readonly ArrayList Legal_Items = new ArrayList()
        {"0","1","2","3","4","5","6","7","8","9"};
    }
}

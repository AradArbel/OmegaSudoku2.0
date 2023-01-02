using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class Utilities
    {
        // Utilities class for all the constants variables in the project

        //public static int boardSize;

        public static int maxCellValue;
        
        public const int minCellValue = 1;

        //public static int boardRange = maxCellValue - 1;

        //public static int boxRange = (int)Math.Sqrt(maxCellValue);

        // List of all legal sizes that a board can be
        public static readonly List<int> Legal_Sizes = new List<int>()
        {(int)Math.Pow(1,2),(int)Math.Pow(4,2),(int)Math.Pow(9,2),(int)Math.Pow(16,2),(int)Math.Pow(25,2)};
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    // Representation of a Sudoku board
    public class SudokuBoard
    {
        private SudokuCell[,] board;

       /// Constructor for this class.
        public SudokuBoard()
        {
            this.board = new SudokuCell[Constants.maxCellValue, Constants.maxCellValue];
            // Initialize the Sudoku table
            for (int row = 0; row < Constants.maxCellValue; row++)
            {
                for (int col = 0; col < Constants.maxCellValue; col++)
                {
                    this.board[row, col] = new SudokuCell(row, col);
                }

            }
        }

        /// Get sudoku board
        public SudokuCell[,] Board
        { get { return this.board; } }
    }
}

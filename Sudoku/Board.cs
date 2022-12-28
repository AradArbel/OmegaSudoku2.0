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
        public SudokuBoard(string boardData)
        {
            if (IsLegalBoard(boardData))
            {
                this.board = new SudokuCell[Constants.maxCellValue, Constants.maxCellValue];
                // Initialize the Sudoku table
                for (int row = 0; row < Constants.maxCellValue; row++)
                {
                    for (int col = 0; col < Constants.maxCellValue; col++)
                    {
                        this.board[row, col] = new SudokuCell(row, col, (int)boardData[(row * Constants.maxCellValue) + col] - '0');
                    }

                }
            }
             // todo add try catch notLegalStringException
           

            
        }

        /// Get sudoku board
        public SudokuCell[,] Board
        { get { return this.board; } }

        // Check if the board is full (None 0 values)
        public bool IsFull()
        {
            for (int row = 0; row < Constants.boardRange; row++)
                for (int col = 0; col < Constants.boardRange; col++)
                    if (this.Board[row, col].Value == 0) //marked with 0 is empty
                        return true;
            return false;
        }

        // Check legal string of the board output
        private bool IsLegalBoard(String boardData)
        {
            // Check for every char in the string if it is in the legal items list. if not return false.
            foreach (char value in boardData)
                if (!Constants.Legal_Items.Contains(value))
                    return false;
            return true;
        }

        // Print board
        public void PrintBoard()
        {
            for (int row = 0; row < Constants.maxCellValue; row++)
            {
                for (int col = 0; col < Constants.maxCellValue; col++)
                {
                    Console.Write(this.board[row, col].Value.ToString() + " ");
                }
                Console.WriteLine("");    
            }
        }

    }
}

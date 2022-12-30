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
            // check if the string that been put is legal
            IsLegalBoard(boardData);
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

        /// Get sudoku board
        public SudokuCell[,] Board
        {
            get
            {
                try
                {
                    return this.board;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Error: The board has not been initialized.");
                    return null;
                }
            }
        }

        // Check if the board is full (None 0 values)
        public bool IsFull()
        {
            try
            {
                for (int row = 0; row < Constants.boardRange; row++)
                    for (int col = 0; col < Constants.boardRange; col++)
                        if (this.Board[row, col].Value == 0) //marked with 0 is empty
                            return false;
                return true;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Error: One or more cells in the board have not been initialized.");
                return false;
            }
        }

        // Check legal string of the board output
        private void IsLegalBoard(String boardData)
        {
            // Check for every char in the string if it is in the legal items list.
            foreach (char value in boardData)
            {
                if (!Constants.Legal_Items.Contains(value))
                    throw new NotLegalStringException("Error: The board data string has ilegal char in his string data.\n");
            }
            // Check if the amount of values is equal to the board size
            if (boardData.Length != Constants.maxCellValue * Constants.maxCellValue)
                throw new NotLegalDataSizeException("Error: The board size is ilegal. \n");
        }

        // Print board
        public void PrintBoard()
        {
            for (int row = 0; row < Constants.maxCellValue; row++)
            {
                for (int col = 0; col < Constants.maxCellValue; col++)
                {
                    Console.Write(board[row, col].Value + " ");
                }
                Console.WriteLine("");    
            }
        }

    }
}

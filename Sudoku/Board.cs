using System;
using System.Collections;
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
            IsLegalBoardString(boardData);
            this.board = new SudokuCell[Utilities.maxCellValue, Utilities.maxCellValue];
            // Initialize the Sudoku table
            for (int row = 0; row < Utilities.maxCellValue; row++)
            {
                for (int col = 0; col < Utilities.maxCellValue; col++)
                {
                    this.board[row, col] = new SudokuCell(row, col, (int)boardData[(row * Utilities.maxCellValue) + col] - '0');
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

        // Get size of the board
        public int Size { get => Size; }

        // Check if the board is full (None 0 values)
        public bool IsFull()
        {
            try
            {
                for (int row = 0; row < Utilities.maxCellValue - 1; row++)
                    for (int col = 0; col < Utilities.maxCellValue - 1; col++)
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

        // List of all legal items that can be a value in a cell
        public static ArrayList Legal_Items()
        {
            ArrayList legal_items = new ArrayList();
            legal_items.Add('0');
            for (int value = Utilities.minCellValue; value <= Utilities.maxCellValue; value++)
            {
                char validChar = (char)value;
                validChar += '0';
                legal_items.Add(validChar);
            }
            return legal_items;
        }

        // Check legal string of the board output
        private void IsLegalBoardString(String boardData)
        {
            // Check if the amount of values is equal to the board size
            if (!Utilities.Legal_Sizes.Contains(boardData.Length))
                throw new NotLegalDataSizeException("Error: The board size is ilegal.\n Please insert a board whose size is one of the following sizes: 1X1, 4X4, 9X9, 16X16, 25X25");

            // Check for every char in the string if it is in the legal items list.
            foreach (char value in boardData)
            {
                if (!Legal_Items().Contains(value))
                    throw new NotLegalStringException("Error: The board data string has ilegal char in his string data.\n");
            }

        }

        // Print board
        public void PrintBoard()
        {
            for (int row = 0; row < Utilities.maxCellValue; row++)
            {
                for (int col = 0; col < Utilities.maxCellValue; col++)
                {
                    Console.Write(board[row, col].Value + " ");
                }
                Console.WriteLine("");
            }
        }

    }
}
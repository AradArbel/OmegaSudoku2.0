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

        /// Get and set sudoku board
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
            //set the board field to the new board
            set { this.board = value; }
        }

        // Get size of the board
        public int Size { get => Size; }

        //Convert Board To String Method
        public string ConvertBoardToString()
        {
            //create a new StringBuilder object
            StringBuilder boardData = new StringBuilder();
            //loop through the rows and columns of the board
            for (int row = 0; row < Utilities.maxCellValue; row++)
            {
                for (int col = 0; col < Utilities.maxCellValue; col++)
                {
                    //append the value of each cell to the StringBuilder object
                    boardData.Append(this.board[row, col].Value);
                }
            }
            //return the string representation of the board
            return boardData.ToString();
        }

        public void UpdateBoardData(string data)
        {
            for (int row = 0; row < Utilities.maxCellValue; row++)
            {
                for (int col = 0; col < Utilities.maxCellValue; col++)
                {
                    this.board[row, col] = new SudokuCell(row, col, (int)data[(row * Utilities.maxCellValue) + col] - '0');
                }
            }
        }

        public void restoreBoad()
        {

        }

        //Clone Method
        public SudokuBoard Clone()
        {
            // Copy the current string data
            string currentBoardString = ConvertBoardToString();
            //create a new instance of the SudokuBoard class
            SudokuBoard clone = new(currentBoardString);
            //loop through the rows and columns of the board
            for (int row = 0; row < Utilities.maxCellValue; row++)
            {
                for (int col = 0; col < Utilities.maxCellValue; col++)
                {
                    //copy the values of each cell from the current board to the new board
                    clone.board[row, col] = this.board[row, col];
                }
            }
            //return the cloned board
            return clone;
        }


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
                throw new NotLegalDataSizeException("Error: The board size is ilegal. Please insert a board whose size is one of the following sizes: 1X1, 4X4, 9X9, 16X16, 25X25");

            // Check for every char in the string if it is in the legal items list.
            foreach (char value in boardData)
            {
                if (!Legal_Items().Contains(value))
                    throw new NotLegalStringException("Error: The board data string has ilegal char in his string data.");
            }

        }

        // Print board
        public void PrintBoard()
        {
            //Define box range
            int boxRange = (int)(Math.Sqrt(Utilities.maxCellValue));

            // Itrate throw every cell in the board and print is value;
            for (int row = 0; row < Utilities.maxCellValue; row++)
            {
                for (int col = 0; col < Utilities.maxCellValue; col++)
                {
                    Console.Write(board[row, col].Value + " ");
                    if ((col + 1) % boxRange == 0)
                    {
                        Console.Write("| ");
                    }
                }
                // Separation between boxes to make the grid more readable 
                Console.WriteLine();
                if ((row + 1) % boxRange == 0)
                {
                    for (int spaceCounter = 0; spaceCounter < Utilities.maxCellValue + boxRange - 1; spaceCounter++)
                    {
                        Console.Write("--");
                    }
                    Console.WriteLine();
                }
            }
        }


    }
}
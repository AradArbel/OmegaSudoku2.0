using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Sudoku
{
    static class ConstraintPropagation
    {
        // Look for cells that have the same set of possible values,
        // and eliminate those values from the list of potential values for other cells in the same row

        // Board range according to it size
        public static int boardRange = Utilities.maxCellValue - 1;
        static bool NakedPairRow(SudokuBoard board)
        {
            bool flag = false; // Check if naked pair has been found
            // Iterate through the rows of the puzzle
            for (int row = 0; row < boardRange; row++)
            {
                // Iterate through the columns of the puzzle
                for (int col = 0; col < boardRange; col++)
                {
                    // Get the current cell
                    SudokuCell cell = board.Board[row, col];

                    // Skip cells that have already been assigned a value
                    if (board.Board[row, col].Value != 0)
                        continue;

                    // Iterate through the remaining cells in the row
                    for (int currentCol = col + 1; currentCol < boardRange; currentCol++)
                    {
                        // Get the other cell in the row
                        SudokuCell otherCell = board.Board[row, currentCol];

                        // Skip cells that have already been assigned a value
                        if (board.Board[row, currentCol].Value != 0)
                            continue;

                        // Check if the two cells have the same set of possible values
                        if (cell.PossibleValues.SequenceEqual(otherCell.PossibleValues) && cell.PossibleValues.Count == 2 && otherCell.PossibleValues.Count == 2)
                        {
                            // The cells have the same set of possible values, so eliminate those values from the list of potential values for all other cells in the row
                            for (int checkCol = 0; checkCol < boardRange; checkCol++)
                            {
                                if (checkCol != cell.Col && checkCol != otherCell.Col) // Not one of the cells that found to have the same set of 2 possible value
                                {
                                    board.Board[row, checkCol].PossibleValues.RemoveAll(val => cell.PossibleValues.Contains(val));
                                    flag = true;
                                }

                            }
                        }
                    }
                }
            }
            return flag;
        }

        // Look for cells that have the same set of possible values,
        // and eliminate those values from the list of potential values for other cells in the same col
        static bool NakedPairCol(SudokuBoard board)
        {
            bool flag = false; // Check if naked pair has been found
            for (int col = 0; col < boardRange; col++)
            {
                // Iterate through the rows of the puzzle
                for (int row = 0; row < boardRange; row++)
                {
                    // Get the current cell
                    SudokuCell cell = board.Board[row, col];

                    // Skip cells that have already been assigned a value
                    if (board.Board[row, col].Value != 0)
                        continue;

                    // Iterate through the remaining cells in the column
                    for (int currentRow = row + 1; currentRow < boardRange; currentRow++)
                    {
                        // Get the other cell in the column
                        SudokuCell otherCell = board.Board[currentRow, col];

                        // Skip cells that have already been assigned a value
                        if (board.Board[currentRow, col].Value != 0)
                            continue;

                        // Check if the two cells have the same set of possible values
                        if (cell.PossibleValues.SequenceEqual(otherCell.PossibleValues) && cell.PossibleValues.Count == 2 && otherCell.PossibleValues.Count == 2)
                        {
                            // The cells have the same set of possible values, so eliminate those values from the list of potential values for all other cells in the column
                            for (int checkRow = 0; checkRow < boardRange; checkRow++)
                            {
                                if (checkRow != cell.Row && checkRow != otherCell.Row) // Not one of the cells that found to have the same set of 2 possible value
                                {
                                    board.Board[checkRow, col].PossibleValues.RemoveAll(val => cell.PossibleValues.Contains(val));
                                    flag = true;
                                }
                            }
                        }
                    }
                }
            }
            return flag;
        }

        // Look for cells that have the same set of possible values,
        // and eliminate those values from the list of potential values for other cells in the same box
        public static void NakedPairBox(SudokuBoard board)
        {
            // Calculate box range
            int boxRange = (int)Math.Sqrt(Utilities.maxCellValue);

            // Iterate through the regions of the puzzle
            for (int box = 0; box < boardRange; box++)
            {
                // Calculate the starting row and column for the box
                int startRow = (box / boxRange) * boxRange;
                int startCol = (box % boxRange) * boxRange;

                // Iterate through the cells in the box
                for (int row = startRow; row < startRow + boxRange; row++)
                {
                    for (int col = startCol; col < startCol + boxRange; col++)
                    {
                        // Get the current cell
                        SudokuCell cell = board.Board[row, col];

                        // Skip cells that have already been assigned a value
                        if (board.Board[row, col].Value != 0)
                            continue;

                        // Iterate through the remaining cells in the box
                        for (int currentRow = row; currentRow < startRow + boxRange; currentRow++)
                        {
                            for (int currentCol = col + 1; currentCol < startCol + boxRange; currentCol++)
                            {
                                // Get the other cell in the box
                                SudokuCell otherCell = board.Board[currentRow, currentCol];

                                // Skip cells that have already been assigned a value
                                if (board.Board[currentRow, currentCol].Value != 0)
                                    continue;

                                // Check if the two cells have the same set of possible values
                                if (cell.PossibleValues.SequenceEqual(otherCell.PossibleValues))
                                {
                                    // The cells have the same set of possible values, so eliminate those values from the list of potential values for all other cells in the box
                                    for (int otherRow = startRow; otherRow < startRow + boxRange; otherRow++)
                                    {
                                        for (int otherCol = startCol; otherCol < startCol + boxRange; otherCol++)
                                        {
                                            if (otherRow != cell.Row && otherCol != cell.Col && otherRow != otherCell.Row && otherCol != otherCell.Col) // Not one of the cells that found to have the same set of 2 possible value
                                            {
                                                board.Board[otherRow, otherCol].PossibleValues.RemoveAll(val => cell.PossibleValues.Contains(val));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        


        //iteratively apply constraint propagation techniques until puzzle is fully deduced
        public static bool DeducePuzzle(SudokuBoard board)
        {
            bool isDeduced = false;
            // continue iterating until puzzle is fully deduced
            while (!isDeduced)
            {
                isDeduced = true;
                // iterate through all cells in the board
                for (int row = 0; row < Utilities.maxCellValue; row++)
                {
                    for (int col = 0; col < Utilities.maxCellValue; col++)
                    {
                        // only check empty cells
                        if (board.Board[row, col].Value == 0)
                        {
                            // apply constraint propagation techniques
                            if (ApplyNakedSingle(board, row, col))
                            {
                                // if a value was placed, continue iterating
                                isDeduced = false;
                            }
                            else if (ApplyHiddenSingle(board, row, col))
                            {
                                isDeduced = false;
                            }
                            else if (ApplyNakedPair(board))
                            {
                                isDeduced = false;
                            }
                            else if (ApplyHiddenPair(board, row, col))
                            {
                                isDeduced = false;
                            }
                        }
                    }
                }
            }
            return isDeduced;
        }

        private static bool ApplyHiddenSingle(SudokuBoard board, int row, int col)
        {
            throw new NotImplementedException();
        }

        private static bool ApplyNakedSingle(SudokuBoard board, int row, int col)
        {
            if (board.Board[row, col].PossibleValues.Count == 1)
            {
                board.Board[row, col].Value = (board.Board[row, col].PossibleValues[0]);
                return true;
            }
            return false;
        }


        private static bool ApplyHiddenPair(SudokuBoard board, int row, int col)
        {
            throw new NotImplementedException();
        }

        // Apply "naked pair" optimization to each row,col and box of the sudoku board
        private static bool ApplyNakedPair(SudokuBoard board)
        {
            // Apply naked pair on the board
            if (NakedPairRow(board) || NakedPairCol(board))
                return true;
            return false;

            //NakedPairRow(board);
            // Apply on cols
            //NakedPairCol(board);
            // Applay on boxes
            //NakedPairBox(board);
        }
    }
}

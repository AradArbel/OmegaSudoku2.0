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
        static void NakedPairRow(SudokuBoard board)
        {
            // Iterate through the rows of the puzzle
            for (int row = 0; row < Utilities.maxCellValue; row++)
            {
                // Iterate through the columns of the puzzle
                for (int col = 0; col < Utilities.maxCellValue; col++)
                {
                    // Get the current cell
                    SudokuCell cell = board.Board[row, col];

                    // Skip cells that have already been assigned a value
                    if (board.Board[row, col].Value != 0)
                        continue;

                    // Iterate through the remaining cells in the row
                    for (int currentCol = col + 1; currentCol < Utilities.maxCellValue; currentCol++)
                    {
                        // Get the other cell in the row
                        SudokuCell otherCell = board.Board[row, currentCol];

                        // Skip cells that have already been assigned a value
                        if (board.Board[row, currentCol].Value != 0)
                            continue;

                        // Check if the two cells have the same set of possible values
                        if (cell.PossibleValues.SequenceEqual(otherCell.PossibleValues))
                        {
                            // The cells have the same set of possible values, so eliminate those values from the list of potential values for all other cells in the row
                            for (int currentRow = 0; currentRow < Utilities.maxCellValue; currentRow++)
                            {
                                if (currentRow == row)
                                    continue;

                                SudokuCell otherRowCell = board.Board[currentRow, col];
                                otherRowCell.PossibleValues.RemoveAll(val => cell.PossibleValues.Contains(val));
                                board.Board[row, currentCol] = otherRowCell;
                            }
                        }
                    }
                }
            }


        }

        // Look for cells that have the same set of possible values,
        // and eliminate those values from the list of potential values for other cells in the same col
        static void NakedPairCol(SudokuBoard board)
        {
            for (int col = 0; col < Utilities.maxCellValue; col++)
            {
                // Iterate through the rows of the puzzle
                for (int row = 0; row < Utilities.maxCellValue; row++)
                {
                    // Get the current cell
                    SudokuCell cell = board.Board[row, col];

                    // Skip cells that have already been assigned a value
                    if (board.Board[row, col].Value != 0)
                        continue;

                    // Iterate through the remaining cells in the column
                    for (int currentRow = row + 1; currentRow < Utilities.maxCellValue; currentRow++)
                    {
                        // Get the other cell in the column
                        SudokuCell otherCell = board.Board[currentRow, col];

                        // Skip cells that have already been assigned a value
                        if (board.Board[currentRow, col].Value != 0)
                            continue;

                        // Check if the two cells have the same set of possible values
                        if (cell.PossibleValues.SequenceEqual(otherCell.PossibleValues))
                        {
                            // The cells have the same set of possible values, so eliminate those values from the list of potential values for all other cells in the column
                            for (int currentCol = 0; currentCol < Utilities.maxCellValue; currentCol++)
                            {
                                if (currentCol == col)
                                    continue;

                                SudokuCell otherColCell = board.Board[row, currentCol];
                                otherColCell.PossibleValues.RemoveAll(val => cell.PossibleValues.Contains(val));
                                board.Board[row, currentCol] = otherColCell;
                            }
                        }
                    }
                }
            }
        }

        // Look for cells that have the same set of possible values,
        // and eliminate those values from the list of potential values for other cells in the same box
        public static void NakedPairBox(SudokuBoard board)
        {
            // Calculate box range
            int boxRange = (int)Math.Sqrt(Utilities.maxCellValue);

            // Iterate through the regions of the puzzle
            for (int box = 0; box < Utilities.maxCellValue; box++)
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
                                            if (otherRow == row && otherCol == col)
                                                continue;

                                            SudokuCell otherBoxCell = board.Board[otherRow, otherCol];
                                            otherBoxCell.PossibleValues.RemoveAll(val => cell.PossibleValues.Contains(val));
                                            board.Board[otherRow, otherCol] = otherBoxCell;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Apply "naked pair" optimization to each row,col and box of the sudoku board
        public static void NakedPair(SudokuBoard board)
        {
            // Apply on rows
            NakedPairRow(board);
            // Apply on cols
            NakedPairCol(board);
            // Applay on boxes
            NakedPairBox(board);
        }

    }

}

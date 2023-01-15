using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        static bool NakedPairRow(SudokuBoard board,int row,int col)
        {
            bool flag = false; // Check if naked pair has been found
            // Iterate through the remaining cells in the row
            for (int currentCol = col + 1; currentCol < Utilities.maxCellValue; currentCol++)
            {
                // Get the other cell in the row
                SudokuCell otherCell = board.Board[row, currentCol];

                // Skip cells that have already been assigned a value
                if (board.Board[row, currentCol].Value != 0)
                    continue;

                // Check if the two cells have the same set of possible values
                if (board.Board[row,col].PossibleValues.SequenceEqual(otherCell.PossibleValues) && board.Board[row, col].PossibleValues.Count == 2 && otherCell.PossibleValues.Count == 2)
                {
                    // The cells have the same set of possible values, so eliminate those values from the list of potential values for all other cells in the row
                    for (int checkCol = 0; checkCol < Utilities.maxCellValue; checkCol++)
                    {
                        if (checkCol != board.Board[row, col].Col && checkCol != otherCell.Col) // Not one of the cells that found to have the same set of 2 possible value
                        {
                            board.Board[row, checkCol].PossibleValues.RemoveAll(val => board.Board[row, col].PossibleValues.Contains(val));
                            flag = true;
                        }

                    }
                }
            }
            return flag;
        }

        // Look for cells that have the same set of possible values,
        // and eliminate those values from the list of potential values for other cells in the same col
        static bool NakedPairCol(SudokuBoard board,int row,int col)
        {
            bool flag = false; // Check if naked pair has been found
            // Iterate through the remaining cells in the column
            for (int currentRow = row + 1; currentRow < Utilities.maxCellValue; currentRow++)
            {
                // Get the other cell in the column
                SudokuCell otherCell = board.Board[currentRow, col];

                // Skip cells that have already been assigned a value
                if (board.Board[currentRow, col].Value != 0)
                    continue;

                // Check if the two cells have the same set of possible values
                if (board.Board[row,col].PossibleValues.SequenceEqual(otherCell.PossibleValues) && board.Board[row, col].PossibleValues.Count == 2 && otherCell.PossibleValues.Count == 2)
                {
                    // The cells have the same set of possible values, so eliminate those values from the list of potential values for all other cells in the column
                    for (int checkRow = 0; checkRow < Utilities.maxCellValue; checkRow++)
                    {
                        if (checkRow != board.Board[row, col].Row && checkRow != otherCell.Row) // Not one of the cells that found to have the same set of 2 possible value
                        {
                            board.Board[checkRow, col].PossibleValues.RemoveAll(val => board.Board[row, col].PossibleValues.Contains(val));
                            flag = true;
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


        private static bool HiddenPairRow(SudokuBoard board, int row)
        {
            bool isDone = false; // check if the optimition worked or not

            // Create a list of candidates in the current row
            List<int> candidates = new List<int>();
            for (int col = 0; col < Utilities.maxCellValue; col++)
            {
                candidates.AddRange(board.Board[row, col].PossibleValues);
            }

            // Iterate through all pairs of candidates
            for (int i = 0; i < candidates.Count; i++)
            {
                for (int j = i + 1; j < candidates.Count; j++)
                {
                    int candidate1 = candidates[i];
                    int candidate2 = candidates[j];

                    // Check if the pair of candidates is only found in two specific cells
                    int pairCount = 0;
                    int pairCol1 = -1, pairCol2 = -1;
                    for (int col = 0; col < Utilities.maxCellValue; col++)
                    {
                        if (board.Board[row, col].PossibleValues.Contains(candidate1) && board.Board[row, col].PossibleValues.Contains(candidate2))
                        {
                            pairCount++;
                            if (pairCount == 1)
                            {
                                pairCol1 = col;
                            }
                            else if (pairCount == 2)
                            {
                                pairCol2 = col;
                            }
                            else
                            {
                                // pairCount > 2, this pair can't be a hidden pair
                                break;
                            }
                        }
                    }

                    // If the pair is only found in two specific cells, remove other candidates from those cells
                    if (pairCount == 2)
                    {
                        for (int col = 0; col < Utilities.maxCellValue; col++)
                        {
                            if (col != pairCol1 && col != pairCol2)
                            {
                                board.Board[row, col].PossibleValues.Remove(candidate1);
                                board.Board[row, col].PossibleValues.Remove(candidate2);
                                isDone = true; // the optimization has been done
                            }
                            
                        }
                        
                    }
                    
                }
            }
            return isDone;

        }
        private static bool HiddenPairCol(SudokuBoard board, int col)
        {
            bool isDone = false; // check if the optimition worked or not

            // Create a list of candidates in the current column
            List<int> candidates = new List<int>();
            for (int row = 0; row < Utilities.maxCellValue; row++)
            {
                candidates.AddRange(board.Board[row, col].PossibleValues);
            }

            // Iterate through all pairs of candidates
            for (int i = 0; i < candidates.Count; i++)
            {
                for (int j = i + 1; j < candidates.Count; j++)
                {
                    int candidate1 = candidates[i];
                    int candidate2 = candidates[j];

                    // Check if the pair of candidates is only found in two specific cells
                    int pairCount = 0;
                    int pairRow1 = -1, pairRow2 = -1;
                    for (int row = 0; row < Utilities.maxCellValue; row++)
                    {
                        if (board.Board[row, col].PossibleValues.Contains(candidate1) && board.Board[row, col].PossibleValues.Contains(candidate2))
                        {
                            pairCount++;
                            if (pairCount == 1)
                            {
                                pairRow1 = row;
                            }
                            else if (pairCount == 2)
                            {
                                pairRow2 = row;
                            }
                            else
                            {
                                // pairCount > 2, this pair can't be a hidden pair
                                break;
                            }
                        }
                    }

                    // If the pair is only found in two specific cells, remove other candidates from those cells
                    if (pairCount == 2)
                    {
                        for (int row = 0; row < Utilities.maxCellValue; row++)
                        {
                            if (row != pairRow1 && row != pairRow2)
                            {
                                board.Board[row, col].PossibleValues.Remove(candidate1);
                                board.Board[row, col].PossibleValues.Remove(candidate2);
                                // In case that the optimiztation is done. else return false
                                isDone = true; // the optimization has been done
                            }
                        }
                        
                    }
                    
                }
            }
            return isDone; 
        }

        private static bool HiddenPairBox(SudokuBoard board, int row, int col)
        {
            bool isDone = false; // check if the optimition worked or not

            // Determine the top-left coordinates of the box containing the given cell
            int boxRange = (int)Math.Sqrt(Utilities.maxCellValue);
            int boxRow = row / boxRange;
            int boxCol = col / boxRange;

            // Create a list of candidates in the current box
            List<int> candidates = new List<int>();
            for (int r = boxRow * boxRange; r < (boxRow + 1) * boxRange; r++)
            {
                for (int c = boxCol * boxRange; c < (boxCol + 1) * boxRange; c++)
                {
                    candidates.AddRange(board.Board[r, c].PossibleValues);
                }
            }

            // Iterate through all pairs of candidates
            for (int i = 0; i < candidates.Count; i++)
            {
                for (int j = i + 1; j < candidates.Count; j++)
                {
                    int candidate1 = candidates[i];
                    int candidate2 = candidates[j];

                    // Check if the pair of candidates is only found in two specific cells
                    int pairCount = 0;
                    int pairRow1 = -1, pairCol1 = -1, pairRow2 = -1, pairCol2 = -1;
                    for (int r = boxRow * boxRange; r < (boxRow + 1) * boxRange; r++)
                    {
                        for (int c = boxCol * boxRange; c < (boxCol + 1) * boxRange; c++)
                        {
                            if (board.Board[r, c].PossibleValues.Contains(candidate1) && board.Board[r, c].PossibleValues.Contains(candidate2))
                            {
                                pairCount++;
                                if (pairCount == 1)
                                {
                                    pairRow1 = r;
                                    pairCol1 = c;
                                }
                                else if (pairCount == 2)
                                {
                                    pairRow2 = r;
                                    pairCol2 = c;
                                }
                                else
                                {
                                    // pairCount > 2, this pair can't be a hidden pair
                                    break;
                                }
                            }
                        }
                    }

                    // If the pair is only found in two specific cells, remove other candidates from those cells
                    if (pairCount == 2)
                    {
                        for (int currentRow = boxRow * boxRange; currentRow < (boxRow + 1) * boxRange; currentRow++)
                        {
                            for (int currentCol = boxCol * boxRange; currentCol < (boxCol + 1) * boxRange; currentCol++)
                            {
                                if ((currentRow != pairRow1 || currentCol != pairCol1) && (currentRow != pairRow2 || currentCol != pairCol2))
                                {
                                    board.Board[currentRow, currentCol].PossibleValues.Remove(candidate1);
                                    board.Board[currentRow, currentCol].PossibleValues.Remove(candidate2);
                                    // In case that the optimiztation is done. else return false
                                    isDone = true; // the optimization has been done
                                }
                            }
                        }
                    }
                }
            }
            return isDone;
        }


        //iteratively apply constraint propagation techniques until puzzle is fully deduced
        public static bool DeducePuzzle(SudokuBoard board,int row,int col)
        {
            bool isDeduced = false;
            // continue iterating until puzzle is fully deduced
            while (!isDeduced)
            {
                isDeduced = true;
                // only check empty cells
                if (board.Board[row, col].Value == 0)
                {
                    // apply constraint propagation techniques

                    //CrookAlgorithm.RemoveUnpossibleValues(board.Board[row, col], board); // remove values that are not longer possible from the possible values list of the cell

                    if (ApplyNakedSingle(board, row, col))
                    {
                        // if a value was placed, continue iterating
                        isDeduced = false;
                    }
                    else if (ApplyHiddenSingle(board, row, col))
                    {
                        isDeduced = false;
                    }
                    else if (ApplyNakedPair(board, row, col))
                    {
                        isDeduced = false;
                    }
                    else if (ApplyHiddenPair(board, row, col))
                    {
                        isDeduced = false;
                    }
                }           
            }
            return isDeduced;
        }

        private static bool ApplyHiddenSingle(SudokuBoard board, int row, int col)
        {
            bool isDone = false; // check if the optimization worked or not

            // Iterate through all candidates)
            for (int num = Utilities.minCellValue; num <= Utilities.maxCellValue; num++)
            {
                int count = 0;
                int colIndex = -1;
                // Iterate through all columns in the current row
                for (int currentCol = 0; currentCol < Utilities.maxCellValue; currentCol++)
                {
                    // Check if the current cell has the current candidate
                    if (board.Board[row, currentCol].PossibleValues.Contains(num))
                    {
                        count++;
                        colIndex = currentCol;
                    }
                }
                // If the current candidate is only found in one cell in the current row
                if (count == 1)
                {
                    // Assign the candidate as the value of that cell
                    board.Board[row, colIndex].Value = num;
                    isDone = true;
                }
                

                // Check columns
                count = 0;
                int rowIndex = -1;
                // Iterate through all rows in the current column
                for (int currentRow = 0; currentRow < Utilities.maxCellValue; currentRow++)
                {
                    // Check if the current cell has the current candidate
                    if (board.Board[currentRow, col].PossibleValues.Contains(num))
                    {
                        count++;
                        rowIndex = currentRow;
                    }
                }
                // If the current candidate is only found in one cell in the current column
                if (count == 1)
                {
                    // Assign the candidate as the value of that cell
                    board.Board[rowIndex, col].Value = num;
                    isDone = true;
                }
                

                // check boxes    
                int boxRange = (int)Math.Sqrt(Utilities.maxCellValue); // Calculate the indices of the box 

                count = 0;
                rowIndex = -1;
                colIndex = -1;
                int boxRow = row / boxRange;
                int boxColumn = col / boxRange;
                // Iterate through the elements in the box
                for (int currentRow = boxRow * boxRange; currentRow < boxRow * boxRange + boxRange; currentRow++)
                {
                    for (int currentCol = boxColumn * boxRange; currentCol < boxColumn * boxRange + boxRange; currentCol++)
                        if (board.Board[currentRow, currentCol].PossibleValues.Contains(num))
                        {
                            count++;
                            rowIndex = currentRow;
                            colIndex = currentCol;
                        }
               
                }
                // If the current candidate is only found in one cell in the current box
                if (count == 1)
                {
                    // Assign the candidate as the value of that cell
                    board.Board[rowIndex, colIndex].Value = num;
                    isDone = true;
                }
                
            }

            return isDone;
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

        // Apply "hidden pair" optimization to each row,col and box of the sudoku board
        private static bool ApplyHiddenPair(SudokuBoard board, int row, int col)
        {
            // Apply hidden pair on the board
            if (HiddenPairRow(board, row) || HiddenPairCol(board,col) /*|| HiddenPairBox(board,row, col)*/)
                return true;
            return false;
        }

        // Apply "naked pair" optimization to each row,col and box of the sudoku board
        private static bool ApplyNakedPair(SudokuBoard board,int row,int col)
        {
            // Apply naked pair on the board
            if (NakedPairRow(board,row,col) || NakedPairCol(board, row, col))
                return true;
            return false;
        }
    }
}

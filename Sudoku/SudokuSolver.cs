using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Sudoku
{
    public class SudokuSolver
    {
        //Check whether value is already exist in col or not
        public static bool isExistInCol(SudokuCell cell, SudokuBoard board, int num)
        {
            int col = cell.Col;
            for (int row = 0; row < Utilities.maxCellValue - 1; row++)
                if (board.Board[row, col].Value == num)
                    return true;
            return false;
        }

        //Check whether value is already exist in row or not
        public static bool isExistInRow(SudokuCell cell, SudokuBoard board, int num)
        {
            int row = cell.Row;
            for (int col = 0; col < Utilities.maxCellValue - 1; col++)
                if (board.Board[row, col].Value == num)
                    return true;
            return false;
        }

        //Check whether value is already exist in box or not
        public static bool isExistInBox(SudokuCell cell, SudokuBoard board, int num)
        {
            // Calculate the indices of the box that the value is in
            int boxRange = (int)Math.Sqrt(Utilities.maxCellValue);
            int boxRow = cell.Row / boxRange;
            int boxColumn = cell.Col / boxRange;
            // Iterate through the elements in the box and check if the value is present
            for (int row = boxRow * boxRange; row < boxRow * boxRange + boxRange; row++)
            {
                for (int col = boxColumn * boxRange; col < boxColumn * boxRange + boxRange; col++)
                {
                    if (board.Board[row, col].Value == num)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //get empty location and update row and column
        public static int[] findEmptyPlace(SudokuBoard board)
        {
            int row, col;
            bool empty = false; // helper flag that turn true when found an empty place

            //array of two numbers that represent the empty place loation by row and col
            int[] emptyPlace = new int[2];
            for (row = 0; row < Utilities.maxCellValue - 1; row++)
            {
                if (!empty) // check if the empty place already has found. if yes return emptyPlace if not keep searching
                {
                    for (col = 0; col < Utilities.maxCellValue - 1 && !empty; col++)  //marked with 0 is empty
                    {
                        if (board.Board[row, col].Value == 0)
                        {
                            emptyPlace[0] = row; // add row number to the first place in the array which represent the row of the empty place
                            emptyPlace[1] = col; // add col number to the second place in the array which represent the col of the empty place  
                            empty = true; // change the flag to true once we found the empty place to exist the loop
                        }
                    }
                }


            }
            return emptyPlace;
        }

        //Check if item is not found in col, row and current box
        public static bool isValidPlace(SudokuCell cell, SudokuBoard board, int num)
        {

            return !isExistInRow(cell, board, num) // not in row
                && !isExistInCol(cell, board, num) // not in col
                && !isExistInBox(cell, board, num); // not in box
        }

        // Solve the sudoku in recursive backtracking method
        public static bool backTracking(SudokuBoard board)
        {
            //Itrate throw all cells in the board to find the first empty cell
            int row = -1, col = -1;
            for (int i = 0; i < Utilities.maxCellValue; i++)
            {
                for (int j = 0; j < Utilities.maxCellValue; j++)
                {
                    // if the cell is empty, break
                    if (board.Board[i, j].Value == 0)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
                // when there were no empty cells in the row, break
                if (row != -1)
                    break;
            }

            //when all places are filled
            if (row == -1)
                return true;

            // itreate throw all possible values in each cell and try to set their possible values to specific location
            foreach (char possibleValue in board.Board[row, col].PossibleValues)
            {
                //check validation, if yes, put the number in the grid
                if (isValidPlace(board.Board[row, col], board, (int)possibleValue))
                {
                    board.Board[row, col].Value = (int)possibleValue;
                    if (backTracking(board)) //recursively go for other rooms in the grid
                        return true;
                    //turn to unassigned space when conditions are not satisfied
                    board.Board[row, col].Value = 0;
                }
            }
            // No vaild value found -> puzzle cant be solved    
            return false;
        }

        // Solve the sudoku puzzle
        public static bool solveSudoku(SudokuBoard board)
        {
            // First scan all board and remove values from possible values list of ever cell that we know already that cant be in that specific cell
            CrookAlgorithm.RemoveValuesFromCells(board);

            // Then sets all unique cell value
            CrookAlgorithm.UniqueCells(board);

            // After that use backTracking algorithem in recursive way
            if (backTracking(board))
            {
                return true;
            }
            // puzzle cant be solved
            return false;
        }
    }
}

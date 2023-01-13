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
        public static bool IsExistInCol(SudokuCell cell, SudokuBoard board, int num)
        {
            int col = cell.Col;
            for (int row = 0; row < Utilities.maxCellValue - 1; row++)
                if (board.Board[row, col].Value == num)
                    return true;
            return false;
        }

        //Check whether value is already exist in row or not
        public static bool IsExistInRow(SudokuCell cell, SudokuBoard board, int num)
        {
            int row = cell.Row;
            for (int col = 0; col < Utilities.maxCellValue - 1; col++)
                if (board.Board[row, col].Value == num)
                    return true;
            return false;
        }

        //Check whether value is already exist in box or not
        public static bool IsExistInBox(SudokuCell cell, SudokuBoard board, int num)
        {
            // Calculate the indices of the box 
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
        public static void FindEmptyPlace(SudokuBoard board,ref int row, ref int col)
        {
            for (int currentRow = 0; currentRow < Utilities.maxCellValue; currentRow++)
            {
                for (int currentCol = 0; currentCol < Utilities.maxCellValue; currentCol++)
                {
                    // if the cell is empty, break
                    if (board.Board[currentRow, currentCol].Value == 0)
                    {
                        row = currentRow;
                        col = currentCol;
                        break;
                    }
                }
                // when there were no empty cells in the row, break
                if (row != -1)
                    break;
            }
        }

        //Check if item is not found in col, row and current box
        public static bool IsValidPlace(SudokuCell cell, SudokuBoard board, int num)
        {

            return !IsExistInRow(cell, board, num) // not in row
                && !IsExistInCol(cell, board, num) // not in col
                && !IsExistInBox(cell, board, num); // not in box
        }

        // Solve the sudoku in recursive backtracking method
        public static bool BackTracking(SudokuBoard board)
        {
            SudokuBoard copyBoard = board; // Create a copy of the board before making any changes

            //Itrate throw all cells in the board to find the first empty cell
            int row = -1, col = -1;
            FindEmptyPlace(board, ref row, ref col); // Using "ref" to pass the variables by refernce and not by value to allows the method change row and col

            //when all places are filled
            if (row == -1)
                return true;


            //CrookAlgorithm.RemoveValuesFromCells(board);

            //CrookAlgorithm.RemoveUnpossibleValues(board.Board[row, col], board);

            // itreate throw all possible values in each cell and try to set their possible values to specific location
            foreach (int possibleValue in board.Board[row, col].PossibleValues)
            {
                //check validation, if yes, put the number in the grid
                if (IsValidPlace(board.Board[row, col], board,possibleValue))
                {
                     // create new copy of the board before
                    board.Board[row, col].Value = possibleValue;
                    if (BackTracking(board)) //recursively go for other rooms in the grid
                        return true;
                    //turn to unassigned space when conditions are not satisfied
                    board.Board[row, col].Value = 0;
                    //board.Board = copyBoard.Board; // restore the board to its previous state

                    //restore possible values for each cell
                    //CrookAlgorithm.UpdatePossibleValues(board);
                }
            }
            //board.Board = copyBoard.Board; // restore the board to its previous state
            // No vaild value found -> puzzle cant be solved    
            return false;
        }

        // Solve the sudoku puzzle
        public static bool SolveSudoku(SudokuBoard board)
        {
            // First scan all board and remove values from possible values list of ever cell that we know already that cant be in that specific cell
            CrookAlgorithm.RemoveValuesFromCells(board);

            // Then sets all unique cell value - cell that have only one possible value
            CrookAlgorithm.UniqueCells(board);

            // After that use backTracking algorithem in recursive way
            if (BackTracking(board))
            {
                return true;
            }
            // puzzle cant be solved
            return false;
        }
    }
}

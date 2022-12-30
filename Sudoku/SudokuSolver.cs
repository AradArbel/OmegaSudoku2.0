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
            for (int row = 0; row < Constants.boardRange; row++)
                if (board.Board[row, col].Value == num)
                    return true;
            return false;
        }

        //Check whether value is already exist in row or not
        public static bool isExistInRow(SudokuCell cell, SudokuBoard board, int num)
        {
            int row = cell.Row;
            for (int col = 0; col < Constants.boardRange; col++)
                if (board.Board[row, col].Value == num)
                    return true;
            return false;
        }

        //Check whether value is already exist in box or not
        public static bool isExistInBox(SudokuCell cell, SudokuBoard board, int num)
        {
            // Calculate the indices of the box that the value is in
            int boxRow = cell.Row / Constants.boxRange;
            int boxColumn = cell.Col / Constants.boxRange;
            // Iterate through the elements in the box and check if the value is present
            for (int row = boxRow * Constants.boxRange; row < boxRow * Constants.boxRange + Constants.boxRange; row++)
            {
                for (int col = boxColumn * Constants.boxRange; col < boxColumn * Constants.boxRange + Constants.boxRange; col++)
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
            for (row = 0; row < Constants.boardRange; row++)
            {
                if (!empty) // check if the empty place already has found. if yes return emptyPlace if not keep searching
                {
                    for (col = 0; col < Constants.boardRange && !empty; col++)  //marked with 0 is empty
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
            // First scan all board and remove values from possible values list of ever cell that we know already that cant be in that specific cell 
            CrookAlgorithm.RemoveValuesFromCells(board);

            // Then sets all unique cell value
            CrookAlgorithm.UniqueCells(board);

            // After that use backTracking algorithem in recursive way
            if (board.IsFull())
                return true; //when all places are filled
            foreach (SudokuCell cell in board.Board)
            {
                if(cell.Value == 0)
                {
                    // itreate throw all possible values in each cell and try to set their possible values to specific location 
                    foreach (int possibleValue in cell.PossibleValues)
                    {
                        if (isValidPlace(board.Board[cell.Row, cell.Col], board, possibleValue))
                        { //check validation, if yes, put the number in the grid
                            board.Board[cell.Row, cell.Col].Value = possibleValue;
                            if (backTracking(board)) //recursively go for other rooms in the grid
                                return true;
                            board.Board[cell.Row, cell.Col].Value = 0; //turn to unassigned space when conditions are not satisfied
                        }
                    }
                }
            }
            return false;
        }
        public static bool solveSudoku(SudokuBoard board)
        {
            // First scan all board and remove values from possible values list of ever cell that we know already that cant be in that specific cell 
             CrookAlgorithm.RemoveValuesFromCells(board);

            // Then sets all unique cell value
            //CrookAlgorithm.UniqueCells(board);

            // After that use backTracking algorithem in recursive way
            if(backTracking(board))
            {
                return true;
            }
            return false;
        }
    }
}

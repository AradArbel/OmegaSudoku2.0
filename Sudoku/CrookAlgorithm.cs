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
    static class CrookAlgorithm
    {
        // Remove possible values that are already used in the row, column, and box from a cell
        public static void RemoveUnpossibleValues(SudokuCell cell, SudokuBoard board)
        {
            if (cell.Value == 0)
            {
                List<int> unpossibleValues = new List<int>(); // list of values to be remove
                foreach (int value in cell.PossibleValues)
                {
                    //check if value is unpossible
                    if (SudokuSolver.IsExistInRow(cell, board, value) || SudokuSolver.IsExistInCol(cell, board, value) || SudokuSolver.IsExistInBox(cell, board, value))
                        unpossibleValues.Add(value);
                }
                foreach (int value in unpossibleValues) // remove all impossibale values
                {
                    cell.RemovePossibility(value);
                }
            }
        }

        // Update possible values list for each cell of the board
        public static void UpdatePossibleValues(SudokuBoard board)
        {
            foreach (SudokuCell sudokuCell in board.Board)
            {
                sudokuCell.PossibleValues.Clear();
                for (int possible = Utilities.minCellValue; possible <= Utilities.maxCellValue; possible++)
                {
                    // Check if value is possible
                    if(!SudokuSolver.IsExistInRow(sudokuCell, board, possible) && SudokuSolver.IsExistInCol(sudokuCell, board, possible) && SudokuSolver.IsExistInBox(sudokuCell, board, possible)) 
                        sudokuCell.PossibleValues.Add(possible);
                }  
            }
        }

        // Remove unpossible values from each cell on the board
        public static void RemoveValuesFromCells(SudokuBoard board)
            {
            foreach(SudokuCell sudokuCell in board.Board)
                RemoveUnpossibleValues(sudokuCell, board);
            }

        // Check if there are any unique cells (cell that only have one possible value from the beginning) in the board. if found update their value
        public static void UniqueCells(SudokuBoard board)
        {
            foreach (SudokuCell sudokuCell in board.Board)
                if (sudokuCell.PossibleValues.Count == 1 )
                    sudokuCell.Value = (sudokuCell.PossibleValues[0]);
        }

    }
    
}

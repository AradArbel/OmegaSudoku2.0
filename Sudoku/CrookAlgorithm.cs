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
        // Remove values that are already used in the row, column, and box
        static void RemoveUnpossibleValues(SudokuCell cell, SudokuBoard board)
        {
            int row = cell.Row;
            int col = cell.Col;

            for (int possibleIndex = 0; possibleIndex < cell.PossibleValues.Count; possibleIndex++)
            {
                int value = cell.PossibleValues[possibleIndex];
                if (SudokuSolver.isExistInRow(cell, board, (int)value) || SudokuSolver.isExistInCol(cell, board, (int)value) || SudokuSolver.isExistInBox(cell, board, (int)value))
                {
                    cell.RemovePossibility((int)value);
            }
            }

        }

        // Remove unpossible values from each cell on the board
        public static void RemoveValuesFromCells(SudokuBoard board)
            {
            foreach(SudokuCell sudokuCell in board.Board)
                {
                RemoveUnpossibleValues(sudokuCell, board);
                }
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

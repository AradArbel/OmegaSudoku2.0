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
        // Remove values that are already used in the row, column, and block
        static void RemoveUnpossibleValues(SudokuCell cell, SudokuBoard board)
        {
            int row = cell.Row;
            int col = cell.Col;

            for (int cellIndex = 0; cellIndex < Constants.maxCellValue; cellIndex++)
            {
                // remove function in ArrayList removes the first occurrence of a specific object and If the ArrayList does not contain the specified object, the ArrayList remains unchanged.

                cell.RemovePossibility(board.Board[row, cellIndex].Value); // if found an equal value in the same row, remove it from the possible values list
                cell.RemovePossibility(board.Board[cellIndex, col].Value);  // if found an equal value in the same col, remove it from the possible values list  
            }

            int blockRow = row / Constants.boxRange;
            int blockCol = col / Constants.boxRange;

            for (int i = blockRow * Constants.boxRange; i < blockRow * Constants.boxRange + Constants.boxRange; i++)
            {
                for (int j = blockCol * Constants.boxRange; j < blockCol * Constants.boxRange + Constants.boxRange; j++)
                {
                    cell.RemovePossibility(board.Board[i, j].Value);  // if found an equal value in the same box, remove it from the possible values list
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
                    sudokuCell.SetValue(value: sudokuCell.PossibleValues[0]);

        }



    }

}

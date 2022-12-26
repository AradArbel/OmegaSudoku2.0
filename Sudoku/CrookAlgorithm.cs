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
    public class CrookAlgorithm
    {
        // Remove values that are already used in the row, column, and block
        public void removeUnpossibleValues(SudokuCell cell, SudokuBoard board)
        {
            int row = cell.Row;
            int col = cell.Col;

            for (int cellIndex = 0; cellIndex < Constants.maxCellValue; cellIndex++)
            {
                cell.PossibleValues.Remove(board.Board[row, cellIndex]);
                cell.PossibleValues.Remove(board.Board[cellIndex, col]);
            }

            int blockRow = row / (int)Math.Sqrt(Constants.maxCellValue);
            int blockCol = col / (int)Math.Sqrt(Constants.maxCellValue);

            for (int i = blockRow * (int)Math.Sqrt(Constants.maxCellValue); i < blockRow * (int)Math.Sqrt(Constants.maxCellValue) + (int)Math.Sqrt(Constants.maxCellValue); i++)
            {
                for (int j = blockCol * (int)Math.Sqrt(Constants.maxCellValue); j < blockCol * (int)Math.Sqrt(Constants.maxCellValue) + (int)Math.Sqrt(Constants.maxCellValue); j++)
                {
                    cell.PossibleValues.Remove(board.Board[i, j]);
                }
            }

            //set possibleValues;
        }

    }
    
}

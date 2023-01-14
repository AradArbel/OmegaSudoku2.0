using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Sudoku
{
    // Representation of a cell in a Sudoku board 
    public class SudokuCell
    {

        // possible values for this cell.
        private List<int> possibleValues;

        //Constructor for this class.
        public SudokuCell(int row, int col,int value)
        {
            //Row for this cell into a Sudoku board.
            Row = row;
            //Column for this cell into a Sudoku board.
            Col = col;
            //Value for this cell into a Sudoku board.
            Value = value;

            possibleValues = new List<int>();
            // check if the cell is empty or already have constant value 
            if (value == 0)
            {
                // initialize all possible values in the beginning
                for (int possible = Utilities.minCellValue; possible <= Utilities.maxCellValue; possible++)
                {
                    possibleValues.Add(possible);
                }
            }
            else // mean that this cell have constant value. add this single value to possible values list
            {
               possibleValues.Add(value);
            }
        }

        // Get row for this cell into a Sudoku board.
        public int Row { get; }

        // Get column for this cell into a Sudoku board.
        public int Col { get; }

        // Get and set Value for this cell into a Sudoku board (0 if value is unknown)
        public int Value { get; set; }

        // Get and set for possible values list for this cell
        public List<int> PossibleValues
        {
            get { return this.possibleValues;}
            set { this.possibleValues = value; }
        }

        // Removes a value from the list of possible values.
        public void RemovePossibility(int value)
        {
            this.possibleValues.Remove(value);
        }

        // Has this cell a valid value?
        public bool IsValid
        {
            get { return this.Value >= Utilities.minCellValue && this.Value <= Utilities.maxCellValue; }
        }

    }
}

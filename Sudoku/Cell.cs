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
        // Row for this cell into a Sudoku board.
        private int row;

        //Column for this cell into a Sudoku board.
        private int col;

        //box for this cell into a Sudoku board.
        private int box;

        // Value for this cell into a Sudoku board.
        private int value;

        // possible values for this cell.
        private ArrayList possibleValues;

        //Constructor for this class.
        public SudokuCell(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.value = 0;
            this.possibleValues = new ArrayList();
            // initialize all possible values in the beginning
            for (int possible = Constants.minCellValue; possible <= Constants.maxCellValue; possible++)
            {
                possibleValues.Add(possible);
            }
        }

        // Get row for this cell into a Sudoku board.
        public int Row
        {
            get { return this.row; }
        }

        // Get column for this cell into a Sudoku board.
        public int Col
        {
            get { return this.col; }
        }

        // Get Value for this cell into a Sudoku board (0 if value is unknown)
        public int Value
        {
            get{ return this.value;}
        }

        // Get and set for possible values list for this cell
        public ArrayList PossibleValues
        {
            get { return this.possibleValues;}
            set { this.possibleValues = value; }
        }

        // Removes a value from the list of possible values.
        public void RemovePossibility(int value)
        {
            if (this.possibleValues.Contains(value))
            {
                this.possibleValues.Remove(value);
            }
        }

        // Has this cell a valid value?
        public bool IsValid
        {
            get { return (this.value >= Constants.minCellValue && this.value <= Constants.maxCellValue); }
        }

        // Sets a value for this cell.
        public void SetValue(int value)
        {
            if (this.IsValid)
            {
                throw new ApplicationException(
                    string.Format("Already has been set a value for cell at [{0}, {1}].",
                    this.row + 1, this.col + 1));
            }

            this.IsVaildValue(value);
            this.value = value;
        }

        // Checks if the specified value is valid for a cell.
        private bool IsVaildValue(int value)
        {
            if (!possibleValues.Contains(value))
            {
                return false;
            }
            return true;

        }
    }
}

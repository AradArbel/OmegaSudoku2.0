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
        private object value;

        // possible values for this cell.
        private ArrayList possibleValues;

        //Constructor for this class.
        public SudokuCell(int row, int col,object value)
        {
            this.row = row;
            this.col = col;
            this.value = value;
            this.possibleValues = new ArrayList();

            // check if the cell is empty or already have constant value 
            if ((int)value == 0)
            {
                // initialize all possible values in the beginning
                for (int possible = Constants.minCellValue; possible <= Constants.maxCellValue; possible++)
                {
                    possibleValues.Add(possible);
                }
            }
            else // mean that this cell have constant value. add this single value to possible values list
            {
                possibleValues.Add((int)value);
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
            get{ return (int)this.value;}    
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
            get { return (int)this.value >= Constants.minCellValue && (int) this.value <= Constants.maxCellValue; }
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

        // Sets a value for this cell.
        public void SetValue(Object value)
        {
            /*if (this.IsValid)
            {
                throw new ApplicationException(
                    string.Format("Already has been set a value for cell at [{0}, {1}].",
                    this.row + 1, this.col + 1));
            }
            */

            if (IsVaildValue((int)value))
                this.value = value;
            //todo add not vaild value exception
        }

        
    }
}

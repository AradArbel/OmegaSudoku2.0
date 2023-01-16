using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sudoku
{
    // Tests for differents boards
    internal class Tests
    {
        [TestMethod]
        public void Test_1x1()
        {
            string boardInput = "0";
            SudokuBoard board = new (boardInput);
            SudokuSolver.SolveSudoku(board);
            string result = board.ConvertBoardToString();
            Assert.AreEqual("1", result);
        }


        [TestMethod]
        public void Test_4x4Empty()
        {
            string boardInput = "0000000000000000";
            SudokuBoard board = new(boardInput);
            SudokuSolver.SolveSudoku(board);
            string result = board.ConvertBoardToString();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Test_4x4NonEmpty()
        {
            string boardInput = "0000200004000000";
            SudokuBoard board = new(boardInput);
            SudokuSolver.SolveSudoku(board);
            string result = board.ConvertBoardToString();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Test_9x9Empty()
        {
            string boardInput = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SudokuBoard board = new(boardInput);
            SudokuSolver.SolveSudoku(board);
            string result = board.ConvertBoardToString();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_9x9NonEmpty()
        {
            string boardInput = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";
            SudokuBoard board = new(boardInput);
            SudokuSolver.SolveSudoku(board);
            string result = board.ConvertBoardToString();
            Assert.AreEqual(result, "831529674796814253542637189159783426483296715627145938365471892274958361918362547");

        }

        [TestMethod]
        public void Test_16x16Empty()
        {
            string boardInput = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SudokuBoard board = new(boardInput);
            SudokuSolver.SolveSudoku(board);
            string result = board.ConvertBoardToString();
            Assert.AreEqual(result, "831529674796814253542637189159783426483296715627145938365471892274958361918362547");

        }
    }
}

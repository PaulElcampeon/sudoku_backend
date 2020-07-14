using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_Application.Models
{
    public class SudokuAnswerRequest
    {
        public SudokuValue[,] originalBoard { get; set; }

        public SudokuValue[,] edittedBoard { get; set; }

    }
}

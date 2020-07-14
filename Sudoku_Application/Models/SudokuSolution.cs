using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_Application.Models
{
    public class SudokuSolution
    {
        public bool isSuccessful { get; set; }

        public SudokuValue[,] solution { get; set; }
    }
}

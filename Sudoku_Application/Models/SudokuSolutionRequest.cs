﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_Application.Models
{
    public class SudokuSolutionRequest
    {
        public SudokuValue[,] currentBoard { get; set; }
    }
}

using Sudoku_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_Application.Service
{
    public interface ISudokuService
    {
        SudokuSolution FindSolution(SudokuSolutionRequest solutionRequest);

        bool IsRequestValid(SudokuSolutionRequest solutionRequest);
    }
}

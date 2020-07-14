using Sudoku_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_Application.Services
{
    public interface ISudokuService
    {
        SudokuSolution FindSolution(SudokuSolutionRequest solutionRequest);

        bool IsAnswerCorrect(SudokuAnswerRequest answerRequest);

        bool IsSolutionRequestValid(SudokuSolutionRequest solutionRequest);

        bool CheckIfSudokuBoardsAreEqual(SudokuValue[,] board1, SudokuValue[,] board2);

        bool IsAnswerRequestValid(SudokuAnswerRequest answerRequest);

        SudokuValue[,] FormatSudokuBoard(int[,] board);
    }
}

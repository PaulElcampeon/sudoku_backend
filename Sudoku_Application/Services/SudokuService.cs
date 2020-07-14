using Sudoku_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_Application.Services
{
    public class SudokuService : ISudokuService
    {
        private const int SIZE = 9;

        public SudokuSolution FindSolution(SudokuSolutionRequest solutionRequest)
        {
            SudokuValue[,] board = solutionRequest.currentBoard;

            bool hasFoundSolution = UseBackTrackingAlgorithmToFindSolution(board);

            return new SudokuSolution() { isSuccessful = hasFoundSolution, solution = board };         
        }

        private bool IsValueInRow(SudokuValue[,] board, int row, int value)
        {
            for (int i = 0; i < SIZE; i++)
            {
                if (board[row, i].value == value) return true;

            }

            return false;
        }

        private bool IsValueInColumn(SudokuValue[,] board,  int column, int value)
        {
            for (int i = 0; i < SIZE; i++)
            {
                if (board[i, column].value == value) return true;
                
            }

            return false;
        }

        private bool IsValueIBlock(SudokuValue[,] board,  int row, int column, int value)
        {
            int rowToCheck = row - row % 3;
            int columnToCheck = column - column % 3;

            for (int i = rowToCheck; i < rowToCheck + 3; i++)
            {
                for (int j = columnToCheck; j < columnToCheck + 3; j++)
                {
                    if (board[i, j].value == value) return true;
                }
            }

            return false;
        }

        private bool IsValueOkToUse(SudokuValue[,] board, int row, int column, int value)
        {
            return !IsValueInRow(board, row, value) && !IsValueInColumn(board, column, value) && !IsValueIBlock(board, row, column,value);
        }

    
        private bool UseBackTrackingAlgorithmToFindSolution(SudokuValue[,] board)
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int column = 0; column < SIZE; column++)
                {
                    //Check if cell is empty
                    if (board[row,column].value == 0)
                    {
                        //Try possible values from 1 - 9
                        for (int value = 1; value <= SIZE; value++)
                        {
                            if (IsValueOkToUse(board, row, column, value))
                            {
                                //value is ok to use
                                board[row, column].value = value;

                                if (UseBackTrackingAlgorithmToFindSolution(board))//Start backtracking recursively
                                {
                                    return true;
                                }
                                else
                                {
                                    board[row, column].value = 0;// If no solution, we empty the cell and continue
                                }
                            }
                        }
                        return false;
                    }
                }

            } 
            return true;
        }

        public bool IsSolutionRequestValid(SudokuSolutionRequest solutionRequest)
        {
            SudokuValue[,] board = solutionRequest.currentBoard;

            return board.GetLength(0) == 9 && board.GetLength(1) == 9;
        }

        public bool IsAnswerRequestValid(SudokuAnswerRequest answerRequest)
        {
            return answerRequest.edittedBoard.GetLength(0) == 9 &&
                answerRequest.edittedBoard.GetLength(1) == 9 &&
                answerRequest.originalBoard.GetLength(0) == 9 &&
                answerRequest.originalBoard.GetLength(1) == 9;
        }

        public SudokuValue[,] FormatSudokuBoard(int[,] board)
        {
            SudokuValue[,] newBoard = new SudokuValue[board.GetLength(0), board.GetLength(1)];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    bool wasGiven = board[i, y] != 0;

                    newBoard[i, y] = new SudokuValue() { value = board[i, y], wasGiven = wasGiven };
                }
            }

            return newBoard;
        }

        public bool IsAnswerCorrect(SudokuAnswerRequest answerRequest)
        {
            SudokuValue[,] solutionToOriginalBoard = answerRequest.originalBoard;

            bool hasFoundSolution = UseBackTrackingAlgorithmToFindSolution(solutionToOriginalBoard);

            if (hasFoundSolution)
            {
                return CheckIfSudokuBoardsAreEqual(answerRequest.originalBoard, answerRequest.edittedBoard);
            }

            return false;
        }

        public bool CheckIfSudokuBoardsAreEqual(SudokuValue[,] board1, SudokuValue[,] board2)
        {
            if (board1.GetLength(0) == board2.GetLength(0) && board1.GetLength(1) == board2.GetLength(1))
            {
                for (int i = 0; i < board1.GetLength(0); i++)
                {
                    for (int y = 0; y < board1.GetLength(1); y++)
                    {
                        if (board1[i, y].value != board2[i, y].value) return false;

                        if (board1[i, y].wasGiven != board2[i, y].wasGiven) return false;
                    }
                }
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}

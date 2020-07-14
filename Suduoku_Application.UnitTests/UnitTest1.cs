using NUnit.Framework;
using Sudoku_Application.Models;
using Sudoku_Application.Services;

namespace Tests
{
    public class SudokuServiceTests
    {
        private SudokuService service;

        [SetUp]
        public void Setup()
        {
            service = new SudokuService();
        }

        [Test]
        public void FormatSudokuBoard_BoardFormatted_ReturnsTrue()
        {
            // Arrange
            int[,] unformattedBoard = new int[2, 2] { { 1, 0 }, { 3, 4 } };
            SudokuValue[,] expected = new SudokuValue[2, 2] 
            { 
                {
                    new SudokuValue() { value = 1, wasGiven = true},
                    new SudokuValue() { value = 0, wasGiven = false}
                }, 
                {
                    new SudokuValue() { value = 3, wasGiven = true},
                    new SudokuValue() { value = 4, wasGiven = true}
                }
            };

            // Act
            SudokuValue[,] actual = service.FormatSudokuBoard(unformattedBoard);
            
            // Assert
            Assert.IsTrue(CheckIfSudokuBoardsAreEqual(expected, actual));
        }


        [Test]
        public void FindSolution_SolutionFound_ReturnsTrue()
        {
            // Arrange
            SudokuSolutionRequest request = new SudokuSolutionRequest() { currentBoard = GetValidFormattedBoard()};

            // Act
            SudokuSolution solution = service.FindSolution(request);
            
            // Assert
            Assert.IsTrue(solution.isSuccessful, "Should return true");
        }

        [Test]
        public void FindSolution_NoSolutionFound_ReturnsFalse()
        {
            // Arrange
            SudokuSolutionRequest request = new SudokuSolutionRequest() { currentBoard = GetInvalidFormattedBoard() };

            // Act
            SudokuSolution solution = service.FindSolution(request);

            // Assert
            Assert.IsFalse(solution.isSuccessful, "Should return false");
        }

        [Test]
        public void IsRequestValid_RequestWillBeValid_ReturnsTrue()
        {
            // Arrange
            SudokuSolutionRequest request = new SudokuSolutionRequest() { currentBoard = new SudokuValue[9, 9] };

            // Act
            bool actual = service.IsRequestValid(request);

            // Assert
            Assert.IsTrue(actual, "Should return true");
        }

        [Test]
        public void IsRequestValid_RequestWillBeInvalid_false()
        {
            // Arrange
            SudokuSolutionRequest request = new SudokuSolutionRequest() { currentBoard = new SudokuValue[2, 2] };

            // Act
            bool actual = service.IsRequestValid(request);

            // Assert
            Assert.IsFalse(actual, "Should return false");
        }

        private SudokuValue[,] GetValidFormattedBoard()
        {
            int[,] unformattedValidBoard = new int[,]
            {
                { 0, 0, 2, 0, 9, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 7, 0, 8, 9 },
                { 6, 8, 9, 0, 0, 4, 0, 0, 0 },
                { 0, 0, 3, 0, 0, 0, 0, 0, 0},
                { 0, 6, 0, 0, 8, 9, 0, 2, 3 },
                { 8, 9, 7, 0, 1, 0, 0, 4, 6 },
                { 3, 2, 1, 8, 4, 0, 0, 7, 0 },
                { 5, 0, 0, 0, 0, 3, 8, 0, 0 },
                { 9, 7, 8, 0, 2, 0, 3, 6, 0 }
            };
            return service.FormatSudokuBoard(unformattedValidBoard);
        }

        private SudokuValue[,] GetInvalidFormattedBoard()
        {
            int[,] unformattedInvalidBoard = new int[,]
            {
                { 0, 2, 2, 0, 2, 2, 0, 0, 0 },
                { 0, 0, 2, 0, 0, 7, 0, 8, 9 },
                { 6, 8, 2, 0, 0, 4, 0, 0, 0 },
                { 0, 0, 3, 0, 0, 0, 0, 0, 0},
                { 2, 6, 0, 2, 8, 9, 2, 2, 3 },
                { 0, 0, 7, 0, 1, 0, 2, 4, 6 },
                { 3, 2, 1, 8, 4, 0, 0, 7, 0 },
                { 5, 0, 2, 0, 0, 0, 0, 0, 2 },
                { 0, 7, 8, 0, 2, 0, 0, 6, 0 }
            };
            return service.FormatSudokuBoard(unformattedInvalidBoard);
        }

        private bool CheckIfSudokuBoardsAreEqual(SudokuValue[,] board1, SudokuValue[,] board2)
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
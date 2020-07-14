using NUnit.Framework;
using Sudoku_Application.Models;
using Sudoku_Application.Services;

namespace Tests
{
    public class SudokuServiceTests
    {
        private SudokuService _service;

        [SetUp]
        public void Setup()
        {
            _service = new SudokuService();
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
            SudokuValue[,] actual = _service.FormatSudokuBoard(unformattedBoard);
            
            // Assert
            Assert.IsTrue(_service.CheckIfSudokuBoardsAreEqual(expected, actual));
        }


        [Test]
        public void FindSolution_SolutionFound_ReturnsTrue()
        {
            // Arrange
            SudokuSolutionRequest request = new SudokuSolutionRequest() { currentBoard = GetValidFormattedBoard()};

            // Act
            SudokuSolution solution = _service.FindSolution(request);
            
            // Assert
            Assert.IsTrue(solution.isSuccessful, "Should return true");
        }

        [Test]
        public void FindSolution_NoSolutionFound_ReturnsFalse()
        {
            // Arrange
            SudokuSolutionRequest request = new SudokuSolutionRequest() { currentBoard = GetInvalidFormattedBoard() };

            // Act
            SudokuSolution solution = _service.FindSolution(request);

            // Assert
            Assert.IsFalse(solution.isSuccessful, "Should return false");
        }

        [Test]
        public void IsSolutionRequestValid_RequestWillBeValid_ReturnsTrue()
        {
            // Arrange
            SudokuSolutionRequest request = new SudokuSolutionRequest() { currentBoard = new SudokuValue[9, 9] };

            // Act
            bool actual = _service.IsSolutionRequestValid(request);

            // Assert
            Assert.IsTrue(actual, "Should return true");
        }

        [Test]
        public void IsSolutionRequestValid_RequestWillBeInvalid_ReturnsFalse()
        {
            // Arrange
            SudokuSolutionRequest request = new SudokuSolutionRequest() { currentBoard = new SudokuValue[2, 2] };

            // Act
            bool actual = _service.IsSolutionRequestValid(request);

            // Assert
            Assert.IsFalse(actual, "Should return false");
        }

        [Test]
        public void IsAnswerRequestValid_RequestWillBeValid_ReturnsTrue()
        {
            // Arrange
            SudokuAnswerRequest request = new SudokuAnswerRequest()
            {
                edittedBoard = new SudokuValue[9, 9],
                originalBoard = new SudokuValue[9, 9]
            };

            // Act
            bool actual = _service.IsAnswerRequestValid(request);

            // Assert
            Assert.IsTrue(actual, "Should return true");
        }

        [Test]
        public void IsAnswerRequestValid_RequestWillBeInvalid_ReturnsFalse()
        {
            // Arrange
            SudokuAnswerRequest request = new SudokuAnswerRequest()
            {
                edittedBoard = new SudokuValue[1, 2],
                originalBoard = new SudokuValue[3, 9]
            };

            // Act
            bool actual = _service.IsAnswerRequestValid(request);

            // Assert
            Assert.IsFalse(actual, "Should return false");
        }

        [Test]
        public void IsAnswerCorrect_AnswerShouldBeCorrect_ReturnsTrue()
        {
            // Arrange
            SudokuAnswerRequest answerRequest = new SudokuAnswerRequest();
            answerRequest.originalBoard = GetValidFormattedBoard();

            SudokuSolutionRequest solutionRequest = new SudokuSolutionRequest() { currentBoard = GetValidFormattedBoard() };
            answerRequest.edittedBoard = _service.FindSolution(solutionRequest).solution;

            // Act
            bool actual = _service.IsAnswerCorrect(answerRequest);

            // Assert
            Assert.IsTrue(actual, "Should return true");
        }

        [Test]
        public void IsAnswerCorrect_AnswerShouldBeIncorrect_ReturnsFalse()
        {
            // Arrange
            SudokuAnswerRequest answerRequest = new SudokuAnswerRequest();
            answerRequest.originalBoard = GetValidFormattedBoard();
            answerRequest.edittedBoard = GetValidFormattedBoard();

            // Act
            bool actual = _service.IsAnswerCorrect(answerRequest);

            // Assert
            Assert.IsFalse(actual, "Should return false");
        }

        [Test]
        public void CheckIfSudokuBoardsAreEqual_BoardsShouldBeEqual_ReturnsTrue()
        {
            // Arrange
            SudokuValue[,] board1 = GetValidFormattedBoard();
            SudokuValue[,] board2 = GetValidFormattedBoard();

            // Act
            bool actual = _service.CheckIfSudokuBoardsAreEqual(board1, board2);

            // Assert
            Assert.IsTrue(actual, "Should return true");
        }

        [Test]
        public void CheckIfSudokuBoardsAreEqual_BoardsShouldNotBeEqual_ReturnsFalse()
        {
            // Arrange
            SudokuValue[,] board1 = GetValidFormattedBoard();

            SudokuValue[,] board2 = GetValidFormattedBoard();
            board2[0, 0].value = 10;

            // Act
            bool actual = _service.CheckIfSudokuBoardsAreEqual(board1, board2);

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
            return _service.FormatSudokuBoard(unformattedValidBoard);
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
            return _service.FormatSudokuBoard(unformattedInvalidBoard);
        }
    }
}
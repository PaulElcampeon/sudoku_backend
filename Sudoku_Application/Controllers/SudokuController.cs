using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sudoku_Application.Models;
using Sudoku_Application.Services;

namespace Sudoku_Application.Controllers
{
    [Route("api/sudoku/")]
    [ApiController]
    public class SudokuController : ControllerBase
    {
        private readonly ISudokuService _service;

        public SudokuController(ISudokuService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("get-solution")]
        public ActionResult<SudokuSolution> GetSolution(SudokuSolutionRequest solutionRequest)
        {
            if (_service.IsSolutionRequestValid(solutionRequest))
            {
                SudokuSolution solution = _service.FindSolution(solutionRequest);

                return solution;
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("check-answer")]
        public ActionResult<SudokuSolution> Submit(SudokuAnswerRequest answerRequest)
        {
            if (_service.IsAnswerRequestValid(answerRequest))
            {
                bool isCorrect = _service.IsAnswerCorrect(answerRequest);

                return new SudokuSolution() { isSuccessful = isCorrect };
            }

            return BadRequest();
        }
    }
}
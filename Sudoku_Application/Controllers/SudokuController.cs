using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sudoku_Application.Models;
using Sudoku_Application.Service;

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
        [Route("solution")]
        public ActionResult<SudokuSolution> GetSolution(SudokuSolutionRequest solutionRequest)
        {
            if (_service.IsRequestValid(solutionRequest))
            {
                SudokuSolution solution = _service.FindSolution(solutionRequest);

                return solution;
            }

            return BadRequest();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sudoku_Application.Models;

namespace Sudoku_Application.Controllers
{
    [Route("api/sudoku")]
    [ApiController]
    public class SudokuController : ControllerBase
    {
        [HttpPost]
        public ActionResult<SudokuSolution> GetSolution()
        {

        }
    }
}
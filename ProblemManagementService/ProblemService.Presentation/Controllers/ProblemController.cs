using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.Service.Interfaces;

namespace ProblemService.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProblemByIdAsync(int id)
        {
            var result = await _problemService.GetProblemByIDAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProblem(CreateProblemDto problemDto)
        {
           var result = await _problemService.AddProblemAsync(problemDto);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllProblem()
        {
            var result =  await _problemService.GetAllProblemAsync();
            return Ok(result);
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateProblem(ProblemDto problemDto)
        {

            var result = await _problemService.UpdateProblemAsync(problemDto);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProblem(int Id)
        {

            var result = await _problemService.DeleteProblemAsync(Id);
            return Ok(result);
        }
    }
}

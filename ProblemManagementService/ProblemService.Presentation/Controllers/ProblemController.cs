using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemService.Application.DTOs.FilterDto;
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

        [HttpGet("list")]
        public async Task<ActionResult> GetListProblemByIdAsync([FromQuery] List<int> ids)
        {
            var result = await _problemService.GetListProblemByIdAsync(ids);
            return Ok(result);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateProblem(CreateProblemDto problemDto)
        {
           var result = await _problemService.AddProblemAsync(problemDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProblem([FromQuery] FilterDto filter)
        {
            var result =  await _problemService.GetAllProblemAsync(filter);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateProblem(ProblemDtoDetail problemDtoDetail)
        {

            var result = await _problemService.UpdateProblemAsync(problemDtoDetail);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProblem(int Id)
        {

            var result = await _problemService.DeleteProblemAsync(Id);
            return Ok(result);
        }
    }
}

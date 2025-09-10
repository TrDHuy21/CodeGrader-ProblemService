using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemService.Application.DTOs.InOutExampleDto;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.Service.Interfaces;

namespace ProblemService.Presentation.Controllers
{
    [Route("api/problem/[controller]")]
    [ApiController]
    public class InOutExampleController : ControllerBase
    {
        private readonly IInOutExampleService _inOutExampleService;

        public InOutExampleController(IInOutExampleService inOutExampleService)
        {
            _inOutExampleService = inOutExampleService;
        }

        [HttpGet("GetByProblemId/{id}")]
        public async Task<ActionResult> GetInOutExampleByProblemIdAsync(int id)
        {
            var result = await _inOutExampleService.GetInOutExampleByProblemIDAsync(id);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateInOutExample(CreateInOutExampleDto inOutExampleDto)
        {
            var result = await _inOutExampleService.AddInOutExampleAsync(inOutExampleDto);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllInOutExample()
        {
            var result = await _inOutExampleService.GetAllInOutExampleAsync();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut()]
        public async Task<ActionResult> UpdateInOutExample(InOutExampleDtoDetail inOutExampleDto)
        {

            var result = await _inOutExampleService.UpdateInOutExampleAsync(inOutExampleDto);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteInOutExample(int Id)
        {

            var result = await _inOutExampleService.DeleteInOutExampleAsync(Id);
            return Ok(result);
        }
    }
}

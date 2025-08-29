using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemService.Application.DTOs.ProblemTagDto;
using ProblemService.Application.DTOs.TagDto;
using ProblemService.Application.Service.Interfaces;

namespace ProblemService.Presentation.Controllers
{
    [Route("api/problem/[controller]")]
    [ApiController]
    public class ProblemTagController : ControllerBase
    {
        private readonly IProblemTagService _problemTagService;

        public ProblemTagController(IProblemTagService problemTagService)
        {
            _problemTagService = problemTagService;
        }

        [HttpGet("GetProblemTag")]
        public ActionResult GetProblemTagByIdAsync([FromQuery] int ProblemId , [FromQuery] int TagId)
        {
            var result = _problemTagService.GetProblemTagById(ProblemId,TagId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateProblemTag(CreateProblemTagDto problemTagDto)
        {
            var result = await _problemTagService.AddProblemTagAsync(problemTagDto);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public ActionResult GetAllProblemTag()
        {
            var result = _problemTagService.GetAllProblemTag();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProblemTag(int ProblemId, int TagId)
        {

            var result = await _problemTagService.DeleteProblemTagAsync(ProblemId,TagId);
            return Ok(result);
        }
    }
}

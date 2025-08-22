using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemService.Application.DTOs.ProblemTagDto;
using ProblemService.Application.DTOs.TagDto;
using ProblemService.Application.Service.Interfaces;

namespace ProblemService.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemTagController : ControllerBase
    {
        private readonly IProblemTagService _problemTagService;

        public ProblemTagController(IProblemTagService problemTagService)
        {
            _problemTagService = problemTagService;
        }

        [HttpGet("GetProblemById")]
        public ActionResult GetProblemTagByIdAsync(int ProblemId , int TagId)
        {
            var result = _problemTagService.GetProblemTagById(ProblemId,TagId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTag(CreateProblemTagDto problemTagDto)
        {
            var result = await _problemTagService.AddProblemTagAsync(problemTagDto);
            return Ok(result);
        }

        [HttpGet("GetAllTag")]
        public ActionResult GetAllTag()
        {
            var result = _problemTagService.GetAllProblemTag();
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteTag(int ProblemId, int TagId)
        {

            var result = await _problemTagService.DeleteProblemTagAsync(ProblemId,TagId);
            return Ok(result);
        }
    }
}

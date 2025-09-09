using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.DTOs.TagDto;
using ProblemService.Application.Service.Interfaces;

namespace ProblemService.Presentation.Controllers
{
    [Route("api/problem/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTagByIdAsync(int id)
        {
            var result = await _tagService.GetTagByIDAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateTag(CreateTagDto tagDto)
        {
            var result = await _tagService.AddTagAsync(tagDto);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllTag()
        {
            var result = await _tagService.GetAllTagAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut()]
        public async Task<ActionResult> UpdateTag(TagDtoDetail tagDto)
        {

            var result = await _tagService.UpdateTagAsync(tagDto);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteTag(int Id)
        {

            var result = await _tagService.DeleteTagAsync(Id);
            return Ok(result);
        }

        [HttpGet("GetTagsNotInProblem/{Id}")]
        public async Task<ActionResult> GetTagsNotInProblem(int Id)
        {

            var result = await _tagService.GetTagsNotInProblem(Id);
            return Ok(result);
        }
    }
}

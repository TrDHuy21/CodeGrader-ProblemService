using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.DTOs.TagDto;
using ProblemService.Application.Service.Interfaces;

namespace ProblemService.Presentation.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<ActionResult> CreateTag(CreateTagDto tagDto)
        {
            var result = await _tagService.AddTagAsync(tagDto);
            return Ok(result);
        }

        [HttpGet("GetAllTag")]
        public async Task<ActionResult> GetAllTag()
        {
            var result = await _tagService.GetAllTagAsync();
            return Ok(result);
        }

        [HttpPut("UpdateTag")]
        public async Task<ActionResult> UpdateTag(TagDto tagDto)
        {

            var result = await _tagService.UpdateTagAsync(tagDto);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteTag(int Id)
        {

            var result = await _tagService.DeleteTagAsync(Id);
            return Ok(result);
        }
    }
}

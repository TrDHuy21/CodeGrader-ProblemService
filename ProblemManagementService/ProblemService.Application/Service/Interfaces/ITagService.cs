using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using ProblemService.Application.DTOs.TagDto;


namespace ProblemService.Application.Service.Interfaces
{
    public interface ITagService
    {
        Task<Result<IEnumerable<TagDto>>> GetAllTagAsync();
        Task<Result<TagDto>> GetTagByIDAsync(int id);
        Task<Result<CreateTagDto>> AddTagAsync(CreateTagDto tagDto);
        Task<Result<TagDto>> DeleteTagAsync(int id);
        Task<Result<TagDtoDetail>> UpdateTagAsync(TagDtoDetail tagDto);
    }
}

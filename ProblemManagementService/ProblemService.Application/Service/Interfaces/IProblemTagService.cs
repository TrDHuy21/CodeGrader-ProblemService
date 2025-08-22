using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.DTOs.ProblemTagDto;

namespace ProblemService.Application.Service.Interfaces
{
    public interface IProblemTagService
    {
        Result<IEnumerable<ProblemTagDtoDetail>> GetAllProblemTag();
        Result<ProblemTagDtoDetail> GetProblemTagById(int ProblemId, int TagId);
        Task<Result<CreateProblemTagDto>> AddProblemTagAsync(CreateProblemTagDto problemTagDto);
        Task<Result<ProblemTagDto>> DeleteProblemTagAsync(int ProblemId , int TagId);
    }
}

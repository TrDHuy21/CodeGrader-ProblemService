using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using ProblemService.Application.DTOs.FilterDto;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Domain.Entities;

namespace ProblemService.Application.Service.Interfaces
{
    public interface IProblemService
    {
        Task<Result<IEnumerable<ProblemDtoDetail>>> GetAllProblemAsync(FilterDto filter);
        Task<Result<ProblemDtoDetail>> GetProblemByIDAsync(int id);
        Task<Result<CreateProblemDto>> AddProblemAsync(CreateProblemDto problemDto);
        Task<Result<ProblemDto>> DeleteProblemAsync(int id);
        Task<Result<ProblemDtoDetail>> UpdateProblemAsync(ProblemDtoDetail problemDtoDetail);
    }
}

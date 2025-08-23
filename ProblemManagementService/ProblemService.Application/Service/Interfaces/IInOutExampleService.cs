using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using ProblemService.Application.DTOs.InOutExampleDto;
using ProblemService.Application.DTOs.ProblemDto;

namespace ProblemService.Application.Service.Interfaces
{
    public interface IInOutExampleService
    {
        Task<Result<IEnumerable<InOutExampleDto>>> GetAllInOutExampleAsync();
        Task<Result<IEnumerable<InOutExampleDto>>> GetInOutExampleByProblemIDAsync(int id);
        Task<Result<CreateInOutExampleDto>> AddInOutExampleAsync(CreateInOutExampleDto inOutExampleDto);
        Task<Result<InOutExampleDto>> DeleteInOutExampleAsync(int id);
        Task<Result<InOutExampleDto>> UpdateInOutExampleAsync(InOutExampleDto inOutExampleDto);
    }
}

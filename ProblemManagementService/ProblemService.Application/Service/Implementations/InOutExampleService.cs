using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using ProblemService.Application.DTOs.InOutExampleDto;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.Service.Interfaces;
using ProblemService.Application.Validations;
using ProblemService.Domain.Entities;
using ProblemService.Infrastructure.UnitOfWork;

namespace ProblemService.Application.Service.Implementations
{
    public class InOutExampleService : IInOutExampleService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InOutExampleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CreateInOutExampleDto>> AddInOutExampleAsync(CreateInOutExampleDto inOutExampleDto)
        {
            try
            {

                //Validate
                var problemContain = await _unitOfWork.Problems.GetByIDAsync(inOutExampleDto.ProblemId);
                if (problemContain == null) {
                    return Result<CreateInOutExampleDto>.Failure("Invalid Problem Id", new List<ErrorField>());
                }
                List<ErrorField> errors = new List<ErrorField>();
                errors.Add(InOutExampleValidation.ValidInput(inOutExampleDto.InputExample));
                errors.Add(InOutExampleValidation.ValidOutput(inOutExampleDto.OutputExample));
                errors.Add(InOutExampleValidation.ValidExplanation(inOutExampleDto.Explanation));
                foreach (ErrorField error in errors)
                {
                    if (error.Message.Count > 0)
                    {
                        return Result<CreateInOutExampleDto>.Failure("Validation error", errors);
                    }
                }
                var inoutex = _mapper.Map<InOutExample>(inOutExampleDto);
                await _unitOfWork.InOutExamples.AddAsync(inoutex);
                await _unitOfWork.SaveChangeAsync();
                return Result<CreateInOutExampleDto>.Success(inOutExampleDto);
            }
            catch (Exception ex)
            {
                return Result<CreateInOutExampleDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<InOutExampleDto>> DeleteInOutExampleAsync(int id)
        {
            try
            {
                var inOutExample = await _unitOfWork.InOutExamples.GetByIDAsync(id);
                if (inOutExample == null)
                {
                    return Result<InOutExampleDto>.Failure("Invalid Id", new List<ErrorField>());
                }
                //await _unitOfWork.Problems.Delete(problem);
                inOutExample.IsDelete = true;
                await _unitOfWork.InOutExamples.Update(inOutExample);
                await _unitOfWork.SaveChangeAsync();
                var inOutExDto = _mapper.Map<InOutExampleDto>(inOutExample);
                return Result<InOutExampleDto>.Success(inOutExDto);
            }
            catch (Exception ex)
            {
                return Result<InOutExampleDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<IEnumerable<InOutExampleDto>>> GetAllInOutExampleAsync()
        {
            try
            {
                var inOuts = await _unitOfWork.InOutExamples.GetAllAsync();
                var inOutsDto = _mapper.Map<IEnumerable<InOutExampleDto>>(inOuts);
                return Result<IEnumerable<InOutExampleDto>>.Success(inOutsDto);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<InOutExampleDto>>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<IEnumerable<InOutExampleDto>>> GetInOutExampleByProblemIDAsync(int id)
        {
            try
            {
                var inOuts = await _unitOfWork.InOutExamples.GetInOutExampleByProblemIdAsync(id);
                var inOutsDto = _mapper.Map<IEnumerable<InOutExampleDto>>(inOuts);
                return Result<IEnumerable<InOutExampleDto>>.Success(inOutsDto);

            }
            catch (Exception ex) {
                return Result<IEnumerable<InOutExampleDto>>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<InOutExampleDtoDetail>> UpdateInOutExampleAsync(InOutExampleDtoDetail inOutExampleDto)
        {
            try
            {
                var inOutExist = await _unitOfWork.InOutExamples.GetByIDAsync(inOutExampleDto.Id);
                if (inOutExist == null)
                {
                    return Result<InOutExampleDtoDetail>.Failure("Invalid Id", new List<ErrorField>());
                }
                var problemContain = await _unitOfWork.Problems.GetByIDAsync(inOutExampleDto.ProblemId);
                if (problemContain == null)
                {
                    return Result<InOutExampleDtoDetail>.Failure("Invalid Problem Id", new List<ErrorField>());
                }
                //Validate
                List<ErrorField> errors = new List<ErrorField>();
                errors.Add(InOutExampleValidation.ValidInput(inOutExampleDto.InputExample));
                errors.Add(InOutExampleValidation.ValidOutput(inOutExampleDto.OutputExample));
                errors.Add(InOutExampleValidation.ValidExplanation(inOutExampleDto.Explanation));
                foreach (ErrorField error in errors)
                {
                    if (error.Message.Count > 0)
                    {
                        return Result<InOutExampleDtoDetail>.Failure("Validation error", errors);
                    }
                }
                var inOut = _mapper.Map<InOutExample>(inOutExampleDto);
                await _unitOfWork.InOutExamples.Update(inOut);
                await _unitOfWork.SaveChangeAsync();
                return Result<InOutExampleDtoDetail>.Success(inOutExampleDto);
            }
            catch (Exception ex)
            {
                return Result<InOutExampleDtoDetail>.Failure(ex.Message, new List<ErrorField>());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.Service.Interfaces;
using ProblemService.Application.Validations;
using ProblemService.Domain.Entities;
using ProblemService.Infrastructure.UnitOfWork;

namespace ProblemService.Application.Service.Implementations
{
    public class ProblemService : IProblemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProblemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CreateProblemDto>> AddProblemAsync(CreateProblemDto problemDto)
        {
            try
            {
                //Validate
                List<ErrorField> errors = new List<ErrorField>();
                errors.Add(ProblemValidation.ValidName(problemDto.Name));
                errors.Add(ProblemValidation.ValidContent(problemDto.Content));
                errors.Add(ProblemValidation.ValidLevel(problemDto.Level));
                errors.Add(ProblemValidation.ValidPromt(problemDto.Promt));
                foreach (ErrorField error in errors) {
                    if (error.Message.Count > 0) { 
                        return Result<CreateProblemDto>.Failure("Validation error",errors);
                    }
                }
                var problem = _mapper.Map<Problem>(problemDto);
                await _unitOfWork.Problems.AddAsync(problem);
                await _unitOfWork.SaveChangeAsync();
                return Result<CreateProblemDto>.Success(problemDto);
            }
            catch (Exception ex) {
                return Result<CreateProblemDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<ProblemDto>> DeleteProblemAsync(int id)
        {
            try
            {
                var problem = await _unitOfWork.Problems.GetByIDAsync(id);
                if (problem == null) {
                    return Result<ProblemDto>.Failure("Invalid Id", new List<ErrorField>());
                }
                //await _unitOfWork.Problems.Delete(problem);
                problem.IsDelete = true;
                await _unitOfWork.Problems.Update(problem);
                await _unitOfWork.SaveChangeAsync();
                var problemDto = _mapper.Map<ProblemDto>(problem);
                return Result<ProblemDto>.Success(problemDto);
            }
            catch(Exception ex) {
                return Result<ProblemDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<IEnumerable<ProblemDto>>> GetAllProblemAsync()
        {
            try
            {
                var problems = await _unitOfWork.Problems.GetAllAsync();
                var problemsDto = _mapper.Map<IEnumerable<ProblemDto>>(problems);
                return Result<IEnumerable<ProblemDto>>.Success(problemsDto);
            }
            catch (Exception ex) {
                return Result<IEnumerable<ProblemDto>>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<ProblemDto>> GetProblemByIDAsync(int id)
        {
            try
            {
                var problem = await _unitOfWork.Problems.GetByIDAsync(id);
                if (problem == null) {
                    return Result<ProblemDto>.Failure("Invalid Id", new List<ErrorField>());
                }
                var problemDto = _mapper.Map<ProblemDto>(problem);
                return Result<ProblemDto>.Success(problemDto);

            }
            catch (Exception ex) {
                return Result<ProblemDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<ProblemDto>> UpdateProblemAsync(ProblemDto problemDto)
        {
            try
            {
                var problemExist = await _unitOfWork.Problems.GetByIDAsync(problemDto.Id);
                if (problemExist == null) {
                    return Result<ProblemDto>.Failure("Invalid Id", new List<ErrorField>());
                }
                //Validate
                List<ErrorField> errors = new List<ErrorField>();
                errors.Add(ProblemValidation.ValidName(problemDto.Name));
                errors.Add(ProblemValidation.ValidContent(problemDto.Content));
                errors.Add(ProblemValidation.ValidLevel(problemDto.Level));
                errors.Add(ProblemValidation.ValidPromt(problemDto.Promt));
                foreach (ErrorField error in errors)
                {
                    if (error.Message.Count > 0)
                    {
                        return Result<ProblemDto>.Failure("Validation error", errors);
                    }
                }
                var problem = _mapper.Map<Problem>(problemDto);
                await _unitOfWork.Problems.Update(problem);
                await _unitOfWork.SaveChangeAsync();
                return Result<ProblemDto>.Success(problemDto);
            }
            catch (Exception ex) {
                return Result<ProblemDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }
    }
}

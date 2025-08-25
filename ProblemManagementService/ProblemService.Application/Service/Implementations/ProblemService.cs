using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Microsoft.EntityFrameworkCore;
using ProblemService.Application.DTOs.FilterDto;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.Service.Interfaces;
using ProblemService.Application.Validations;
using ProblemService.Domain.Entities;
using ProblemService.Infrastructure.UnitOfWork;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using ProblemService.Application.DTOs.InOutExampleDto;
using ProblemService.Application.DTOs.TagDto;

namespace ProblemService.Application.Service.Implementations
{
    public class ProblemService : IProblemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProblemService> _logger;
        private readonly ITagService _tagService;
        private readonly IInOutExampleService _inoutExampleService;
        public ProblemService(IUnitOfWork unitOfWork, IMapper mapper , ILogger<ProblemService> logger,ITagService tagService , IInOutExampleService inOutExampleService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _tagService = tagService;
            _inoutExampleService = inOutExampleService;
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

        public async Task<Result<IEnumerable<ProblemDtoDetail>>> GetAllProblemAsync(FilterDto filter)
        {
            try
            {
                //filter
                var problems = _unitOfWork.Problems.GetAll();
                if (!String.IsNullOrEmpty(filter.NameSearch?.Trim()))
                {
                    problems = problems.Where(p => p.Name.Contains(filter.NameSearch));
                }
                if (!string.IsNullOrWhiteSpace(filter.SortBy))
                {
                    var sortExpression = filter.IsDecending
                        ? $"{filter.SortBy} descending"
                        : filter.SortBy;

                    problems = problems.OrderBy(sortExpression);
                }
                problems = problems.Take(filter.PageSize).Skip(filter.PageNumber*(filter.PageNumber-1));
                var problemsList = await problems.ToListAsync();
                var problemsDto = _mapper.Map<IEnumerable<ProblemDtoDetail>>(problemsList);
                return Result<IEnumerable<ProblemDtoDetail>>.Success(problemsDto);
            }
            catch (Exception ex) {
                return Result<IEnumerable<ProblemDtoDetail>>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<ProblemDtoDetail>> GetProblemByIDAsync(int id)
        {
            try
            {
                var query = _unitOfWork.Problems.GetAll().Where(p => p.Id == id);
                var problem = await query.FirstOrDefaultAsync();
                if (problem == null) {
                    return Result<ProblemDtoDetail>.Failure("Invalid Id", new List<ErrorField>());
                }
                var problemDto = _mapper.Map<ProblemDtoDetail>(problem);
                return Result<ProblemDtoDetail>.Success(problemDto);

            }
            catch (Exception ex) {
                return Result<ProblemDtoDetail>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<ProblemDtoDetail>> UpdateProblemAsync(ProblemDtoDetail problemDtoDetail)
        {
            try
            {
                var problemExist = await _unitOfWork.Problems.GetByIDAsync(problemDtoDetail.Id);
                if (problemExist == null)
                {
                    return Result<ProblemDtoDetail>.Failure("Invalid Id", new List<ErrorField>());
                }

                //Validate
                List<ErrorField> errors = new List<ErrorField>();
                errors.Add(ProblemValidation.ValidName(problemDtoDetail.Name));
                errors.Add(ProblemValidation.ValidContent(problemDtoDetail.Content));
                errors.Add(ProblemValidation.ValidLevel(problemDtoDetail.Level));
                errors.Add(ProblemValidation.ValidPromt(problemDtoDetail.Promt));
                foreach (ErrorField error in errors)
                {
                    if (error.Message.Count > 0)
                    {
                        return Result<ProblemDtoDetail>.Failure("Validation error", errors);
                    }
                }
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    //Update problem
                    ProblemDto problemDto = new ProblemDto();
                    problemDto.Id = problemDtoDetail.Id;
                    problemDto.Name = problemDtoDetail.Name;
                    problemDto.Content = problemDtoDetail.Content;
                    problemDto.Level = problemDtoDetail.Level;
                    problemDto.Promt = problemDtoDetail.Promt;
                    var problem = _mapper.Map<Problem>(problemDto);
                    await _unitOfWork.Problems.Update(problem);

                    foreach (InOutExampleDtoDetail inOutExampleDto in problemDtoDetail.inOutExamples)
                    {
                        var result = await _inoutExampleService.UpdateInOutExampleAsync(inOutExampleDto);
                        if (!result.isSuccess)
                        {
                            _unitOfWork.RollbackTransactionAsync();
                            var totalResult = new Result<ProblemDtoDetail>(result.isSuccess,null,result.ErrorMessage,result.detail);
                            return totalResult;
                        }
                    }

                    foreach(TagDtoDetail tagDto in problemDtoDetail.tags)
                    {
                        var result = await _tagService.UpdateTagAsync(tagDto);
                        if (!result.isSuccess)
                        {
                            await _unitOfWork.RollbackTransactionAsync();
                            var totalResult = new Result<ProblemDtoDetail>(result.isSuccess, null, result.ErrorMessage, result.detail);
                            return totalResult;
                        }
                    }

                    await _unitOfWork.CommitTransactionAsync();

                }
                catch (Exception ex) {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<ProblemDtoDetail>.Failure(ex.Message, new List<ErrorField>());
                }
                return Result<ProblemDtoDetail>.Success(problemDtoDetail);
            }
            catch (Exception ex) {
                return Result<ProblemDtoDetail>.Failure(ex.Message, new List<ErrorField>());
            }
        }
    }
}

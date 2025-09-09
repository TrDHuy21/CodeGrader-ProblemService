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
                ErrorDetail errorDetail = new ErrorDetail();
                var errorDetailName = ProblemValidation.ValidName(problemDto.Name);
                if (!string.IsNullOrWhiteSpace(errorDetailName.ErrorMessage))
                {
                    errorDetail.Errors.Add(errorDetailName);
                }
                var errorDetailContent = ProblemValidation.ValidContent(problemDto.Content);
                if (!string.IsNullOrWhiteSpace(errorDetailContent.ErrorMessage))
                {
                    errorDetail.Errors.Add(errorDetailContent);
                }
                var errorDetailLevel = ProblemValidation.ValidLevel(problemDto.Level);
                if (!string.IsNullOrWhiteSpace(errorDetailLevel.ErrorMessage))
                {
                    errorDetail.Errors.Add(errorDetailLevel);
                }
                var errorDetailPromt = ProblemValidation.ValidPromt(problemDto.Promt);
                if (!string.IsNullOrWhiteSpace(errorDetailPromt.ErrorMessage))
                {
                    errorDetail.Errors.Add(errorDetailPromt);
                }

                if (errorDetail.Errors.Count > 0)
                {
                    return Result<CreateProblemDto>.Failure("Validation error", errorDetail);
                }


                var problem = _mapper.Map<Problem>(problemDto);
                await _unitOfWork.Problems.AddAsync(problem);
                await _unitOfWork.SaveChangeAsync();
                return Result<CreateProblemDto>.Success(problemDto);
            }
            catch (Exception ex) {
                return Result<CreateProblemDto>.Failure(ex.Message, new ErrorDetail());
            }
        }

        public async Task<Result<int>> countTotalProblem(FilterDto filter)
        {
            try
            {
                //filter
                var problems = _unitOfWork.Problems.GetAll();
                if (!String.IsNullOrEmpty(filter.NameSearch?.Trim()))
                {
                    problems = problems.Where(p => p.Name.Contains(filter.NameSearch));
                }
                if (filter.Levels.Count != 0)
                {
                    problems = problems.Where(p => filter.Levels.Contains(p.Level));
                }
                if (filter.Tagnames.Count != 0)
                {
                    var listTags = filter.Tagnames.Select(t => t.Trim().ToLower());
                    var requestTag = _unitOfWork.Tags.GetAll().Where(t => listTags.Contains(t.Name.Trim().ToLower())).Select(t => t.Id);
                    var listTagID = await requestTag.ToListAsync();
                    problems = problems.Where(p => listTagID.All(tagId => p.ProblemTags.Any(pt => pt.TagId == tagId)));
                }
                var problemsList = await problems.ToListAsync();
                if (filter.Levels.Count == 0 || filter.PageNumber == 0)
                {
                    problemsList = [];
                }
                return Result<int>.Success(problemsList.Count);
            }
            catch (Exception ex) {
                return Result<int>.Failure(ex.Message, new ErrorDetail());
            }
        }

        public async Task<Result<ProblemDto>> DeleteProblemAsync(int id)
        {
            try
            {
                var problem = await _unitOfWork.Problems.GetByIDAsync(id);
                if (problem == null) {
                    return Result<ProblemDto>.Failure("Invalid Id", new ErrorDetail());
                }
                var request = await _unitOfWork.ProblemTags.GetAllAsync();
                var problemTags = request.Where(pt => pt.ProblemId == id);
                foreach (var problemTag in problemTags) {
                    await _unitOfWork.ProblemTags.Delete(problemTag);
                }
                await _unitOfWork.Problems.Delete(problem);
                await _unitOfWork.SaveChangeAsync();
                var problemDto = _mapper.Map<ProblemDto>(problem);
                return Result<ProblemDto>.Success(problemDto);
            }
            catch(Exception ex) {
                return Result<ProblemDto>.Failure(ex.Message, new ErrorDetail());
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
                if(filter.Levels.Count !=0)
                {
                    problems = problems.Where(p => filter.Levels.Contains(p.Level));
                }
                if(filter.Tagnames.Count !=0)
                {
                    var listTags = filter.Tagnames.Select(t => t.Trim().ToLower());
                    var requestTag = _unitOfWork.Tags.GetAll().Where(t => listTags.Contains(t.Name.Trim().ToLower())).Select(t => t.Id);
                    var listTagID = await requestTag.ToListAsync();
                    problems = problems.Where(p => listTagID.All(tagId => p.ProblemTags.Any(pt => pt.TagId == tagId)));
                }
                if (!string.IsNullOrWhiteSpace(filter.SortBy))
                {
                    var sortExpression = filter.IsDecending
                        ? $"{filter.SortBy} descending"
                        : filter.SortBy;

                    problems = problems.OrderBy(sortExpression);
                }
                problems = problems.Skip(filter.PageSize * (filter.PageNumber-1)).Take(filter.PageSize);
                var problemsList = await problems.ToListAsync();
                if(filter.PageNumber == 0)
                {
                    problemsList = [];
                }
                var problemsDto = _mapper.Map<IEnumerable<ProblemDtoDetail>>(problemsList);
                return Result<IEnumerable<ProblemDtoDetail>>.Success(problemsDto);
            }
            catch (Exception ex) {
                return Result<IEnumerable<ProblemDtoDetail>>.Failure(ex.Message, new ErrorDetail());
            }
        }

        public async Task<Result<IEnumerable<ProblemDtoForBookmarkService>>> GetListProblemByIdAsync(List<int> ids)
        {
            try
            {
                if (ids.Count == 0 || ids == null)
                {
                    return Result<IEnumerable<ProblemDtoForBookmarkService>>.Failure("List ID is empty", new ErrorDetail());
                }
                var query = _unitOfWork.Problems.GetAll().Where(p => ids.Contains(p.Id)).OrderBy(p => p.Id);
                var problems = await query.ToListAsync();
                var problemsDto = _mapper.Map<IEnumerable<ProblemDtoForBookmarkService>>(problems);
                return Result<IEnumerable<ProblemDtoForBookmarkService>>.Success(problemsDto);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ProblemDtoForBookmarkService>>.Failure(ex.Message, new ErrorDetail());
            }
        }

        public async Task<Result<ProblemDtoDetail>> GetProblemByIDAsync(int id)
        {
            try
            {
                var query = _unitOfWork.Problems.GetAll().Where(p => p.Id == id);
                var problem = await query.FirstOrDefaultAsync();
                if (problem == null) {
                    return Result<ProblemDtoDetail>.Failure("Invalid Id", new ErrorDetail());
                }
                var problemDto = _mapper.Map<ProblemDtoDetail>(problem);
                return Result<ProblemDtoDetail>.Success(problemDto);

            }
            catch (Exception ex) {
                return Result<ProblemDtoDetail>.Failure(ex.Message, new ErrorDetail());
            }
        }

        public async Task<Result<ProblemDtoDetail>> UpdateProblemAsync(ProblemDtoDetail problemDtoDetail)
        {
            try
            {
                var problemExist = await _unitOfWork.Problems.GetByIDAsync(problemDtoDetail.Id);
                if (problemExist == null)
                {
                    return Result<ProblemDtoDetail>.Failure("Problem Error : Invalid Id", new ErrorDetail());
                }

                //Validate
                ErrorDetail errorDetail = new ErrorDetail();
                var errorDetailName = ProblemValidation.ValidName(problemDtoDetail.Name);
                if (!string.IsNullOrWhiteSpace(errorDetailName.ErrorMessage))
                {
                    errorDetail.Errors.Add(errorDetailName);
                }
                var errorDetailContent = ProblemValidation.ValidContent(problemDtoDetail.Content);
                if (!string.IsNullOrWhiteSpace(errorDetailContent.ErrorMessage))
                {
                    errorDetail.Errors.Add(errorDetailContent);
                }
                var errorDetailLevel = ProblemValidation.ValidLevel(problemDtoDetail.Level);
                if (!string.IsNullOrWhiteSpace(errorDetailLevel.ErrorMessage))
                {
                    errorDetail.Errors.Add(errorDetailLevel);
                }
                var errorDetailPromt = ProblemValidation.ValidPromt(problemDtoDetail.Promt);
                if (!string.IsNullOrWhiteSpace(errorDetailPromt.ErrorMessage))
                {
                    errorDetail.Errors.Add(errorDetailPromt);
                }

                if (errorDetail.Errors.Count > 0)
                {
                    return Result<ProblemDtoDetail>.Failure("Validation error", errorDetail);
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
                    problemDto.IsDelete = problemDtoDetail.IsDelete;
                    var problem = _mapper.Map<Problem>(problemDto);
                    await _unitOfWork.Problems.Update(problem);

                    foreach (InOutExampleDtoDetail inOutExampleDto in problemDtoDetail.inOutExamples)
                    {
                        var result = await _inoutExampleService.UpdateInOutExampleAsync(inOutExampleDto);
                        if (!result.isSuccess)
                        {
                            _unitOfWork.RollbackTransactionAsync();
                            var totalResult = new Result<ProblemDtoDetail>(result.isSuccess,null,"InOutExample Error : "+result.Message,result.errorDetail);
                            return totalResult;
                        }
                    }

                    foreach(TagDtoDetail tagDto in problemDtoDetail.tags)
                    {
                        var result = await _tagService.UpdateTagAsync(tagDto);
                        if (!result.isSuccess)
                        {
                            await _unitOfWork.RollbackTransactionAsync();
                            var totalResult = new Result<ProblemDtoDetail>(result.isSuccess, null, "Tag Error : "+ result.Message, result.errorDetail);
                            return totalResult;
                        }
                    }

                    await _unitOfWork.CommitTransactionAsync();

                }
                catch (Exception ex) {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<ProblemDtoDetail>.Failure("Problem Error : "+ex.Message, new ErrorDetail());
                }
                return Result<ProblemDtoDetail>.Success(problemDtoDetail);
            }
            catch (Exception ex) {
                return Result<ProblemDtoDetail>.Failure("Problem Error : " + ex.Message, new ErrorDetail());
            }
        }
    }
}

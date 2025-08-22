using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.DTOs.ProblemTagDto;
using ProblemService.Application.Service.Interfaces;
using ProblemService.Domain.Entities;
using ProblemService.Infrastructure.UnitOfWork;

namespace ProblemService.Application.Service.Implementations
{
    public class ProblemTagService : IProblemTagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProblemTagService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CreateProblemTagDto>> AddProblemTagAsync(CreateProblemTagDto problemTagDto)
        {
            try
            {
                var problemExist = await _unitOfWork.Problems.GetByIDAsync(problemTagDto.ProblemId);
                var tagExist = await _unitOfWork.Tags.GetByIDAsync(problemTagDto.TagId);
                if(problemExist == null  || tagExist == null)
                {
                    return Result<CreateProblemTagDto>.Failure("Invalid Id", new List<ErrorField>());
                }
                var problemTag = _mapper.Map<ProblemTag>(problemTagDto);
                await _unitOfWork.ProblemTags.AddAsync(problemTag);
                await _unitOfWork.SaveChangeAsync();
                return Result<CreateProblemTagDto>.Success(problemTagDto);
            }
            catch (Exception ex) {
                return Result<CreateProblemTagDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<ProblemTagDto>> DeleteProblemTagAsync(int ProblemId, int TagId)
        {
            try
            {
                var problemTag = _unitOfWork.ProblemTags.GetProblemTagById(ProblemId,TagId);
                if (problemTag == null)
                {
                    return Result<ProblemTagDto>.Failure("Invalid Id", new List<ErrorField>());
                }
                
                //await _unitOfWork.ProblemTags.Delete(problemTag);
                problemTag.IsDelete = true;
                await _unitOfWork.ProblemTags.Update(problemTag);
                await _unitOfWork.SaveChangeAsync();
                var problemTagDto = _mapper.Map<ProblemTagDto>(problemTag);
                return Result<ProblemTagDto>.Success(problemTagDto);
            }
            catch (Exception ex) {
                return Result<ProblemTagDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public  Result<IEnumerable<ProblemTagDtoDetail>> GetAllProblemTag()
        {
            try
            {
                var problemTag = _unitOfWork.ProblemTags.GetAllInclude().ToList();
                if (problemTag == null || problemTag.Count == 0)
                {
                    return Result<IEnumerable<ProblemTagDtoDetail>>.Success(new List<ProblemTagDtoDetail>());

                }
                var problemTagDto = _mapper.Map<IEnumerable<ProblemTagDtoDetail>>(problemTag);
                return Result<IEnumerable<ProblemTagDtoDetail>>.Success(problemTagDto);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ProblemTagDtoDetail>>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public Result<ProblemTagDtoDetail> GetProblemTagById(int ProblemId, int TagId)
        {
            try
            {
                var problemTag = _unitOfWork.ProblemTags.GetProblemTagById(ProblemId, TagId);
                if(problemTag == null)
                {
                    return Result<ProblemTagDtoDetail>.Failure("Not Found", new List<ErrorField>());
                }
                var problemTagDto = _mapper.Map<ProblemTagDtoDetail>(problemTag);
                return Result<ProblemTagDtoDetail>.Success(problemTagDto);

            }
            catch (Exception ex) {
                return Result<ProblemTagDtoDetail>.Failure(ex.Message, new List<ErrorField>());
            }
        }

    }
}

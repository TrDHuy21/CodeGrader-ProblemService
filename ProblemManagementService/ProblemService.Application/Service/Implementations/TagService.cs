using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using ProblemService.Application.DTOs.InOutExampleDto;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.DTOs.TagDto;
using ProblemService.Application.Service.Interfaces;
using ProblemService.Application.Validations;
using ProblemService.Domain.Entities;
using ProblemService.Infrastructure.UnitOfWork;

namespace ProblemService.Application.Service.Implementations
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CreateTagDto>> AddTagAsync(CreateTagDto tagDto)
        {
            try
            {
                //Validate
                ErrorDetail errorDetail = new ErrorDetail();
                var errorDetailName = TagValidation.ValidName(tagDto.Name);
                if (!string.IsNullOrWhiteSpace(errorDetailName.ErrorMessage))
                {
                    errorDetail.Errors.Add(errorDetailName);
                }
                if (errorDetail.Errors.Count > 0)
                {
                    return Result<CreateTagDto>.Failure("Validation error", errorDetail);
                }


                var tag = _mapper.Map<Tag>(tagDto);
                await _unitOfWork.Tags.AddAsync(tag);
                await _unitOfWork.SaveChangeAsync();
                return Result<CreateTagDto>.Success(tagDto);
            }
            catch (Exception ex)
            {
                return Result<CreateTagDto>.Failure(ex.Message, new ErrorDetail());
            }
        }

        public async Task<Result<TagDto>> DeleteTagAsync(int id)
        {
            try
            {
                var tag = await _unitOfWork.Tags.GetByIDAsync(id);
                if (tag == null)
                {
                    return Result<TagDto>.Failure("Invalid Id", new ErrorDetail());
                }
                
                //tag.IsDelete = true;
                var request = await _unitOfWork.ProblemTags.GetAllAsync();
                var problemTags = request.Where(pt => pt.ProblemId == id);
                foreach (var problemTag in problemTags)
                {
                    await _unitOfWork.ProblemTags.Delete(problemTag);
                }
                await _unitOfWork.Tags.Delete(tag);
                await _unitOfWork.SaveChangeAsync();

                var tagDto = _mapper.Map<TagDto>(tag);
                return Result<TagDto>.Success(tagDto);
            }
            catch (Exception ex)
            {
                return Result<TagDto>.Failure(ex.Message, new ErrorDetail());
            }
        }

        public async Task<Result<IEnumerable<TagDto>>> GetAllTagAsync()
        {
            try
            {
                var tags = await _unitOfWork.Tags.GetAllAsync();
                var tagsDto = _mapper.Map<IEnumerable<TagDto>>(tags);
                return Result<IEnumerable<TagDto>>.Success(tagsDto);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<TagDto>>.Failure(ex.Message, new ErrorDetail());
            }
        }

        public async Task<Result<TagDto>> GetTagByIDAsync(int id)
        {
            try
            {
                var tag = await _unitOfWork.Tags.GetByIDAsync(id);
                if (tag == null)
                {
                    return Result<TagDto>.Failure("Invalid Id", new ErrorDetail());
                }
                var tagDto = _mapper.Map<TagDto>(tag);
                return Result<TagDto>.Success(tagDto);

            }
            catch (Exception ex)
            {
                return Result<TagDto>.Failure(ex.Message, new ErrorDetail());
            }
        }

        public async Task<Result<IEnumerable<TagDto>>> GetTagsNotInProblem(int problemId)
        {
            try
            {
                var problemTags = await _unitOfWork.ProblemTags.GetAllAsync();
                var listTag = problemTags.ToList().Where(pt => pt.ProblemId == problemId).Select(pt => pt.TagId);
                var tags = await _unitOfWork.Tags.GetAllAsync();
                var tagsAvalaible = tags.ToList().Where(t => !listTag.Contains(t.Id));
                var tagsDto = _mapper.Map<IEnumerable<TagDto>>(tagsAvalaible);
                return Result<IEnumerable<TagDto>>.Success(tagsDto);
            }
            catch (Exception ex) {

                return Result<IEnumerable<TagDto>>.Failure(ex.Message, new ErrorDetail());
            }
        }

        public async Task<Result<TagDtoDetail>> UpdateTagAsync(TagDtoDetail tagDto)
        {
            try
            {
                var tagExist = await _unitOfWork.Tags.GetByIDAsync(tagDto.Id);
                if (tagExist == null)
                {
                    return Result<TagDtoDetail>.Failure("Invalid Id", new ErrorDetail());
                }

                //Validate
                ErrorDetail errorDetail = new ErrorDetail();
                var errorDetailName = TagValidation.ValidName(tagDto.Name);
                if (!string.IsNullOrWhiteSpace(errorDetailName.ErrorMessage))
                {
                    errorDetail.Errors.Add(errorDetailName);
                }
                if (errorDetail.Errors.Count > 0)
                {
                    return Result<TagDtoDetail>.Failure("Validation error", errorDetail);
                }

                var tag = _mapper.Map<Tag>(tagDto);
                await _unitOfWork.Tags.Update(tag);
                await _unitOfWork.SaveChangeAsync();
                return Result<TagDtoDetail>.Success(tagDto);
            }
            catch (Exception ex)
            {
                return Result<TagDtoDetail>.Failure(ex.Message, new ErrorDetail());
            }
        }

    }
}

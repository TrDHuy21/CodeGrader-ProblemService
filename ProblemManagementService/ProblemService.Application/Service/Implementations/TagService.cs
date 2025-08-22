using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
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
                List<ErrorField> errors = new List<ErrorField>();
                errors.Add(TagValidation.ValidName(tagDto.Name));
                foreach (ErrorField error in errors)
                {
                    if (error.Message.Count > 0)
                    {
                        return Result<CreateTagDto>.Failure("Validation error", errors);
                    }
                }
                var tag = _mapper.Map<Tag>(tagDto);
                await _unitOfWork.Tags.AddAsync(tag);
                await _unitOfWork.SaveChangeAsync();
                return Result<CreateTagDto>.Success(tagDto);
            }
            catch (Exception ex)
            {
                return Result<CreateTagDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<TagDto>> DeleteTagAsync(int id)
        {
            try
            {
                var tag = await _unitOfWork.Tags.GetByIDAsync(id);
                if (tag == null)
                {
                    return Result<TagDto>.Failure("Invalid Id", new List<ErrorField>());
                }
                //await _unitOfWork.Tags.Delete(tag);
                tag.IsDelete = true;
                await _unitOfWork.Tags.Update(tag);
                await _unitOfWork.SaveChangeAsync();

                var tagDto = _mapper.Map<TagDto>(tag);
                return Result<TagDto>.Success(tagDto);
            }
            catch (Exception ex)
            {
                return Result<TagDto>.Failure(ex.Message, new List<ErrorField>());
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
                return Result<IEnumerable<TagDto>>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<TagDto>> GetTagByIDAsync(int id)
        {
            try
            {
                var tag = await _unitOfWork.Tags.GetByIDAsync(id);
                if (tag == null)
                {
                    return Result<TagDto>.Failure("Invalid Id", new List<ErrorField>());
                }
                var tagDto = _mapper.Map<TagDto>(tag);
                return Result<TagDto>.Success(tagDto);

            }
            catch (Exception ex)
            {
                return Result<TagDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }

        public async Task<Result<TagDto>> UpdateTagAsync(TagDto tagDto)
        {
            try
            {
                var tagExist = await _unitOfWork.Tags.GetByIDAsync(tagDto.Id);
                if (tagExist == null)
                {
                    return Result<TagDto>.Failure("Invalid Id", new List<ErrorField>());
                }

                //Validate
                List<ErrorField> errors = new List<ErrorField>();
                errors.Add(TagValidation.ValidName(tagDto.Name));
                foreach (ErrorField error in errors)
                {
                    if (error.Message.Count > 0)
                    {
                        return Result<TagDto>.Failure("Validation error", errors);
                    }
                }

                var tag = _mapper.Map<Tag>(tagDto);
                await _unitOfWork.Tags.Update(tag);
                await _unitOfWork.SaveChangeAsync();
                return Result<TagDto>.Success(tagDto);
            }
            catch (Exception ex)
            {
                return Result<TagDto>.Failure(ex.Message, new List<ErrorField>());
            }
        }
    }
}

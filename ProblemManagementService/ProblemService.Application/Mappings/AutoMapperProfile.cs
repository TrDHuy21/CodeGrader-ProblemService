using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.DTOs.TagDto;
using ProblemService.Domain.Entities;

namespace ProblemService.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Problem
            CreateMap<Problem, CreateProblemDto>().ReverseMap();
            CreateMap<Problem, ProblemDto>().ReverseMap();

            //Tag
            CreateMap<Tag,TagDto>().ReverseMap();
            CreateMap<Tag, CreateTagDto>().ReverseMap();

        }
    }
}

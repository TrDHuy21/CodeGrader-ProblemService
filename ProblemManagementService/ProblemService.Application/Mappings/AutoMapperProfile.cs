using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProblemService.Application.DTOs.InOutExampleDto;
using ProblemService.Application.DTOs.ProblemDto;
using ProblemService.Application.DTOs.ProblemTagDto;
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
            CreateMap<Problem, ProblemDtoDetail>()
                    .ForMember(pdt => pdt.Id, p => p.MapFrom(pr => pr.Id))
                    .ForMember(pdt => pdt.Name, p => p.MapFrom(pr => pr.Name))
                    .ForMember(pdt => pdt.Content, p => p.MapFrom(pr => pr.Content))
                    .ForMember(pdt => pdt.Level, p => p.MapFrom(pr => pr.Level))
                    .ForMember(pdt => pdt.Promt, p => p.MapFrom(pr => pr.Promt))
                    .ForMember(pdt => pdt.tags, p => p.MapFrom(pr => pr.ProblemTags.Select(pt => pt.Tag).ToList()))
                    .ForMember(pdt => pdt.inOutExamples, p => p.MapFrom(pr => pr.InOutExamples))
                    .ForMember(pdt => pdt.IsDelete, p => p.MapFrom(pr => pr.IsDelete));

            //Tag
            CreateMap<Tag,TagDto>().ReverseMap();
            CreateMap<Tag, CreateTagDto>().ReverseMap();
            CreateMap<Tag, TagDtoDetail>().ReverseMap();

            //ProblemTag
            CreateMap<ProblemTag, ProblemTagDto>().ReverseMap();
            CreateMap<ProblemTag, CreateProblemTagDto>().ReverseMap();
            CreateMap<ProblemTag, ProblemTagDtoDetail>()
                    .ForMember(ptd => ptd.ProblemId , pt => pt.MapFrom(problemTag => problemTag.ProblemId))
                    .ForMember(ptd => ptd.TagId, pt => pt.MapFrom(problemTag => problemTag.TagId))
                    .ForMember(ptd => ptd.ProblemName, pt => pt.MapFrom(problemTag => problemTag.Problem.Name))
                    .ForMember(ptd => ptd.TagName, pt => pt.MapFrom(problemTag => problemTag.Tag.Name));

            //InOutDto
            CreateMap<InOutExample, InOutExampleDto>().ReverseMap();
            CreateMap<InOutExample, CreateInOutExampleDto>().ReverseMap();
            CreateMap<InOutExample, InOutExampleDtoDetail>().ReverseMap();


        }
    }
}

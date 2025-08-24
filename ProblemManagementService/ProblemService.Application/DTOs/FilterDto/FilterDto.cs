using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemService.Application.DTOs.FilterDto
{
    public class FilterDto
    {
        //Search By Name
        public string? NameSearch { get; set; } 

        ////Searh By Bookmark
        //public bool Bookmark {  get; set; } = false;

        //Paging
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;


        //Sort
        public string? SortBy { get; set; }
        public bool IsDecending { get; set; } = false;
    }
}

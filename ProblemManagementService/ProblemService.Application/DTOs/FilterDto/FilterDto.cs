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

        //Search By Level
        public List<int> Levels { get; set; } = new List<int>();

        //Search by Tag name
        public List<String> Tagnames { get; set; } = new List<string>();

        //Paging
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;


        //Sort
        public string? SortBy { get; set; }
        public bool IsDecending { get; set; } = false;
    }
}

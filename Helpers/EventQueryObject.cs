using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_eventz.Helpers
{
    public class EventQueryObject
    {
        public string? SearchParams { get; set; } = null;
        public string? Category { get; set; } = null;
        public string? Location { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending {get; set;} = false;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public int pageSize {get; set;} = 10;
        public int pageNumber {get; set;} = 1;
    }
}
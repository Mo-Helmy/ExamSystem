using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Specifications.TopicSpec
{
    public class TopicSpecificationParams
    {
        private const int maxPageSize = 10;
        private int pageSize = 5;
        private string? search;

        public int PageSize { get => pageSize; set => pageSize = value > maxPageSize ? maxPageSize : value; }
        public int PageIndex { get; set; } = 1;

        public string? Sort { get; set; }

        public int? CategoryId { get; set; }

        public string? Search { get => search; set => search = value?.ToLower(); }
    }
}

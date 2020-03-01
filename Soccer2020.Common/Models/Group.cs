using System.Collections.Generic;

namespace Soccer2020.Common.Models
{
    public class Group : List<GroupDetailResponse>
    {
        public string Name { get; set; }

        public List<GroupDetailResponse> Teams => this;
    }
}
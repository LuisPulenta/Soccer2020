using Soccer2020.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Soccer2020.Common.Helpers
{
    public class TransformHelper : ITransformHelper
    {
        public List<Group> ToGroups(List<GroupResponse> groupResponses)
        {
            List<Group> list = new List<Group>();

            foreach (GroupResponse groupResponse in groupResponses)
            {
                Group group = new Group();
                foreach (GroupDetailResponse groupDetail in groupResponse.GroupDetails
                                                                         .OrderByDescending(gd => gd.Points)
                                                                         .ThenByDescending(gd => gd.GoalDifference)
                                                                         .ThenByDescending(gd => gd.GoalsFor)
                                                                         .ThenBy(gd => gd.Team.Name))
                {
                    group.Add(groupDetail);
                }

                group.Name = groupResponse.Name;
                list.Add(group);
            }

            return list;
        }
    }
}
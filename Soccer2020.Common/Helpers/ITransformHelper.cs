using Soccer2020.Common.Models;
using System.Collections.Generic;

namespace Soccer2020.Common.Helpers
{
    public interface ITransformHelper
    {
        List<Group> ToGroups(List<GroupResponse> groupResponses);
    }
}
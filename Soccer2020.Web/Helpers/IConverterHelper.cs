using Soccer2020.Web.Data.Entities;
using Soccer2020.Web.Models;
using System.Threading.Tasks;

namespace Soccer2020.Web.Helpers
{
    public interface IConverterHelper
    {
        TeamEntity ToTeamEntity(TeamViewModel model, string path, bool isNew);

        TeamViewModel ToTeamViewModel(TeamEntity teamEntity);

        TournamentEntity ToTournamentEntity(TournamentViewModel model, string path, bool isNew);

        TournamentViewModel ToTournamentViewModel(TournamentEntity tournamentEntity);

        Task<GroupEntity> ToGroupEntityAsync(GroupViewModel model, bool isNew);

        GroupViewModel ToGroupViewModel(GroupEntity groupEntity);

        Task<GroupDetailEntity> ToGroupDetailEntityAsync(GroupDetailViewModel model, bool isNew);

        GroupDetailViewModel ToGroupDetailViewModel(GroupDetailEntity groupDetailEntity);

        Task<MatchEntity> ToMatchEntityAsync(MatchViewModel model, bool isNew);

        MatchViewModel ToMatchViewModel(MatchEntity matchEntity);

    }
}
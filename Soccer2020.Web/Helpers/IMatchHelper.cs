using System.Threading.Tasks;

namespace Soccer2020.Web.Helpers
{
    public interface IMatchHelper
    {
        Task CloseMatchAsync(int matchId, int goalsLocal, int goalsVisitor);
    }
}
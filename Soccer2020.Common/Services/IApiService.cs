using Soccer2020.Common.Models;
using System.Threading.Tasks;

namespace Soccer2020.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller);
    }
}
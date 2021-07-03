using System.Threading.Tasks;
using WebApp.Api.Models;

namespace WebApp.Api.Contracts
{
    public interface IMojangService
    {
        public Task<PlayerProfile> GetPlayer(string username);
    }
}

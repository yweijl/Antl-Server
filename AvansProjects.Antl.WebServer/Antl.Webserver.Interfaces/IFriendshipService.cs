using Antl.WebServer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Antl.WebServer.Interfaces
{
    public interface IFriendshipService
    {
        Task<List<ApplicationUser>> GetListAsync(string externalId, int userId);
    }
}
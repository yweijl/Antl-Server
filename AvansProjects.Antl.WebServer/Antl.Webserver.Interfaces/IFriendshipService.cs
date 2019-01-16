using Antl.WebServer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Antl.WebServer.Dtos;

namespace Antl.WebServer.Interfaces
{
    public interface IFriendshipService
    {
        Task<List<ApplicationUser>> GetListAsync(string externalId, int userId);
        Task<Friendship> AddAsync(FriendshipDto dto);
    }
}
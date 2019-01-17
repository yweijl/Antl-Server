using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Interfaces
{
    public interface IFriendshipRepository
    {
        Task<List<ApplicationUser>> GetListAsync(Expression<Func<Friendship, bool>> where);
    }
}
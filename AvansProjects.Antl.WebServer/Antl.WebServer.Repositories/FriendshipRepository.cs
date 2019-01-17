//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using Antl.WebServer.Entities;
//using Antl.WebServer.Infrastructure;
//using Antl.WebServer.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace Antl.WebServer.Repositories
//{
//    public class FriendshipRepository : IFriendshipRepository
//    {
//        private readonly AntlContext _context;

//        public FriendshipRepository(AntlContext context, DbSet<Friendship> set)
//        {
//            _context = context;
//        }

//        public Task<List<ApplicationUser>> GetListAsync(Expression<Func<Friendship, bool>> @where)
//        {
//            return _context.FriendShips.Where(where).Include(x => x.ApplicationUser).Select(y => y.ApplicationUser).ToListAsync();
//        }
//    }
//}
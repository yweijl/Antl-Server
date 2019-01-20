using AgileObjects.AgileMapper;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Antl.WebServer.Services
{
    public class FriendshipService : GenericServiceAsync<FriendshipDto, Friendship> , IFriendshipService
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public FriendshipService(IGenericRepository<Friendship> genericRepository,
            IGenericRepository<ApplicationUser> userRepository) : base(genericRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Friendship> AddAsync(FriendshipDto dto, int userId)
        {
            var user = await _userRepository.GetAsync(x => x.Id == userId).ConfigureAwait(false);
            var friend = await _userRepository.GetAsync(x => x.ExternalId.Equals(dto.FriendId)).ConfigureAwait(false);

            if (user == null || friend == null)
            {
                throw new ArgumentException(nameof(ApplicationUser));
            }

            var internalFriendship = GetInternalFriendship( user, friend);

            return await base.AddAsync(internalFriendship).ConfigureAwait(false);
        }

        public async Task<List<FriendDto>> GetListAsync(int id)
        {
            var friendList = await _userRepository.GetListAsync(x =>
                x.Id != id &&
                (x.LeftFriendships.Any(y => y.LeftApplicationUserId == id || y.RightApplicationUserId == id)));
           
            return Mapper.Map(friendList).ToANew<List<FriendDto>>();
        }

        private static InternalFriendshipProjection GetInternalFriendship(ApplicationUser userOne, ApplicationUser userTwo)
        {
            return new InternalFriendshipProjection
            {
                LeftApplicationUser = (userOne.Id < userTwo.Id) ? userOne : userTwo,
                RightApplicationUser = (userOne.Id > userTwo.Id) ? userOne : userTwo
            };
        }
    }
}
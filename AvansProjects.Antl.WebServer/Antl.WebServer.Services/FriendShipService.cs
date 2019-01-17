using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using AgileObjects.AgileMapper.Extensions;

namespace Antl.WebServer.Services
{
    public class FriendshipService : GenericServiceAsync<FriendshipDto, Friendship> , IFriendshipService
    {
        private readonly IGenericRepository<Friendship> _genericRepository;
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public FriendshipService(IGenericRepository<Friendship> genericRepository,
            IGenericRepository<ApplicationUser> userRepository) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _userRepository = userRepository;
        }

        public override async Task<Friendship> AddAsync(FriendshipDto dto)
        {
            var userOne = await _userRepository.GetAsync(x => x.ExternalId == dto.ApplicationUserExternalId).ConfigureAwait(false);
            var userTwo = await _userRepository.GetAsync(x => x.ExternalId == dto.ApplicationUserTwoExternalId).ConfigureAwait(false);

            if (userOne == null || userTwo == null)
            {
                throw new ArgumentException(nameof(ApplicationUser));
            }

            var internalFriendship = GetInternalFriendship(userOne, userTwo);

            return await base.AddAsync(internalFriendship).ConfigureAwait(false);
        }

        public async Task<List<FriendDto>> GetListAsync(int id)
        {
            var friendshipList =
                (await _genericRepository.GetListAsync(x => x.ApplicationUserId == id || x.ApplicationUserTwoId == id)
                    .ConfigureAwait(false)).SelectMany(i => new[] {i.ApplicationUserId, i.ApplicationUserTwoId})
                .Where(x => x != id).ToArray();

            return Mapper.Map(await _userRepository.GetListAsync(x => friendshipList.Contains(x.Id))).ToANew<List<FriendDto>>();
        }

        private static InternalFriendshipProjection GetInternalFriendship(ApplicationUser userOne, ApplicationUser userTwo)
        {
            return new InternalFriendshipProjection
            {
                ApplicationUser = (userOne.Id < userTwo.Id) ? userOne : userTwo,
                ApplicationUserTwo = (userOne.Id > userTwo.Id) ? userOne : userTwo
            };
        }
    }
}
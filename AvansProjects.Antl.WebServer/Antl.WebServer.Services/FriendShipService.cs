using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;

namespace Antl.WebServer.Services
{
    public class FriendShipService : GenericServiceAsync<FriendshipDto, FriendShip> , IFriendShipService
    {
        private readonly IGenericRepository<FriendShip> _friendshipRepository;
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public FriendShipService(IGenericRepository<FriendShip> friendshipRepository,
            IGenericRepository<ApplicationUser> userRepository) : base(friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
            _userRepository = userRepository;
        }

        public override async Task<FriendShip> AddAsync(FriendshipDto dto)
        {
            var userOne = await _userRepository.GetAsync(x => x.ExternalId == dto.UserIdOne).ConfigureAwait(false);
            var userTwo = await _userRepository.GetAsync(x => x.ExternalId == dto.UserIdTwo).ConfigureAwait(false);

            if (userOne == null || userTwo == null)
            {
                throw new ArgumentException(nameof(ApplicationUser));
            }

            var internalFriendship = GetInternalFriendship(userOne.Id, userTwo.Id);

            if (internalFriendship == null) throw new ArgumentNullException(nameof(internalFriendship));

            var entity = Mapper.Map(internalFriendship).ToANew<FriendShip>();

            return await _friendshipRepository.AddAsync(entity).ConfigureAwait(false);
        }

        public async Task<List<ApplicationUser>> GetListAsync(string externalId, int id)
        {
            var userOneList = (await _friendshipRepository.GetListAsync(x =>
                x.ApplicationUser.ExternalId == externalId && x.ApplicationUser.Id != id).ConfigureAwait(false))
                .Select(x => x.ApplicationUser).ToList();

            var userTwoList = (await _friendshipRepository.GetListAsync(x =>
                x.ApplicationUserTwo.ExternalId == externalId && x.ApplicationUserTwo.Id != id).ConfigureAwait(false))
                .Select(x => x.ApplicationUserTwo).ToList();

            return new List<ApplicationUser>().Concat(userOneList).Concat(userTwoList).ToList();
        }

        private static InternalFriendshipDto GetInternalFriendship(int userOne, int userTwo)
        {
            return new InternalFriendshipDto
            {
                ApplicationUserId = Math.Min(userOne, userTwo), ApplicationUserTwoId = Math.Max(userTwo, userOne)
            };
        }
    }
}
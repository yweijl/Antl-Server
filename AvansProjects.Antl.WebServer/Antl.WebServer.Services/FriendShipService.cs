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
    public class FriendshipService : GenericServiceAsync<FriendshipDto, Friendship> , IFriendshipService
    {
        private readonly IGenericRepository<Friendship> _friendshipRepository;
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public FriendshipService(IGenericRepository<Friendship> friendshipRepository,
            IGenericRepository<ApplicationUser> userRepository) : base(friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
            _userRepository = userRepository;
        }

        public override async Task<Friendship> AddAsync(FriendshipDto dto)
        {
            var userOne = await _userRepository.GetAsync(x => x.ExternalId == dto.ApplicationUserExternalId).ConfigureAwait(false);
            var userTwo = await _userRepository.GetAsync(x => x.ExternalId == dto.).ConfigureAwait(false);

            if (userOne == null || userTwo == null)
            {
                throw new ArgumentException(nameof(ApplicationUser));
            }

            var internalFriendship = GetInternalFriendship(userOne, userTwo);


            var entity = Mapper.Map(internalFriendship).ToANew<Friendship>();

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
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
    public class ContactService : GenericServiceAsync<FriendshipDto, FriendShip>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public ContactService(IGenericRepository<FriendShip> friendshipRepository, IGenericRepository<ApplicationUser> userRepository) : base(friendshipRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<FriendShip> AddAsync(FriendshipDto dto)
        {
            var users = await _userRepository.GetAllAsync().ConfigureAwait(true);
            var userOne = users.SingleOrDefault(x => x.ExternalId == dto.UserIdOne);
            var userTwo = users.SingleOrDefault(x => x.ExternalId == dto.UserIdTwo);

            if (userOne == null || userTwo == null)
            {
                throw new ArgumentException();
            }

            var newDto = GetInternalFriendshipDto(userOne, userTwo);

            if (newDto == null) throw new ArgumentNullException(nameof(newDto));
            var entity = Mapper.Map(newDto).ToANew<FriendShip>();

            return await _repository.AddAsync(entity).ConfigureAwait(false);
        }

        private static InternalFriendshipDto GetInternalFriendshipDto(ApplicationUser userOne, ApplicationUser userTwo)
        {
            var newDto = new InternalFriendshipDto();

            if (userOne.Id < userTwo.Id)
            {
                newDto.ApplicationUser = userOne;
                newDto.ApplicationUserTwo = userTwo;
            }
            else
            {
                newDto.ApplicationUser = userTwo;
                newDto.ApplicationUserTwo = userOne;
            }

            return newDto;
        }

        public async Task<ICollection<ApplicationUser>> GetAllFriendsAsync(string code)
        {
            var users = await _userRepository.GetAllAsync().ConfigureAwait(true);
            var requestingUser = users.SingleOrDefault(x => x.ExternalId == code);

            if (requestingUser == null)
            {
                throw new ArgumentException();
            }

            var friendships = await _repository.GetAllAsync().ConfigureAwait(false);

            var usersFriendsOne = friendships.Where(x =>
             x.ApplicationUser.ExternalId == code).Select(x => Mapper.Map(x.ApplicationUserTwo).ToANew<ApplicationUser>()).ToList();
            var usersFriendsTwo = friendships.Where(x =>
                    x.ApplicationUserTwo.ExternalId == code).Select(x => Mapper.Map(x.ApplicationUser).ToANew<ApplicationUser>()).ToList();

            var usersFriendships = new List<ApplicationUser>();
            usersFriendships.AddRange(usersFriendsOne);
            usersFriendships.AddRange(usersFriendsTwo);

            ICollection<ApplicationUser> friendList =
                await Task.FromResult<ICollection<ApplicationUser>>(usersFriendships);

            return friendList;

        }
    }
}
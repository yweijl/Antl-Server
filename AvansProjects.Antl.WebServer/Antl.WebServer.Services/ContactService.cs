using System;
using System.Collections.Generic;
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
            ApplicationUser userOne = new ApplicationUser();
            ApplicationUser userTwo = new ApplicationUser();

            foreach (var user in users)
            {
                if (user.ExternalId == dto.UserIdOne)
                {
                    userOne = user;
                }

                if (user.ExternalId == dto.UserIdTwo)
                {
                    userTwo = user;
                }
            }

            if (userOne == null || userTwo == null)
            {
               throw new ArgumentException();
            }


        InternalFriendshipDto newDto = new InternalFriendshipDto();

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

            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var entity = Mapper.Map(newDto).ToANew<FriendShip>();

            var result = await _repository.AddAsync(entity).ConfigureAwait(false);
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        public async Task<ICollection<ApplicationUser>> GetAllFriendsAsync(String code)
        {
            var users = await _userRepository.GetAllAsync().ConfigureAwait(true);
            ApplicationUser requestingUser = new ApplicationUser();


            foreach (var user in users)
            {
                if (code.Equals(user.ExternalId))
                {
                    requestingUser = user;
                }
            }

            if (requestingUser == null)
            {
                throw new ArgumentException();
            }

            var friendships = await _repository.GetAllAsync().ConfigureAwait(true);
            
            var usersFriendships = new List<ApplicationUser>();

            foreach (var friendship in friendships)
            {
                if (friendship.ApplicationUser.ExternalId == code)
                {
                    usersFriendships.Add(friendship.ApplicationUserTwo);
                }
                
                if (friendship.ApplicationUserTwo.ExternalId == code)
                {
                    usersFriendships.Add(friendship.ApplicationUser);
                }

            }

            ICollection<ApplicationUser> friendList =
                await Task.FromResult<ICollection<ApplicationUser>>(usersFriendships);

            return friendList;

        }

        public class InternalFriendshipDto : IDto
        {
            public int Id { get; set; }
            public ApplicationUser ApplicationUser { get; set; }
            public ApplicationUser ApplicationUserTwo { get; set; }
        }


    }
}
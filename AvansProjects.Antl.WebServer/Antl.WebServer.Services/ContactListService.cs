using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;

namespace Antl.WebServer.Services
{
    class ContactListService : GenericServiceAsync<UserIdentificationDto, ApplicationUser>
    {
    private readonly IGenericRepository<FriendShip> _friendRepository;

    public ContactListService(IGenericRepository<FriendShip> friendshipRepository,
        IGenericRepository<ApplicationUser> userRepository) : base(userRepository)
    {
        _friendRepository = friendshipRepository;
    }

        //public override async Task<ApplicationUser> GetAsync(int id)
        //{
        //    var result = await base.GetAllAsync();
        //    foreach (var applicationUser in result)
             
        //    }

        //    return await base.GetAllAsync();
        //}
    }

}

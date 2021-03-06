﻿using System;
using Antl.WebServer.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Antl.WebServer.Dtos;

namespace Antl.WebServer.Interfaces
{
    public interface IFriendshipService
    {
        Task<List<FriendDto>> GetListAsync(int id);
        Task<Friendship> AddAsync(FriendshipDto dto, int userId);
    }
}
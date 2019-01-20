﻿using System;
using Antl.WebServer.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Antl.WebServer.Dtos;

namespace Antl.WebServer.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetListAsync(UpdateEventDto updateEventDto);
        Task<List<EventSyncDto>> GetHashList(int userId);
        Task<EventSyncDto> AddAsync(EventDto eventDto, int userId);
    }
}
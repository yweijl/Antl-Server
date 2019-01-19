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
    public class EventService : GenericServiceAsync<EventDto, Event>
    {

        public EventService(IGenericRepository<Event> genericRepository) : base(genericRepository)
        {
        }

        public Task<string> AddAsync(EventDto dto, int userId)
        {
            var internalDto = Mapper.Map(dto).ToANew<InternalEventDto>();
            internalDto.EventOwnerId = userId;
            return base.AddAsync(internalDto);
        }
    }
}
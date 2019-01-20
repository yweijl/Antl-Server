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
    public class EventService : GenericServiceAsync<EventDto, Event>, IEventService
    {
        private readonly IGenericRepository<Event> _genericRepository;

        public EventService(IGenericRepository<Event> genericRepository) : base(genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<EventSyncDto> AddAsync(EventDto dto, int userId)
        {
            var internalDto = Mapper.Map(dto).ToANew<InternalEventDto>();
            internalDto.EventOwnerId = userId;
            var @event = await base.AddAsync(internalDto).ConfigureAwait(true);
            return new EventSyncDto
            {
                ExternalId = @event.ExternalId,
                Hash = @event.GetHashCode()
            };
        }

        public async Task<List<EventSyncDto>> GetHashList(int userId)
        {
            var EventSyncList = new List<EventSyncDto>();
            var events = await _genericRepository.GetListAsync(x => x.EventOwnerId == userId && x.IsDeleted != true).ConfigureAwait(false);
            foreach (var @event in events)
            {
                EventSyncList.Add(new EventSyncDto
                {
                    ExternalId = @event.ExternalId,
                    Hash = @event.GetHashCode()
                });
            }

            return EventSyncList;
        }

        public Task<List<EventDto>> GetListAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
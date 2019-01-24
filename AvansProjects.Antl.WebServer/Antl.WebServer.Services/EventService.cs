using AgileObjects.AgileMapper;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Antl.WebServer.Services
{
    public class EventService : GenericServiceAsync<EventDto, Event>, IEventService
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IGenericRepository<EventDate> _eventDateRepository;
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public EventService(IGenericRepository<Event> eventRepository, IGenericRepository<ApplicationUser> userRepository, IGenericRepository<EventDate> eventDateRepository) : base(eventRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _eventDateRepository = eventDateRepository;
        }

        public async Task<EventSyncDto> AddAsync(EventDto dto, int userId)
        {
            var internalDto = Mapper.Map(dto).ToANew<InternalEventDto>();
            internalDto.EventOwnerId = userId;
            internalDto.Hash = internalDto.GetHashCode();
            var @event = await base.AddAsync(internalDto).ConfigureAwait(true);
            return new EventSyncDto
            {
                ExternalId = @event.ExternalId,
                Hash = @event.Hash
            };
        }

        public async Task<List<SendEventDto>> GetListAsync(UpdateEventDto updateEventDto)
        {
           var events = await _eventRepository.GetListAsync(x => updateEventDto.ExternalId.Contains(x.ExternalId)).ConfigureAwait(false);
           foreach (var @event in events)
           {
               @event.EventDates = await _eventDateRepository.GetListAsync(x => x.EventId == @event.Id).ConfigureAwait(false);
           }

           return Mapper.Map(events).ToANew<List<SendEventDto>>().ToList();
        }

        public async Task<List<EventSyncDto>> GetHashList(int userId)
        {
            var eventSyncList = new List<EventSyncDto>();
            var events = await _eventRepository.GetListAsync(x => x.EventOwnerId == userId && x.IsDeleted != true)
                .ConfigureAwait(false);

            foreach (var @event in events)
            {
                eventSyncList.Add(new EventSyncDto
                {
                    ExternalId = @event.ExternalId,
                    Hash = @event.Hash
                });
            }

            return eventSyncList;
        }
    }
}
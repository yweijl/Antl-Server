using Antl.WebServer.Entities;
using System;
using System.Collections.Generic;

namespace Antl.WebServer.Infrastructure.Data
{
    public class EventSeed
    {
        public IList<Event> SeedEvents(IList<ApplicationUser> applicationUsers) =>
            new List<Event>
            {
                new Event
                {
                    Name = "Project X",
                    Location = "Alkmaar",
                    EventDates = new List<EventDate>
                    {
                        new EventDate
                        {
                            DateTime = DateTime.Now.AddDays(5),
                            UserAvailabilities = new List<UserEventAvailability>
                            {
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Yes
                                },
                                new UserEventAvailability
                                {
                                ApplicationUser = applicationUsers[1],
                                Availability = Availability.No
                            },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.No
                                },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.Maybe
                                }
                            }
                        },
                        new EventDate
                        {
                            DateTime = DateTime.Now.AddDays(9),
                            UserAvailabilities = new List<UserEventAvailability>
                            {
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Maybe
                                },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.Yes
                                },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Yes
                                },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.No
                                }
                            }
                        }
                    }
                },
                new Event
                {
                    Name = "Kerst",
                    Location = "Den Haag",
                    EventDates = new List<EventDate>
                    {
                        new EventDate
                        {
                            DateTime = DateTime.Now.AddDays(11),
                            UserAvailabilities = new List<UserEventAvailability>
                            {
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Maybe
                                },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.Yes
                                },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Yes
                                },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.No
                                }
                            }
                        },
                        new EventDate
                        {
                            DateTime = DateTime.Now.AddDays(15),
                            UserAvailabilities = new List<UserEventAvailability>
                            {
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Maybe
                                },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.Yes
                                },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Yes
                                },
                                new UserEventAvailability
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.No
                                }
                            }
                        }
                    }
                }
            };
    }
}

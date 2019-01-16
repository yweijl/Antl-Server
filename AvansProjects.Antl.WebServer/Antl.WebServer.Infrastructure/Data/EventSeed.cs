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
                            UserEventDates = new List<UserEventDate>
                            {
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Yes
                                },
                                new UserEventDate
                                {
                                ApplicationUser = applicationUsers[1],
                                Availability = Availability.No
                            },
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.No
                                },
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.Maybe
                                }
                            }
                        },
                        new EventDate
                        {
                            DateTime = DateTime.Now.AddDays(9),
                            UserEventDates = new List<UserEventDate>
                            {
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Maybe
                                },
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.Yes
                                },
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Yes
                                },
                                new UserEventDate
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
                            UserEventDates = new List<UserEventDate>
                            {
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Maybe
                                },
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.Yes
                                },
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Yes
                                },
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.No
                                }
                            }
                        },
                        new EventDate
                        {
                            DateTime = DateTime.Now.AddDays(15),
                            UserEventDates = new List<UserEventDate>
                            {
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Maybe
                                },
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[1],
                                    Availability = Availability.Yes
                                },
                                new UserEventDate
                                {
                                    ApplicationUser = applicationUsers[0],
                                    Availability = Availability.Yes
                                },
                                new UserEventDate
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

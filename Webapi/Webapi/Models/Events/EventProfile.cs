using AutoMapper;

namespace Webapi.Models.Events;

public class EventProfile : Profile
{
  public EventProfile()
  {
    CreateMap<Event, EventDto>()
      .ReverseMap();
  }
}
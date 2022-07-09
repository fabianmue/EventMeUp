using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models.Events;
using Webapi.Repositories.Events;

namespace Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
  private readonly IEventRepository _eventRepository;

  private readonly IMapper _mapper;

  public EventsController(
    IEventRepository eventRepository,
    IMapper mapper)
  {
    _eventRepository = eventRepository;
    _mapper = mapper;
  }

  [HttpGet("{eventId}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<EventDto>> GetEventById(
    [FromRoute] string eventId)
  {
    Event? ev = await _eventRepository
      .FindAsync(ev => ev.Id == eventId);
    if (ev == null)
    {
      return NotFound();
    }

    return Ok(_mapper.Map<EventDto>(ev));
  }

  [HttpPost("ids")]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public ActionResult<List<EventDto>> GetEventsByIds(
    [FromBody] IEnumerable<string> eventIds)
  {
    IEnumerable<Event> events = _eventRepository
      .FindAll(ev => eventIds.Contains(ev.Id));
    return Ok(_mapper.Map<IEnumerable<EventDto>>(events));
  }

  [HttpPost]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<Tuple<EventDto, string>>> CreateEvent(
    [FromBody] EventCreateDto eventCreateDto)
  {
    var ev = new Event(eventCreateDto);
    _eventRepository.Add(ev);
    await _eventRepository.SaveChangesAsync();
    return Created(
      $"Events/{ev.Id}",
      (_mapper.Map<EventDto>(ev), ev.EditToken)
    );
  }

  [HttpPut("{eventId}")]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<EventDto>> UpdateEvent(
    [FromRoute] string eventId,
    [FromQuery] string editToken,
    [FromBody] EventUpdateDto eventUpdateDto)
  {
    Event? ev = await _eventRepository
      .FindAsync(ev => ev.Id == eventId);
    if (ev == null)
    {
      return NotFound();
    }

    if (ev.EditToken != editToken)
    {
      return Unauthorized();
    }

    ev.Update(eventUpdateDto);
    _eventRepository.Update(ev);
    await _eventRepository.SaveChangesAsync();
    return Ok(_mapper.Map<EventDto>(ev));
  }

  [HttpDelete("{eventId}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult> DeleteEvent(
    [FromRoute] string eventId,
    [FromQuery] string editToken)
  {
    Event? ev = await _eventRepository
      .FindAsync(ev => ev.Id == eventId);
    if (ev == null)
    {
      return NotFound();
    }

    if (ev.EditToken != editToken)
    {
      return Unauthorized();
    }

    _eventRepository.Delete(ev);
    await _eventRepository.SaveChangesAsync();
    return Ok();
  }
}

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
    this._eventRepository = eventRepository;
    this._mapper = mapper;
  }

  [HttpGet("{eventId}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<EventDto>> GetEvent(
    [FromRoute] string eventId)
  {
    Event? ev = await this._eventRepository
      .FindAsync(ev => ev.Id == eventId);
    if (ev == null)
    {
      return this.NotFound();
    }

    return this.Ok(this._mapper.Map<EventDto>(ev));
  }

  [HttpPost]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<EventDto>> CreateEvent(
    [FromBody] EventCreateDto eventCreateDto)
  {
    var ev = new Event(eventCreateDto);
    this._eventRepository.Add(ev);
    await this._eventRepository.SaveChangesAsync();
    return this.Created(
      $"Events/{ev.Id}?editToken={ev.EditToken}",
      this._mapper.Map<EventDto>(ev)
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
    Event? ev = await this._eventRepository
      .FindAsync(ev => ev.Id == eventId);
    if (ev == null)
    {
      return this.NotFound();
    }

    if (ev.EditToken != editToken)
    {
      return this.Unauthorized();
    }

    ev.Update(eventUpdateDto);
    this._eventRepository.Update(ev);
    await this._eventRepository.SaveChangesAsync();
    return this.Ok(this._mapper.Map<EventDto>(ev));
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
    Event? ev = await this._eventRepository
      .FindAsync(ev => ev.Id == eventId);
    if (ev == null)
    {
      return this.NotFound();
    }

    if (ev.EditToken != editToken)
    {
      return this.Unauthorized();
    }

    this._eventRepository.Delete(ev);
    await this._eventRepository.SaveChangesAsync();
    return this.Ok();
  }
}

using System.Net.Mime;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models.Events;
using Webapi.Models.Identity;
using Webapi.Repositories.Events;
using Webapi.Services.Events;

namespace Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
  private readonly IEventRepository _eventRepository;

  private readonly IEventService _eventService;

  private readonly UserManager<WebapiUser> _userManager;

  private readonly IMapper _mapper;

  private readonly ILogger<EventsController> _logger;

  public EventsController(
    IEventRepository eventRepository,
    IEventService eventService,
    UserManager<WebapiUser> userManager,
    IMapper mapper,
    ILogger<EventsController> logger)
  {
    this._eventRepository = eventRepository;
    this._eventService = eventService;
    this._userManager = userManager;
    this._mapper = mapper;
    this._logger = logger;
  }

  [Authorize]
  [HttpGet("owned")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<IList<EventDto>>> GetAllMyOwnedEvents()
  {
    var user = await this.GetCurrentUserAsync();
    if (user == null)
    {
      return this.NotFound();
    }

    var events = await this._eventRepository.GetAllEventsByOwnerAsync(user);
    return this.Ok(_mapper.Map<IList<EventDto>>(events)
      .OrderBy(eventDto => eventDto.Start));
  }


  [Authorize]
  [HttpGet("signedup")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<IList<EventDto>>> GetAllMySignedUpEvents()
  {
    var user = await this.GetCurrentUserAsync();
    if (user == null)
    {
      return this.NotFound();
    }

    var events = await this._eventRepository.GetAllEventsBySingedUpUserAsync(user);
    return this.Ok(_mapper.Map<IList<EventDto>>(events)
      .OrderBy(eventDto => eventDto.Start));
  }

  [HttpGet("{eventId}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<EventDto>> GetEvent([FromQuery] string eventId)
  {
    var ev = await this._eventRepository.GetEventAsync(eventId);
    if (ev == default)
    {
      return this.NotFound();
    }

    return this.Ok(this._mapper.Map<EventDto>(ev));
  }

  [Authorize]
  [HttpPost]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<EventDto>> CreateEvent([FromBody] EventCreateDto eventCreateDto)
  {
    var user = await this.GetCurrentUserAsync();
    if (user == null)
    {
      return this.NotFound();
    }

    var ev = new Event(eventCreateDto, user);
    await this._eventRepository.AddEventAsync(ev);

    return this.CreatedAtAction(nameof(GetEvent), new { id = ev.Id }, this._mapper.Map<EventDto>(ev));
  }

  [HttpPost("{eventId}")]
  [Consumes(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<EventDto>> AddEventSignUp([FromQuery] string eventId, [FromBody] SignUpCreateDto signUpCreateDto)
  {
    Event? ev = await this._eventService.AddEventSignUpAsync(eventId, signUpCreateDto);
    if (ev == null)
    {
      return this.NotFound();
    }

    return this.Ok(this._mapper.Map<EventDto>(ev));
  }

  private async Task<WebapiUser?> GetCurrentUserAsync()
  {
    string? userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
    return await this._userManager.FindByEmailAsync(userEmail);
  }
}

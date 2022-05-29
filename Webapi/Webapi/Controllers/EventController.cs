using System.Net.Mime;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models.Events;
using Webapi.Models.Identity;
using Webapi.Repositories.Events;

namespace Webapi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
  private readonly IEventRepository _repository;

  private readonly UserManager<WebapiUser> _userManager;

  private readonly IMapper _mapper;

  private readonly ILogger<EventsController> _logger;

  public EventsController(
    IEventRepository repository,
    UserManager<WebapiUser> userManager,
    IMapper mapper,
    ILogger<EventsController> logger)
  {
    this._repository = repository;
    this._userManager = userManager;
    this._mapper = mapper;
    this._logger = logger;
  }

  [HttpGet]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDto))]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<IList<EventDto>>> GetAllEvents()
  {
    var user = await GetCurrentUserAsync();
    if (user == null)
    {
      return NotFound();
    }

    var events = await this._repository.GetAllEventsByOwnerAsync(user);
    return Ok(_mapper.Map<IList<EventDto>>(events)
      .OrderBy(eventDto => eventDto.Start));
  }

  [HttpGet("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDto))]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<EventDto>> GetEvent(string id)
  {
    var ev = await this._repository.GetEventAsync(id);
    if (ev == default)
    {
      return NotFound();
    }

    return Ok(_mapper.Map<EventDto>(ev));
  }

  [HttpPost]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<EventDto>> CreateEvent(EventDto eventDto)
  {
    var user = await GetCurrentUserAsync();
    if (user == null)
    {
      return NotFound();
    }

    var ev = new Event(eventDto, user);
    await this._repository.AddEventAsync(ev);

    return CreatedAtAction(nameof(GetEvent), new { id = ev.Id }, eventDto);
  }

  private async Task<WebapiUser?> GetCurrentUserAsync()
  {
    string? userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
    return await this._userManager.FindByEmailAsync(userEmail);
  }
}

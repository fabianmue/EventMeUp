using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models.Signups;
using Webapi.Repositories.Signups;
using Webapi.Services.Signups;

namespace Webapi.Controllers;

[ApiController]
[Route("events/{eventId}/[controller]")]
public class SignupsController : ControllerBase
{
  private readonly ISignupRepository _signupRepository;

  private readonly ISignupService _signupService;

  private readonly IMapper _mapper;

  public SignupsController(
    ISignupRepository signupRepository,
    ISignupService signupService,
    IMapper mapper)
  {
    _signupRepository = signupRepository;
    _signupService = signupService;
    _mapper = mapper;
  }

  [HttpPost]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<Tuple<SignupDto, string>>> CreateSignup(
    [FromRoute] string eventId,
    [FromBody] SignupCreateDto signupCreateDto)
  {
    Signup? signup = await _signupService
      .AddSignupAsync(eventId, signupCreateDto);
    if (signup == null)
    {
      return NotFound();
    }

    return Created(
      $"Events/{eventId}/Signups/{signup.Id}",
      (_mapper.Map<SignupDto>(signup), signup.EditToken)
    );
  }

  [HttpPut("{signupId}")]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<SignupDto>> UpdateSignup(
    [FromRoute] string signupId,
    [FromQuery] string editToken,
    [FromBody] SignupUpdateDto signupUpdateDto)
  {
    Signup? signup = await _signupRepository
      .FindAsync(signup => signup.Id == signupId);
    if (signup == null)
    {
      return NotFound();
    }

    if (signup.EditToken != editToken)
    {
      return Unauthorized();
    }

    signup.Update(signupUpdateDto);
    _signupRepository.Update(signup);
    await _signupRepository.SaveChangesAsync();
    return Ok(_mapper.Map<SignupDto>(signup));
  }

  [HttpDelete("{signupId}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult> DeleteSignup(
    [FromRoute] string signupId,
    [FromQuery] string editToken)
  {
    Signup? signup = await _signupRepository
      .FindAsync(signup => signup.Id == signupId);
    if (signup == null)
    {
      return NotFound();
    }

    if (signup.EditToken != editToken)
    {
      return Unauthorized();
    }

    _signupRepository.Delete(signup);
    await _signupRepository.SaveChangesAsync();
    return Ok();
  }
}

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
    this._signupRepository = signupRepository;
    this._signupService = signupService;
    this._mapper = mapper;
  }

  [HttpPost]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<SignupDto>> CreateSignup(
    [FromRoute] string eventId,
    [FromBody] SignupCreateDto signupCreateDto)
  {
    Signup? signup = await this._signupService
      .AddSignupAsync(eventId, signupCreateDto);
    if (signup == null)
    {
      return this.NotFound();
    }

    return this.Created(
      $"Events/{eventId}/Signups/{signup.Id}",
      (Signup: this._mapper.Map<SignupDto>(signup), signup.EditToken)
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
    Signup? signup = await this._signupRepository
      .FindAsync(signup => signup.Id == signupId);
    if (signup == null)
    {
      return this.NotFound();
    }

    if (signup.EditToken != editToken)
    {
      return this.Unauthorized();
    }

    signup.Update(signupUpdateDto);
    this._signupRepository.Update(signup);
    await this._signupRepository.SaveChangesAsync();
    return this.Ok(this._mapper.Map<SignupDto>(signup));
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
    Signup? signup = await this._signupRepository
      .FindAsync(signup => signup.Id == signupId);
    if (signup == null)
    {
      return this.NotFound();
    }

    if (signup.EditToken != editToken)
    {
      return this.Unauthorized();
    }

    this._signupRepository.Delete(signup);
    await this._signupRepository.SaveChangesAsync();
    return this.Ok();
  }
}

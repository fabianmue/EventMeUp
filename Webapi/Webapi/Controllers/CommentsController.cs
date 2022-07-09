using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models.Comments;
using Webapi.Models.Signups;
using Webapi.Repositories.Comments;
using Webapi.Repositories.Signups;
using Webapi.Services.Signups;

namespace Webapi.Controllers;

[ApiController]
[Route("Events/{eventId}/Signups/{signupId}/[controller]")]
public class CommentsController : ControllerBase
{
  private readonly ISignupRepository _signupRepository;

  private readonly ICommentRepository _commentRepository;

  private readonly ISignupService _signupService;

  private readonly IMapper _mapper;

  public CommentsController(
    ISignupRepository signupRepository,
    ICommentRepository commentRepository,
    ISignupService signupService,
    IMapper mapper)
  {
    _signupRepository = signupRepository;
    _commentRepository = commentRepository;
    _signupService = signupService;
    _mapper = mapper;
  }

  [HttpPost]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<CommentDto>> CreateComment(
    [FromRoute] string eventId,
    [FromRoute] string signupId,
    [FromBody] CommentCreateDto commentCreateDto)
  {
    Comment? comment = await _signupService
      .AddCommentAsync(signupId, commentCreateDto);
    if (comment == null)
    {
      return NotFound();
    }

    return Created(
      $"Events/{eventId}/Signups/{signupId}/Comments/{comment.Id}",
      _mapper.Map<CommentDto>(comment)
    );
  }

  [HttpPut("{commentId}")]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<CommentDto>> UpdateComment(
    [FromRoute] string signupId,
    [FromRoute] string commentId,
    [FromQuery] string editToken,
    [FromBody] CommentUpdateDto commentUpdateDto)
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

    Comment? comment = signup.Comments
      .Find(comment => comment.Id == commentId);
    if (comment == null)
    {
      return NotFound();
    }

    comment.Update(commentUpdateDto);
    _commentRepository.Update(comment);
    await _commentRepository.SaveChangesAsync();
    return Ok(_mapper.Map<CommentDto>(comment));
  }

  [HttpDelete("{commentId}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult> DeleteSignup(
    [FromRoute] string signupId,
    [FromRoute] string commentId,
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

    Comment? comment = signup.Comments
      .Find(comment => comment.Id == commentId);
    if (comment == null)
    {
      return NotFound();
    }

    _commentRepository.Delete(comment);
    await _commentRepository.SaveChangesAsync();
    return Ok();
  }
}

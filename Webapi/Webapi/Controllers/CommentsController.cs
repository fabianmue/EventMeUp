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
    this._signupRepository = signupRepository;
    this._commentRepository = commentRepository;
    this._signupService = signupService;
    this._mapper = mapper;
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
    Comment? comment = await this._signupService
      .AddCommentAsync(signupId, commentCreateDto);
    if (comment == null)
    {
      return this.NotFound();
    }

    return this.Created(
      $"Events/{eventId}/Signups/{signupId}/Comments/{comment.Id}",
      this._mapper.Map<CommentDto>(comment)
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

    Comment? comment = signup.Comments
      .Find(comment => comment.Id == commentId);
    if (comment == null)
    {
      return this.NotFound();
    }

    comment.Update(commentUpdateDto);
    this._commentRepository.Update(comment);
    await this._commentRepository.SaveChangesAsync();
    return this.Ok(this._mapper.Map<CommentDto>(comment));
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

    Comment? comment = signup.Comments
      .Find(comment => comment.Id == commentId);
    if (comment == null)
    {
      return this.NotFound();
    }

    this._commentRepository.Delete(comment);
    await this._commentRepository.SaveChangesAsync();
    return this.Ok();
  }
}

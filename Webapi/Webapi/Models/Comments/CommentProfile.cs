using AutoMapper;

namespace Webapi.Models.Comments;

public class CommentProfile : Profile
{
  public CommentProfile()
  {
    CreateMap<Comment, CommentDto>();
  }
}
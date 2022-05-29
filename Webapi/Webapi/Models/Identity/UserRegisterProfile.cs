using AutoMapper;
using Webapi.Models.Identity;

namespace Webapi.Models.Events;

public class UserRegisterProfile : Profile
{
  public UserRegisterProfile()
  {
    CreateMap<UserRegisterDto, WebapiUser>();
  }
}
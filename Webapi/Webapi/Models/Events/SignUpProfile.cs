using AutoMapper;

namespace Webapi.Models.Events;

public class SignUpProfile : Profile
{
  public SignUpProfile()
  {
    CreateMap<SignUp, SignUpDto>();
  }
}
using AutoMapper;

namespace Webapi.Models.Signups;

public class SignupProfile : Profile
{
  public SignupProfile()
  {
    CreateMap<Signup, SignupDto>();
  }
}
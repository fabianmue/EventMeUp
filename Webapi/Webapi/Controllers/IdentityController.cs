using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Webapi.Models.Identity;

namespace Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
  private readonly UserManager<WebapiUser> _userManager;

  private readonly SignInManager<WebapiUser> _signInManager;

  private readonly IConfiguration _configuration;

  private readonly IMapper _mapper;

  public IdentityController(
      UserManager<WebapiUser> userManager,
      SignInManager<WebapiUser> signInManager,
      IConfiguration configuration,
      IMapper mapper)
  {
    this._userManager = userManager;
    this._signInManager = signInManager;
    this._configuration = configuration;
    this._mapper = mapper;
  }

  [HttpPost("login")]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginResponseDto))]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IActionResult))]
  public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
  {
    if (userLoginDto == null || !ModelState.IsValid)
    {
      return Unauthorized();
    }

    var user = await this._userManager.FindByEmailAsync(userLoginDto?.Email);
    if (user == null)
    {
      return Unauthorized();
    }

    var result = await this._signInManager.CheckPasswordSignInAsync(user, userLoginDto!.Password, false);
    if (!result.Succeeded)
    {
      return Unauthorized();
    }

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };
    var token = GetJwtSecurityToken(claims);
    return Ok(new UserLoginResponseDto
    {
      Token = new JwtSecurityTokenHandler().WriteToken(token)
    });
  }

  [HttpPost("register")]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UserRegisterResponseDto))]
  public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
  {
    if (userRegisterDto == null || !ModelState.IsValid)
    {
      return BadRequest();
    }

    var user = this._mapper.Map<WebapiUser>(userRegisterDto);
    IdentityResult result = await this._userManager.CreateAsync(
      user, userRegisterDto.Password);
    if (!result.Succeeded)
    {
      return BadRequest(new UserRegisterResponseDto
      {
        Errors = result.Errors.Select(error => error.Code)
      });
    }

    return StatusCode(201);
  }

  private JwtSecurityToken GetJwtSecurityToken(IEnumerable<Claim> claims)
  {
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWT:Secret"]));
    return new JwtSecurityToken(
      issuer: this._configuration["JWT:ValidIssuer"],
      audience: this._configuration["JWT:ValidAudience"],
      expires: DateTime.Now.AddHours(1),
      claims: claims,
      signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
    );
  }
}
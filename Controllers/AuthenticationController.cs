using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Assignment.Api.Models.Dtos.Incoming;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("SignIn")]
    public async Task<IActionResult> SignIn(SignInUserDto signInDto)
    {
        var user = await _userManager.FindByEmailAsync(signInDto.Email);

        if (user == null)
            return BadRequest();

        var isCorrect = await _userManager.CheckPasswordAsync(user, signInDto.Password);
        if (!isCorrect)
            return BadRequest();

        var jwtToken = await GenerateJwtToken(user);
        return Ok(jwtToken);
    }

    [HttpPost]
    [Route("SignUp")]
    public async Task<IActionResult> SignUp(SignUpUserDto signUp)
    {
        var user = await _userManager.FindByEmailAsync(signUp.Email);

        if (user != null)
            return BadRequest();

        var newUser = new IdentityUser()
        {
            Email = signUp.Email,
            UserName = signUp.Email
        };

        var isCreated = await _userManager.CreateAsync(newUser, signUp.Password);
        if (!isCreated.Succeeded)
            return BadRequest();

        var jwtToken = await GenerateJwtToken(newUser);
        return Ok(jwtToken);
    }

    private async Task<string> GenerateJwtToken(IdentityUser user)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        var roles = (await _userManager.GetRolesAsync(user)).Where(role => role != "User");
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,
                DateTime.Now.ToUniversalTime().ToString()),
            new Claim(ClaimTypes.Role, "User")
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }
}
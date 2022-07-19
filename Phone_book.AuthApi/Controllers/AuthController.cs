using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Phone_book.Auth.Common;
using Phone_book.Auth.Api.Models;

namespace Phone_book.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IOptions<AuthOptions> authOptions;

    public AuthController(IOptions<AuthOptions> authOptions)
    {
        this.authOptions = authOptions;
    }

    private List<Account> Accounts => new List<Account>
    {
        new Account()
        {
            Id = 1,
            Email = "1@1.1",
            Password = "1234",
            Role = new Role[] {Role.User}
        },
        new Account()
        {
            Id = 2,
            Email = "2@2.2",
            Password = "1234",
            Role = new Role[] {Role.User}
        },
        new Account()
        {
            Id = 3,
            Email = "admin@admin.admin",
            Password = "admin",
            Role = new Role[] {Role.Admin}
        }
    };

    [Route("login")]
    [HttpPost]
    public IActionResult Login([FromBody] Login request)
    {
        var user = AuthenticateUser(request.Email, request.Password);

        if (user != null)
        {
            var token = GenerateGWT(user);

            return Ok(new
            {
                access_token = token
            });
        }

        return Unauthorized();
    }

    private Account AuthenticateUser(string email, string password)
    {
        return Accounts.SingleOrDefault(u => u.Email == email && u.Password == password);
    }

    private string GenerateGWT(Account user)
    {
        var authParms = authOptions.Value;

        var securityKey = authParms.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        foreach (var role in user.Role)
        {
            claims.Add(new Claim("role", role.ToString()));
        }

        var token = new JwtSecurityToken(authParms.Issuer,
            authParms.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParms.TokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
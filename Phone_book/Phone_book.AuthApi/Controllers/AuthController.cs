using Microsoft.AspNetCore.Mvc;
using Phone_book.AuthApi.Models;

namespace Phone_book.AuthApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
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
    public IActionResult Login([FromBody]Login request)
    {
        var user = AuthenticateUser(request.Email, request.Password);

        if (user!=null)
        {
            
        }

        return Unauthorized();
    }

    private Account AuthenticateUser(string email, string password)
    {

        return Accounts.SingleOrDefault(u => u.Email == email && u.Password == password);
    }
}
using System.ComponentModel.DataAnnotations;

namespace Phone_book.AuthApi.Models;

public class Login
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
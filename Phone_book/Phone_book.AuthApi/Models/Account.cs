namespace Phone_book.AuthApi.Models;

public class Account
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role[] Role { get; set; }

}

public enum Role
{
    User,
    Admin
}
using System.ComponentModel.DataAnnotations;

namespace Phone_book.Data.Models;

public class Contact
{
    [Key]
    public int Id { get; set; }

    public string Surname { get; set; }

    public string Name { get; set; }

    public string Patronymic { get; set; }

    [MaxLength(12)]
    public string PhoneNumber { get; set; }

    public string Address { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Phone_book.Data.Models;

public class Contact
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Patronymic { get; set; }

    [MaxLength(12)]
    public string PhoneNumber { get; set; }

    [Required]
    public string Address { get; set; }
}

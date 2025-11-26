using System.ComponentModel.DataAnnotations;

namespace WebWasm.Models;

public class Users
{
    [Key]
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    
    public ICollection<Polls>? Polls { get; set; }
    public ICollection<Votes>? Votes { get; set; }
}
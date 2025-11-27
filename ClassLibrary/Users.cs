using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ClassLibrary;


public class Users : IdentityUser<int>
{
    public ICollection<Polls>? Polls { get; set; }
    public ICollection<Votes>? Votes { get; set; }
}
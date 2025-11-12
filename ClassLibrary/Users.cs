namespace ClassLibrary;

public class Users
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    
    public ICollection<Polls>? Polls { get; set; }
    public ICollection<Votes>? Votes { get; set; }
}
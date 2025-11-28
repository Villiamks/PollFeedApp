namespace WebWasm.DTOs;

public class UserDTO
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public List<PollDTO> Polls { get; set; } = [];
    public List<VoteDTO> Votes { get; set; } = [];
}
namespace ClassLibrary.DTOs;

public class UserDTO
{
    public int UserId { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public List<PollDTO> Polls { get; set; } = [];
    public List<VoteDTO> Votes { get; set; } = [];
}
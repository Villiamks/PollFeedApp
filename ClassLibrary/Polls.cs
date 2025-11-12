namespace ClassLibrary;

public class Polls
{
    public int PollId { get; set; }
    public string Question { get; set; }
    public int UserId { get; set; }
    
    public Users Creator { get; set; }
    public ICollection<VoteOptions>? Options { get; set; }
}
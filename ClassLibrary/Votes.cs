namespace ClassLibrary;

public class Votes
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int VoteOptionId { get; set; }
    
    public Users User { get; set; }
    public VoteOptions VoteOption { get; set; }
}
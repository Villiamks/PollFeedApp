namespace ClassLibrary;

public class VoteOptions
{
    public int VoteOptionId { get; set; }
    public string Caption { get; set; }
    public int PollId { get; set; }
    
    public Polls Poll { get; set; }
    public ICollection<Votes>? Votes { get; set; }
}
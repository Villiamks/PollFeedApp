using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary;

public class VoteOptions
{
    [Key]
    public int VoteOptionId { get; set; }
    public string Caption { get; set; }
    [ForeignKey(nameof(PollId))]
    public int PollId { get; set; }
    
    public Polls Poll { get; set; }
    public ICollection<Votes>? Votes { get; set; }
}
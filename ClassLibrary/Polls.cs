using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary;

public class Polls
{
    [Key]
    public int PollId { get; set; }
    public string Question { get; set; }
    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }
    
    public Users Creator { get; set; }
    public ICollection<VoteOptions>? Options { get; set; }
}
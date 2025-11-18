using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary;

public class Votes
{
    [Key]
    public int VoteId { get; set; }
    public int UserId { get; set; }
    [ForeignKey(nameof(VoteOptionId))]
    public int VoteOptionId { get; set; }
    
    public Users? User { get; set; }
    public VoteOptions? VoteOption{ get; set; }
}
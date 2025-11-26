namespace WebWasm.DTOs;

public class VoteDTO
{
    public int VoteId { get; set; }
    public int? UserId  { get; set; }
    public int VoteOptionId { get; set; }
}
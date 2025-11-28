namespace WebWasm.DTOs;

public class PollDTO
{
    public int PollId { get; set; }
    public int UserId { get; set; }
    public string Question { get; set; } = string.Empty;
    public List<VoteOptionDTO>? Options { get; set; } = new();
}
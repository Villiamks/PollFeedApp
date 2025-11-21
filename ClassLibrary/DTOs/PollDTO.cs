namespace ClassLibrary.DTOs;

public class PollDTO
{
    public int UserId { get; set; }
    public string Question { get; set; } = string.Empty;
    public List<VoteOptionDTO>? Options { get; set; } = new();
}
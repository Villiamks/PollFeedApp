namespace ClassLibrary.DTOs;

public class VoteOptionDTO
{
    public int VoteOptionId { get; set; }
    public string Caption { get; set; } = string.Empty;
    public List<VoteDTO> Votes = [];
}
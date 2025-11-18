using ClassLibrary;

namespace WebFront.Services;

public class VoteService : IVoteService
{
    private readonly HttpClient _httpClient;
    
    public VoteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Votes>?> GetVotes()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Votes>>("api/Votes");
    }

    public async Task<Votes?> GetVoteOption(int id)
    {
        return await _httpClient.GetFromJsonAsync<Votes>($"api/Votes/{id}");
    }

    public async Task CreateVote(Votes vote)
    {
        await _httpClient.PostAsJsonAsync("api/Votes", vote);
    }
    
    public async Task DeleteVote(int id)
    {
        await _httpClient.DeleteAsync($"api/Votes/{id}");
    }
}
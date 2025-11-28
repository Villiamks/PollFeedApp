using System.Net.Http.Json;
using WebWasm.Models;
using WebWasm.DTOs;

namespace WebWasm.Services;

public class VoteOptionService : IVoteOptionService
{
    private readonly HttpClient _httpClient;

    public VoteOptionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<VoteOptions>?> GetAllVoteOptions()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<VoteOptions>>("api/VoteOption");
    }

    public async Task<VoteOptions?> GetVoteOptionById(int id)
    {
        return await _httpClient.GetFromJsonAsync<VoteOptions>($"api/VoteOption/{id}");
    }

    public async Task<List<Votes>> GetVotes(VoteOptions voteOptions)
    {
        var list = await _httpClient.GetFromJsonAsync<IEnumerable<VoteDTO>>($"api/Vote/");
        return list?.Where(v => v.VoteOptionId == voteOptions.VoteOptionId ).Select(v => new Votes()
        {
            VoteId =  v.VoteId,
            UserId = v.UserId,
            VoteOptionId = v.VoteOptionId
        }).ToList() ?? [];
    }

    public async Task CreateVoteOption(VoteOptions voteOptions)
    {
        await _httpClient.PostAsJsonAsync("api/VoteOption", voteOptions);
    }

    public async Task DeleteVoteOption(int id)
    {
        await _httpClient.DeleteAsync($"api/VoteOption/{id}");
    }
}

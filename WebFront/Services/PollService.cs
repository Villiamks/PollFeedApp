using ClassLibrary;
using ClassLibrary.DTOs;

namespace WebFront.Services;

public class PollService : IPollService
{
    private readonly HttpClient _httpClient;

    public PollService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Polls>?> GetAllPolls()
    {
        var inn = await _httpClient.GetFromJsonAsync<IEnumerable<PollDTO>>("api/poll");
        return inn.Select(pdto => new Polls()
        {
            UserId = pdto.UserId,
            Question = pdto.Question,
            Options = pdto.Options.Select(opt => new VoteOptions()
            {
                Caption = opt.Caption
            }).ToList()
        }).ToList();
    }

    public async Task<Polls?> GetPollById(int pollId)
    {
        return await _httpClient.GetFromJsonAsync<Polls?>($"api/poll/{pollId}");
    }

    public async Task CreatePoll(PollDTO poll)
    {
        await _httpClient.PostAsJsonAsync("api/poll", poll);
    }
    
    public async Task DeletePoll(int id)
    {
        await _httpClient.DeleteAsync($"api/poll/{id}");
    }
}
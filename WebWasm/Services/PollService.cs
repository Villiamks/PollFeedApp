using System.Net.Http.Json;
using WebWasm.Models;
using WebWasm.DTOs;
using Microsoft.Extensions.Options;

namespace WebWasm.Services;

public class PollService : IPollService
{
    private readonly HttpClient _httpClient;

    public PollService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Polls>?> GetAllPolls(IVoteOptionService voteOptionService)
    {
        var inndto = await _httpClient.GetFromJsonAsync<IEnumerable<PollDTO>>("api/poll");
        var inn = inndto.Select(pdto => new Polls()
        {
            UserId = pdto.UserId,
            Question = pdto.Question,
            Options = pdto.Options?.Select(opt => new VoteOptions()
            {
                VoteOptionId = opt.VoteOptionId,
                Caption = opt.Caption
            }).ToList()
        }).ToList();

        foreach (Polls poll in inn)
        {
            foreach (var option in poll.Options ?? [])
            {
                option.Votes = await voteOptionService.GetVotes(option);
            }
        }

        return inn;
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

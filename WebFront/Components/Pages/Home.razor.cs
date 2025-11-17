using ClassLibrary;

namespace WebFront.Components.Pages;

public partial class Home
{
    private List<Polls> PollList = [];

    private async Task Vote(VoteOptions vo)
    {
        //TODO
    }

    private async Task MockPolls()
    {
        Users user1 = new Users()
        {
            UserId = 0,
            Email = "oioi@oi.com",
            UserName = "user1",
            PasswordHash = "",
            Salt = ""
        };
        VoteOptions vo1 = new VoteOptions()
        {
            VoteOptionId = 0,
            Caption = "Yes",
            PollId = 0,
            Votes = []
        };
        VoteOptions vo2 = new VoteOptions()
        {
            VoteOptionId = 1,
            Caption = "No",
            PollId = 0,
            Votes = []
        };
        VoteOptions vo3 = new VoteOptions()
        {
            VoteOptionId = 2,
            Caption = "Perhaps",
            PollId = 0,
            Votes = []
        };
        Polls poll1 = new Polls()
        {
            PollId = 0,
            UserId = 0,
            Creator = user1,
            Question = "Pizza?",
            Options = [vo1, vo2]
        };
        Polls poll2 = new Polls()
        {
            PollId = 1,
            UserId = 0,
            Creator = user1,
            Question = "Ja?",
            Options = [vo3]
        };

        PollList.Add(poll1);
        PollList.Add(poll2);
    }

    protected override async Task OnInitializedAsync()
    {
        await MockPolls();
    }
}

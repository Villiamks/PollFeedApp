namespace WebFront.Components.Layout;

public partial class Nav
{
    private bool showNav;

    private async Task ToggleNav()
    {
        showNav = !showNav;
    }

    private async Task Loggout()
    {
        //TODO 
    }

    protected async override Task OnInitializedAsync()
    {
        showNav = false;
    }
}
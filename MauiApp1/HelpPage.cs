namespace PingWall;

public class HelpPage : ContentPage
{
	public HelpPage()
	{
        
        Content = new WebView
        {
            Source = "help.html",
            
        };
        
        var backToolbarItem = new ToolbarItem
        {
            Priority = 1,
            Order=ToolbarItemOrder.Primary,
            IconImageSource="delete.png"
        };
        backToolbarItem.Clicked += async (sender, args) => { await Navigation.PopModalAsync(); };
        ToolbarItems.Add(
            backToolbarItem
            );
    }
}
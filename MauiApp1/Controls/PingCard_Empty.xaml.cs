using PingWall.Helpers;
using PingWall.ViewModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Controls;

public partial class PingCard_Empty : ContentView
{
	public PingCard_Empty()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        MessagingCenter.Send<object>(sender, MessagingCenterMsssages.ADD_NEW_BLANK_CARD);
    }
    private void HelpButton_Clicked(object sender, EventArgs e)
    {
        MessagingCenter.Send<object>(sender, MessagingCenterMsssages.HELP);
    }
}
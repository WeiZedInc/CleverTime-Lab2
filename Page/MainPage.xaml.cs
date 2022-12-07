using CleverTime.VM;

namespace CleverTime;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<MainVM>();
    }

    private async void OnCreateTimer_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CreateTimerPage");
        //SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("DetailsPage");
    }
}


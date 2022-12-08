using CleverTime.VM;
using CommunityToolkit.Maui.Converters;

namespace CleverTime;

public partial class MainPage : ContentPage
{
    MainVM mainVM;
    public static TTimer timer = null; 

    public MainPage()
    {
        InitializeComponent();
        mainVM = ServiceHelper.GetService<MainVM>();
        BindingContext = mainVM;
    }

    public void UpdateVisual() => this.InitializeComponent();

    private async void OnCreateTimer_Clicked(object sender, EventArgs e) => 
        await Shell.Current.GoToAsync("CreateTimerPage");

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        timer = FindTimerByName(frame.ClassId); 
        await Shell.Current.GoToAsync("DetailsPage"); 
    }

    TTimer FindTimerByName(string name) =>
        mainVM.AllTimers.First((timer) => timer.Name == name);

}


using CleverTime.VM;

namespace CleverTime;

public partial class MainPage : ContentPage
{
    DetailsPage detailsPage;
    MainVM mainVM;

    public MainPage()
    {
        InitializeComponent();
        mainVM = ServiceHelper.GetService<MainVM>();
        BindingContext = mainVM;
        detailsPage = ServiceHelper.GetService<DetailsPage>();
    }

    private async void OnCreateTimer_Clicked(object sender, EventArgs e) => 
        await Shell.Current.GoToAsync("CreateTimerPage");

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        detailsPage.Timer = FindTimerByName(frame.ClassId);
        await Shell.Current.GoToAsync("DetailsPage"); //  надо открыть страницу, потом присвоить таймер, оно не может присвоить пока страничка не прогрузилась
    }

    TTimer FindTimerByName(string name) =>
        mainVM.AllTimers.First((timer) => timer.Name == name);

}


﻿namespace CleverTime;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCreateTimer_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CreateTimerPage");
        //SemanticScreenReader.Announce(CounterBtn.Text);
    }


}


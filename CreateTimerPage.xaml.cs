using Microsoft.Maui;

namespace CleverTime;

public partial class CreateTimerPage : ContentPage
{
    TTimer timer;
    string TimerName;

    Label toTickHoursLabel, toTickMinutesLabel, toTickSecondsLabel, 
        toTickFromHoursLabel, toTickFromMinutes, toTickFromSeconds;
    Slider toTickHoursSlider, toTickMinutesSlider, toTickSecondsSlider,
        toTickFromHoursSlider, toTickFromMinutesSlider, toTickFromSecondsSlider;

    Grid TimeToTick_Grid, TimeToTickFrom_Grid;
    int hoursToTick, minutesToTick, secondsToTick, 
        hoursToTickFrom, minutesToTickFrom, secondsToTickFrom = 0;

    public CreateTimerPage()
	{
		InitializeComponent();
        DrawTimeToTick_Sliders();
    }

    private void NameInput_Completed(object sender, EventArgs e) => TimerName = NameInput.Text;

    private void isFromNowCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (e.Value == false)
		{
            DrawTimeToStartFrom_Sliders(); // draw slider to choose time where to start from
        }
		else
        {
            InputsLayout.Remove(TimeToTickFrom_Grid);
        }
	}

    private void doNotDisturbCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // todo
    }

    private void SaveButton_Clicked(object sender, EventArgs e) => SaveTimer(false);
    private void SaveAndRunButton_Clicked(object sender, EventArgs e) => SaveTimer(true);

    void SaveTimer(bool run)
    {
        var now = DateTime.Now;
        if (isFromNowCheckBox.IsEnabled)
            timer.StartTickTime = now;
        else
            timer.StartTickTime = new DateTime(now.Year, now.Month, now.Day, hoursToTickFrom, minutesToTickFrom, secondsToTickFrom);

        //end time for calculations
        timer.TotalTimeToTick = new(hoursToTick, minutesToTick, secondsToTick);
        timer.EndTickTime = new DateTime(now.Year, now.Month, now.Day, hoursToTick, minutesToTick, secondsToTick);

        if (run)
        {

        }
        else
        {

        }
    }

    #region TimeToTick
    void DrawTimeToTick_Sliders()
    {
        TimeToTick_Grid = new Grid
        {
            Margin = new Thickness(10),
            RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition()
            }
        };

        toTickHoursSlider = new Slider();
        toTickHoursSlider.ValueChanged += OnHoursSliderChanged;
        toTickHoursSlider.Minimum = 0;
        toTickHoursSlider.Maximum = 24;
        Grid.SetRow(toTickHoursSlider, 1);
        TimeToTick_Grid.Add(toTickHoursSlider);

        toTickMinutesSlider = new Slider();
        toTickMinutesSlider.ValueChanged += OnMinutesSliderChanged;
        toTickMinutesSlider.Minimum = 0;
        toTickMinutesSlider.Maximum = 60;
        Grid.SetRow(toTickMinutesSlider, 3);
        TimeToTick_Grid.Add(toTickMinutesSlider);

        toTickSecondsSlider = new Slider();
        toTickSecondsSlider.ValueChanged += OnSecondsSliderChanged;
        toTickSecondsSlider.Minimum = 0;
        toTickSecondsSlider.Maximum = 60;
        Grid.SetRow(toTickSecondsSlider, 5);
        TimeToTick_Grid.Add(toTickSecondsSlider);

        var timeToTickLabel = new Label();
        timeToTickLabel.Text = "Choose time to tick";
        timeToTickLabel.HorizontalOptions = LayoutOptions.Center;
        TimeToTick_Grid.Add(timeToTickLabel);

        toTickHoursLabel = new Label();
        Grid.SetRow(toTickHoursLabel, 2);
        TimeToTick_Grid.Add(toTickHoursLabel);

        toTickMinutesLabel = new Label();
        Grid.SetRow(toTickMinutesLabel, 4);
        TimeToTick_Grid.Add(toTickMinutesLabel);

        toTickSecondsLabel = new Label();
        Grid.SetRow(toTickSecondsLabel, 6);
        TimeToTick_Grid.Add(toTickSecondsLabel);

        InputsLayout.Add(TimeToTick_Grid);
    }
    void OnHoursSliderChanged(object sender, ValueChangedEventArgs e)
    {
        hoursToTick = ((int)toTickHoursSlider.Value);
        toTickHoursLabel.Text = "Hours: " + hoursToTick.ToString();
    }
    void OnMinutesSliderChanged(object sender, ValueChangedEventArgs e)
    {
        minutesToTick = ((int)toTickMinutesSlider.Value);
        toTickMinutesLabel.Text = "Minutes: " + minutesToTick.ToString();
    }
    void OnSecondsSliderChanged(object sender, ValueChangedEventArgs e)
    {
        secondsToTick = ((int)toTickSecondsSlider.Value);
        toTickSecondsLabel.Text = "Seconds: " + secondsToTick.ToString();
    }
    #endregion

    #region TimeToTickFrom
    void DrawTimeToStartFrom_Sliders()
    {
        TimeToTickFrom_Grid = new Grid
        {
            Margin = new Thickness(10),
            RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition()
            }
        };

        toTickFromHoursSlider = new Slider();
        toTickFromHoursSlider.ValueChanged += OnStartHoursSliderChanged;
        toTickFromHoursSlider.Minimum = 0;
        toTickFromHoursSlider.Maximum = 24;
        Grid.SetRow(toTickFromHoursSlider, 1);
        TimeToTickFrom_Grid.Add(toTickFromHoursSlider);

        toTickFromMinutesSlider = new Slider();
        toTickFromMinutesSlider.ValueChanged += OnStartMinutesSliderChanged;
        toTickFromMinutesSlider.Minimum = 0;
        toTickFromMinutesSlider.Maximum = 60;
        Grid.SetRow(toTickFromMinutesSlider, 3);
        TimeToTickFrom_Grid.Add(toTickFromMinutesSlider);

        toTickFromSecondsSlider = new Slider();
        toTickFromSecondsSlider.ValueChanged += OnStartSecondsSliderChanged;
        toTickFromSecondsSlider.Minimum = 0;
        toTickFromSecondsSlider.Maximum = 60;
        Grid.SetRow(toTickFromSecondsSlider, 5);
        TimeToTickFrom_Grid.Add(toTickFromSecondsSlider);

        var timeToTickLabel = new Label();
        timeToTickLabel.Text = "Choose time to tick from";
        timeToTickLabel.HorizontalOptions = LayoutOptions.Center;
        TimeToTickFrom_Grid.Add(timeToTickLabel);

        toTickFromHoursLabel = new Label();
        Grid.SetRow(toTickFromHoursLabel, 2);
        TimeToTickFrom_Grid.Add(toTickFromHoursLabel);

        toTickFromMinutes = new Label();
        Grid.SetRow(toTickFromMinutes, 4);
        TimeToTickFrom_Grid.Add(toTickFromMinutes);

        toTickFromSeconds = new Label();
        Grid.SetRow(toTickFromSeconds, 6);
        TimeToTickFrom_Grid.Add(toTickFromSeconds);

        InputsLayout.Add(TimeToTickFrom_Grid);
    }

    void OnStartHoursSliderChanged(object sender, ValueChangedEventArgs e)
    {
        hoursToTickFrom = ((int)toTickFromHoursSlider.Value);
        toTickFromHoursLabel.Text = "Hours: " + hoursToTickFrom.ToString();
    }
    void OnStartMinutesSliderChanged(object sender, ValueChangedEventArgs e)
    {
        minutesToTickFrom = ((int)toTickFromMinutesSlider.Value);
        toTickFromMinutes.Text = "Minutes: " + minutesToTickFrom.ToString();
    }
    void OnStartSecondsSliderChanged(object sender, ValueChangedEventArgs e)
    {
        secondsToTickFrom = ((int)toTickFromSecondsSlider.Value);
        toTickFromSeconds.Text = "Seconds: " + secondsToTickFrom.ToString();
    }
    #endregion
}
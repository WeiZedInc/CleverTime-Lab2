using Microsoft.Maui;

namespace CleverTime;

public partial class CreateTimerPage : ContentPage
{
    TTimer Timer;
    string TimerName;

    Label toTickHoursLabel, toTickMinutesLabel, toTickSecondsLabel;
    Slider toTickHoursSlider, toTickMinutesSlider, toTickSecondsSlider;

    Grid TimerGrid;
    VerticalStackLayout AlarmLayout;
    int hoursToTick, minutesToTick, secondsToTick = 0;

    TimePicker timePicker;
    DatePicker datePicker;

    bool isAlarm = false;

    public CreateTimerPage()
	{
		InitializeComponent();
        DrawTimerGrid();
    }

    private void NameInput_Completed(object sender, EventArgs e) => TimerName = NameInput.Text;

    private void isAlarm_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (e.Value == true)
		{
            InputsLayout.Remove(TimerGrid);
            DrawAlarmLayout();
            isAlarm = true;
        }
		else
        {
            InputsLayout.Remove(AlarmLayout);
            DrawTimerGrid();
            isAlarm = false;
        }
    }

    private void doNotDisturbCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // todo
    }

    private void SaveButton_Clicked(object sender, EventArgs e) => SaveTimer(false);
    private void SaveAndRunButton_Clicked(object sender, EventArgs e) => SaveTimer(true);

    async void SaveTimer(bool run)
    {
        if (isAlarm == false)
        {
            var now = DateTime.Now;

            Timer.TotalTimeToTick = new(hoursToTick, minutesToTick, secondsToTick);

            if (run)
                Timer.StartTickTime = now;
            else
                Timer.StartTickTime = default;
        }
        else // if alarm
        {
            if (datePicker.Date == DateTime.Today && timePicker.Time <= DateTime.Now.TimeOfDay)
            {
                await DisplayAlert("Ooops", "You cant confirm time which had already past ;c" +
                    "\nTime switched to most closerly possible.", "Try again");
                timePicker.Time = DateTime.Now.TimeOfDay.Add(new TimeSpan(0, 1, 0));
                return;
            }
        }

    }

    #region TimerGrid
    void DrawTimerGrid()
    {
        TimerGrid = new Grid
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
        TimerGrid.Add(toTickHoursSlider);

        toTickMinutesSlider = new Slider();
        toTickMinutesSlider.ValueChanged += OnMinutesSliderChanged;
        toTickMinutesSlider.Minimum = 0;
        toTickMinutesSlider.Maximum = 60;
        Grid.SetRow(toTickMinutesSlider, 3);
        TimerGrid.Add(toTickMinutesSlider);

        toTickSecondsSlider = new Slider();
        toTickSecondsSlider.ValueChanged += OnSecondsSliderChanged;
        toTickSecondsSlider.Minimum = 0;
        toTickSecondsSlider.Maximum = 60;
        Grid.SetRow(toTickSecondsSlider, 5);
        TimerGrid.Add(toTickSecondsSlider);

        var timeToTickLabel = new Label();
        timeToTickLabel.Text = "Choose time to tick";
        timeToTickLabel.HorizontalOptions = LayoutOptions.Center;
        TimerGrid.Add(timeToTickLabel);

        toTickHoursLabel = new Label();
        Grid.SetRow(toTickHoursLabel, 2);
        TimerGrid.Add(toTickHoursLabel);

        toTickMinutesLabel = new Label();
        Grid.SetRow(toTickMinutesLabel, 4);
        TimerGrid.Add(toTickMinutesLabel);

        toTickSecondsLabel = new Label();
        Grid.SetRow(toTickSecondsLabel, 6);
        TimerGrid.Add(toTickSecondsLabel);

        InputsLayout.Add(TimerGrid);
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

    #region AlarmLayout
    void DrawAlarmLayout()
    {
        AlarmLayout = new VerticalStackLayout {
            Margin = new Thickness(10),
            Spacing = 10,
            HorizontalOptions = LayoutOptions.CenterAndExpand
            
        };

        timePicker = new TimePicker
        {
            Time = new TimeSpan(8, 0, 0),
        };

        datePicker = new DatePicker
        {
            MinimumDate = DateTime.Today,
            MaximumDate = DateTime.MaxValue,
            Date = DateTime.Today,
        };
        datePicker.HorizontalOptions = LayoutOptions.CenterAndExpand;

        datePicker.DateSelected += DatePicker_DateSelected;
        AlarmLayout.Add(datePicker);
        AlarmLayout.Add(timePicker);

        InputsLayout.Add(AlarmLayout);
    }

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        
    }

    #endregion
}
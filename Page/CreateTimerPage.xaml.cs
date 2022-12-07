using CleverTime.VM;

namespace CleverTime;

public partial class CreateTimerPage : ContentPage
{
    TTimer timer;

    Label toTickHoursLabel, toTickMinutesLabel, toTickSecondsLabel;
    Slider toTickHoursSlider, toTickMinutesSlider, toTickSecondsSlider;

    Grid TimerGrid;
    VerticalStackLayout AlarmLayout;
    int hoursToTick, minutesToTick, secondsToTick = 0;

    TimePicker timePicker;
    DatePicker datePicker;

    bool isAlarm = false;
    bool doNotDisturb = false;

    readonly MainVM mainVM;

    public CreateTimerPage()
	{
		InitializeComponent();
        DrawTimerGrid();
        timer = new TTimer();
        mainVM = ServiceHelper.GetService<MainVM>();
    }

    private void NameInput_Completed(object sender, EventArgs e) => timer.Name = NameInput.Text;
    private void DescriptionInput_Completed(object sender, EventArgs e) => timer.GroupName = DescriptionInput.Text;

    private void doNotDisturbCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e) => doNotDisturb = !doNotDisturb;
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

    private async void AddToGroupCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // returns choosed action in string type
        if (AddToGroupCheckBox.IsChecked == false) // to avoid event looping
        {
            if (timer.GroupName != TTimer.DEFAULT_GROUP)
                timer.GroupName = TTimer.DEFAULT_GROUP;
            return;
        }
        else if (AddToGroupCheckBox.IsChecked)
        {
            string action = await DisplayActionSheet("Choose Group:", "Cancel", "Add new", mainVM.Groups.ToArray());
            if (action == "Cancel" || string.IsNullOrWhiteSpace(action))
            {
                AddToGroupCheckBox.IsChecked = false;
                return;
            }
            else if (action == "Add new")
            {
                AddNewGroup();
            }
            else if (action == TTimer.DEFAULT_GROUP)
            {
                AddToGroupCheckBox.IsChecked = false;
                timer.GroupName = TTimer.DEFAULT_GROUP;
            }
            else
            {
                AddToGroupCheckBox.IsChecked = true;
                timer.GroupName = action;
            }
        }
    }
    async void AddNewGroup() 
    {
        string groupName = await DisplayPromptAsync("Add group", "Input desired group name.");
        if (string.IsNullOrWhiteSpace(groupName) || mainVM.Groups.Contains(groupName))
        {
            await DisplayAlert("Ooops", "Incorrect name for the group ;c", "Try again");
            AddToGroupCheckBox.IsChecked = false;
            return;
        }
        else
        {
            mainVM.Groups.Add(groupName);
            bool isAddingToNewGroup = await DisplayAlert("Success", $"You created a group {groupName}!\n" +
                $"Do you want to add this timer to {groupName}?", "Yes", "No");
            if (isAddingToNewGroup)
            {
                timer.GroupName = groupName;
                AddToGroupCheckBox.IsChecked = true;
            }
            else
                AddToGroupCheckBox.IsChecked = false;
        }
    }

    private void SaveButton_Clicked(object sender, EventArgs e) => SaveTimer(false);
    private void SaveAndRunButton_Clicked(object sender, EventArgs e) => SaveTimer(true);
    async void SaveTimer(bool run)
    {
        if (isAlarm == false)
        {
            timer.TimerTimeToTick = new(hoursToTick, minutesToTick, secondsToTick);
            timer.doNotDisturb = doNotDisturb;
            if (run)
            {
                timer.isRunning = true;
                timer.StartTickTime = DateTime.Now;
            }
            else
            {
                timer.isRunning = false;
                timer.StartTickTime = default;
            }
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

            timer.doNotDisturb = doNotDisturb;
            if (run)
            {
                timer.isRunning = true;
                timer.AlarmDateTime = datePicker.Date.Add(timePicker.Time);
            }
            else
                timer.isRunning = false;
        }
        mainVM.AllTimers.Add(timer);
    }

    #region Layouts

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
        AlarmLayout = new VerticalStackLayout
        {
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

        AlarmLayout.Add(datePicker);
        AlarmLayout.Add(timePicker);

        InputsLayout.Add(AlarmLayout);
    }

    #endregion

    #endregion
}
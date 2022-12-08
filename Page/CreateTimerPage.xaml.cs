using CleverTime.VM;

namespace CleverTime;

public partial class CreateTimerPage : ContentPage
{
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
    string GroupName = TTimer.DEFAULT_GROUP;

    public CreateTimerPage()
    {
        InitializeComponent();
        DrawTimerGrid();
        mainVM = ServiceHelper.GetService<MainVM>();
    }

    private async void NameInput_Completed(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameInput.Text))
        {
            await DisplayAlert("Ooops", "Incorrect name ;c", "Try again");
            return;
        }
    }

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
            if (GroupName != TTimer.DEFAULT_GROUP)
                GroupName = TTimer.DEFAULT_GROUP;
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
                GroupName = TTimer.DEFAULT_GROUP;
            }
            else
            {
                AddToGroupCheckBox.IsChecked = true;
                GroupName = action;
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
                GroupName = groupName;
                AddToGroupCheckBox.IsChecked = true;
            }
            else
                AddToGroupCheckBox.IsChecked = false;
        }
    }

    private void SaveButton_Clicked(object sender, EventArgs e) => SaveTimer(false);
    private void SaveAndRunButton_Clicked(object sender, EventArgs e) => SaveTimer(true);
    TTimer CreateTimerAndSaveData() => new TTimer(name: NameInput.Text, description: DescriptionInput.Text, doNotDisturb: doNotDisturb, groupName: GroupName, isAlarm: isAlarm);
    void ClearData()
    {
        toTickHoursLabel.Text = "Hours: 0";
        toTickMinutesLabel.Text = "Minutest: 0";
        toTickSecondsLabel.Text = "Seconds: 0";
        toTickHoursSlider.Value = 0;
        toTickMinutesSlider.Value = 0;
        toTickSecondsSlider.Value = 0;
        hoursToTick = 0;
        minutesToTick = 0;
        secondsToTick = 0;
        if (isAlarm)
            InputsLayout.Remove(AlarmLayout);
        isAlarm = false;
        MakeAlarmCheckBox.IsChecked = false;
        doNotDisturb = false;
        GroupName = TTimer.DEFAULT_GROUP;
    }
    async void SaveTimer(bool run)
    {
        var timer = CreateTimerAndSaveData();
        try
        {
            if (isAlarm == false)
            {
                var timeToTick = new TimeSpan(hoursToTick, minutesToTick, secondsToTick);
                timer.TimeToEndTicking = new DateTime().Add(timeToTick);
                if (run)
                {
                    timer.isRunning = true;
                    timer.TickingStartedDateTime = DateTime.Now;
                    timer.SetupTimer(timeToTick, start: true);

                }
                else
                {
                    timer.isRunning = false;
                    timer.TickingStartedDateTime = default;
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
                    timer.TickingStartedDateTime = DateTime.Now;
                }
                else
                {
                    timer.isRunning = false;
                    timer.TickingStartedDateTime = default;
                }
                timer.WhenToAlarmDateTime = datePicker.Date.Add(timePicker.Time);
                timer.TimeToEndTicking = timer.WhenToAlarmDateTime;
            }
            mainVM.AllTimers.Add(timer);
            ClearData();
            await Shell.Current.GoToAsync("../");
        }
        catch (Exception)
        {
            throw;
        }
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
using Microsoft.Maui;

namespace CleverTime;

public partial class CreateTimerPage : ContentPage
{
    Label hoursLabel, minutesLabel, secondsLabel;
    Slider hoursSlider, minutesSlider, secondsSlider;
    Grid childGrid;
    Timer startFromTimer;
    // Нужно сделать появление timepicker/datepicker если isFromNowCheckBox_CheckedChanged выключили
    public CreateTimerPage()
	{
		InitializeComponent();
        CreateInputSliders();
    }

    private void NameInput_Completed(object sender, EventArgs e)
	{
        string text = ((Entry)sender).Text;
    }

    private void isFromNowCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (e.Value == false)
		{
            //startFromTimer = new();
        }
		else
        {
            startFromTimer = null;
        }
	}

    private void doNotDisturbCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // todo
    }

    void CreateInputSliders()
    {
        childGrid = new Grid
        {
            Margin = new Thickness(10),
            RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition()
            }
        };

        hoursSlider = new Slider();
        hoursSlider.ValueChanged += OnHoursSliderChanged;
        hoursSlider.Minimum = 0;
        hoursSlider.Maximum = 24;
        childGrid.Add(hoursSlider);

        minutesSlider = new Slider();
        minutesSlider.ValueChanged += OnMinutesSliderChanged;
        minutesSlider.Minimum = 0;
        minutesSlider.Maximum = 60;
        Grid.SetRow(minutesSlider, 2);
        childGrid.Add(minutesSlider);

        secondsSlider = new Slider();
        secondsSlider.ValueChanged += OnSecondsSliderChanged;
        secondsSlider.Minimum = 0;
        secondsSlider.Maximum = 60;
        Grid.SetRow(secondsSlider, 4);
        childGrid.Add(secondsSlider);


        hoursLabel = new Label();
        Grid.SetRow(hoursLabel, 1);
        childGrid.Add(hoursLabel);

        minutesLabel = new Label();
        Grid.SetRow(minutesLabel, 3);
        childGrid.Add(minutesLabel);

        secondsLabel = new Label();
        Grid.SetRow(secondsLabel, 5);
        childGrid.Add(secondsLabel);

        InputsLayout.Add(childGrid);
    }

    void OnHoursSliderChanged(object sender, ValueChangedEventArgs e)
    {
        hoursLabel.Text = "Hours: " + ((int)hoursSlider.Value).ToString();
    }
    void OnMinutesSliderChanged(object sender, ValueChangedEventArgs e)
    {
        minutesLabel.Text = "Minutes: " + ((int)minutesSlider.Value).ToString();
    }
    void OnSecondsSliderChanged(object sender, ValueChangedEventArgs e)
    {
        secondsLabel.Text = "Seconds: " + ((int)secondsSlider.Value).ToString();
    }
}
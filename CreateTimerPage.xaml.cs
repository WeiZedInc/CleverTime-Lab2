namespace CleverTime;

public partial class CreateTimerPage : ContentPage
{
	public CreateTimerPage()
	{
		InitializeComponent();
	}


	private void NameInput_Completed(object sender, EventArgs e)
	{
        string text = ((Entry)sender).Text;
    }
}
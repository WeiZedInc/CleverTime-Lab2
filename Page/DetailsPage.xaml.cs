using CleverTime.VM;

namespace CleverTime;

public partial class DetailsPage : ContentPage
{
	public DetailsPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<DetailsVM>();
    }
}
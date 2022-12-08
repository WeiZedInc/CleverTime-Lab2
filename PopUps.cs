using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace CleverTime
{
    public static class PopUps
    {
        public async static void ShowToast(string text = "I'm a toast", ToastDuration duration = ToastDuration.Short, int
        textSize = 14, CancellationTokenSource cTs = null)
        {
            if (cTs == null)
                cTs = new CancellationTokenSource();

            await Toast.Make(text, duration, textSize).Show(cTs.Token);
        }

        public async static void ShowSnackBar(string text = "This is a Snackbar", string actionButtonText = "Click Here to Dismiss",
            Action action = null, CancellationTokenSource cTs = null)
        {
            if (cTs == null)
                cTs = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.Green,
                ActionButtonTextColor = Colors.Yellow,
                CornerRadius = new CornerRadius(10),
                Font = Font.SystemFontOfSize(14),
                ActionButtonFont = Font.SystemFontOfSize(14),
                CharacterSpacing = 0.5
            };

            TimeSpan duration = TimeSpan.FromSeconds(3);
            await Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions).Show(cTs.Token);
        }
    }
}

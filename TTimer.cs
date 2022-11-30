using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace CleverTime
{
    class TTimer
    {
        public const string DEFAULT_GROUP = "default";
        public static Dictionary<string, TTimer> AllTimers;
        public static List<string> Groups;
        public DateTime StartTickTime { get; set; } 
        public DateTime AlarmDateTime { get; set; }
        public TimeSpan TimerTimeToTick { get; set; } // hh,mm,ss
        public bool isRunning { get; set; }
        public bool isAlarm { get; set; }
        public bool doNotDistub { get; set; }
        public string groupName = DEFAULT_GROUP;

        static TTimer()
        {
            AllTimers = new();
            Groups = new() { DEFAULT_GROUP };
        }

        public static bool isGroupsEmpty() => Groups.Count == 1 ? true : false;

        public async static void ShowToast(string text = "I'm a toast", ToastDuration duration = ToastDuration.Short,int
        textSize = 14, CancellationTokenSource cTs = null)
        {
            if (cTs == null)
                cTs = new CancellationTokenSource();

            await Toast.Make(text, duration, textSize).Show(cTs.Token);
        }

        public async static void ShowSnakeBar(string text = "This is a Snackbar", string actionButtonText = "Click Here to Dismiss",
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

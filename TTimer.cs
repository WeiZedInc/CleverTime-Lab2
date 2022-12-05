using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using Font = Microsoft.Maui.Font;

namespace CleverTime
{
    public class TTimer
    {
        //1.Таймери на певний проміжок часу(власне таймери) або до заданого моменту часу(будильники), Можливість одночасного запуску декількох(довільної кількості) таймерів
        //2.ручний інтерфейс для перегляду списку таймерів, керування таймерами, реалізувати як один список, можливо з додатковою фільтрацією
        //3.Звуковий та візуальний сигнал по завершенню часу.
        //4.Можливість виконання певних налаштовуваних дій по завершенню часу(наприклад, запуск програми чи відкриття документу).
        
        public const string DEFAULT_GROUP = "default";
        public DateTime StartTickTime { get; set; }
        public DateTime AlarmDateTime { get; set; }
        public TimeSpan TimerTimeToTick { get; set; } // hh,mm,ss
        public bool isRunning { get; set; }
        public bool isAlarm { get; set; }
        public bool doNotDisturb { get; set; }
        public string groupName = DEFAULT_GROUP;
        public string Name, Description;

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

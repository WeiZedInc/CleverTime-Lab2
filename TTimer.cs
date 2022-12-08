using System.Timers;

namespace CleverTime
{
    public class TTimer
    {
        //1.Таймери на певний проміжок часу(власне таймери) або до заданого моменту часу(будильники), Можливість одночасного запуску декількох(довільної кількості) таймерів
        //2.ручний інтерфейс для перегляду списку таймерів, керування таймерами, реалізувати як один список, можливо з додатковою фільтрацією
        //3.Звуковий та візуальний сигнал по завершенню часу.
        //4.Можливість виконання певних налаштовуваних дій по завершенню часу(наприклад, запуск програми чи відкриття документу).

        public TTimer(DateTime startTickTime = default, DateTime alarmDateTime= default, TimeSpan timerTimeToTick= default, 
            bool isRunning = false, bool isAlarm = false, bool doNotDisturb = false, string groupName = DEFAULT_GROUP, string name = "test", string description = "test descr")
        {
            TickingStartedDateTime = startTickTime; 
            WhenToAlarmDateTime = alarmDateTime;
            this.isAlarm = isAlarm;
            this.isRunning = isRunning;
            this.doNotDisturb = doNotDisturb;
            GroupName = groupName;
            Name = name;
            Description = description;

            if (isAlarm)
                TimeToEndTicking = alarmDateTime;
            else
                TimeToEndTicking.Add(timerTimeToTick);
        }
        
        public const string DEFAULT_GROUP = "default";
        public bool isRunning { get; set; }
        public bool isAlarm { get; set; }
        public bool doNotDisturb { get; set; }
        public string GroupName { get; set; } = DEFAULT_GROUP;
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public System.Timers.Timer Timer { get; set; }
        public DateTime TickingStartedDateTime { get; set; } // always
        public DateTime WhenToAlarmDateTime { get; set; } // if alarm
        public DateTime TimeToEndTicking { get; set; } // if timer  (gonna be for visual)
        public DateTime CurrentGoneTime { get; set; } = default; // need to be calculated

        public static void OnTimerEvent(Object source, ElapsedEventArgs e)
        {
            PopUps.ShowToast(text: "timer");
        }

        public static void OnAlarmEvent(Object source, ElapsedEventArgs e)
        {
            PopUps.ShowToast(text: "alarm");
        }

        async Task StartVisualTimerTicker()
        {
            var second = new TimeSpan(0, 0, 1);
            while (TimeToEndTicking.Subtract(DateTime.Now) != TimeSpan.Zero)
            {
                TimeToEndTicking.Subtract(second);
                await Task.Delay(1000);
            }
        }

        private void SetupTimer(TimeSpan timeToTick, bool isRepeated = false, bool start = false)
        {
            Timer = new System.Timers.Timer(timeToTick);
            Timer.Elapsed += TTimer.OnTimerEvent;
            Timer.AutoReset = isRepeated;
            if (start)
            {
                StartVisualTimerTicker();
                Timer.Start();
            }
        }

        private void SetupAlarm(DateTime endTime, bool isRepeated = false)
        {
            TimeSpan timeDifference = endTime.Subtract(DateTime.Now);
            Timer = new System.Timers.Timer(timeDifference);
            Timer.Elapsed += TTimer.OnAlarmEvent;
            Timer.AutoReset = isRepeated;
        }
    }
}

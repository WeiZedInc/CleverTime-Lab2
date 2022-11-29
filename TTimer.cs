using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}

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
        public static List<TTimer> AllTimers;
        public DateTime StartTickTime { get; set; }
        public TimeSpan TotalTimeToTick { get; set; }

        static TTimer()
        {
            AllTimers = new();
        }
        public TTimer()
        {
            AllTimers.Add(this);
        }
    }
}

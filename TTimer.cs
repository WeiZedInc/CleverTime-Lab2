using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverTime
{
    class TTimer
    {
        public DateTime StartTickTime { get; set; }
        public DateTime EndTickTime { get; set; }
        public TimeSpan TotalTimeToTick { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayrollTimeclock
{
    public class TimePair
    {
        public readonly ClockEvent StartEvent;
        public readonly ClockEvent EndEvent;
        public readonly TimeSpan Length;
        public readonly bool IsOpen;

        public TimePair(ClockEvent startEvent, ClockEvent endEvent)
        {
            StartEvent = startEvent;
            EndEvent = endEvent;
            if (EndEvent != null)
            {
                Length = EndEvent.InOutDateTime.Subtract(StartEvent.InOutDateTime);
                IsOpen = false;
            }
            else
            {
                Length = new TimeSpan(0);
                IsOpen = true;
            }
        }

        public bool IsAbsent
        {
            get
            {
                if (StartEvent != null && StartEvent.Status == EventStatus.Absent)
                    return true;
                if (EndEvent != null && EndEvent.Status == EventStatus.Absent)
                    return true;
                return false;
            }
        }
    }
}

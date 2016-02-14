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
                if (IsMixed)
                    return false;
                return StartEvent.Status == EventStatus.Absent;
            }
        }

        public bool IsExtra
        {
            get
            {
                if (IsMixed)
                    return false;
                return StartEvent.Status == EventStatus.Extra;
            }
        }

        public bool IsMixed
        {
            get
            {
                if (StartEvent == null || EndEvent == null)
                    return true;
                return (StartEvent.Status == EventStatus.Overridden ? EventStatus.Normal : StartEvent.Status)
                    != (EndEvent.Status == EventStatus.Overridden ? EventStatus.Normal : EndEvent.Status);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PayrollTimeclock
{

    public class ClockEvent
    {
        public DateTime InOutDateTime;
        public DateTime ActionDateTime;
        public EventStatus Status;

        public ClockEvent(DateTime inOutDateTime, DateTime actionDateTime, EventStatus status)
        {
            InOutDateTime = inOutDateTime;
            ActionDateTime = actionDateTime;
            Status = status;
        }

        public void Write(TextWriter writer)
        {
            string statusText;
            switch (Status)
            {
                case EventStatus.Normal: statusText = "N"; break;
                case EventStatus.Overridden: statusText = "O"; break;
                case EventStatus.Deleted: statusText = "D"; break;
                case EventStatus.Absent: statusText = "A"; break;
                default: throw new NotImplementedException("Unrecognized ClockEvent status");
            }
            writer.WriteLine(InOutDateTime.ToString("g") + "|" +
                ActionDateTime.ToString("g") + "|" + statusText);
        }

        public static ClockEvent Read(TextReader reader)
        {
            string line = reader.ReadLine();
            if (line == null)
                return null;
            string[] parts = line.Split('|');
            DateTime inOutDateTime;
            DateTime actionDateTime;
            EventStatus status;
            if (!DateTime.TryParse(parts[0], out inOutDateTime))
                throw new InvalidDataException("Invalid in/out datetime in " + line);
            if (!DateTime.TryParse(parts[1], out actionDateTime))
                throw new InvalidDataException("Invalid action datetime in " + line);
            switch (parts[2])
            {
                case "N": status = EventStatus.Normal; break;
                case "O": status = EventStatus.Overridden; break;
                case "D": status = EventStatus.Deleted; break;
                case "A": status = EventStatus.Absent; break;
                default: throw new InvalidDataException("Invalid status in " + line);
            }
            ClockEvent clockEvent = new ClockEvent(inOutDateTime, actionDateTime, status);
            return clockEvent;
        }

        public static DateTime Round(DateTime when)
        {
            DateTime whenRounded = new DateTime(when.Year, when.Month, when.Day, when.Hour, when.Minute, 0);
            return whenRounded;
        }
    }
}

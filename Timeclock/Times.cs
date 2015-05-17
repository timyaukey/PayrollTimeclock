using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PayrollTimeclock
{
    public enum EventStatus
    {
        Normal = 0,
        Overridden = 1,
        Deleted = 2
    }

    public class Times
    {
        public readonly string FolderName;
        private LinkedList<ClockEvent> _Events;
        private bool _Modified;

        public static Times Load(string folderName)
        {
            Times result = new Times(folderName);
            result.LoadFromFile();
            return result;
        }

        private Times(string folderName)
        {
            FolderName = folderName;
            _Events = new LinkedList<ClockEvent>();
            _Modified = false;
        }

        private void LoadFromFile()
        {
            using (TextReader reader = new StreamReader(FileName))
            {
                for (; ; )
                {
                    ClockEvent clockEvent = ClockEvent.Read(reader);
                    if (clockEvent == null)
                        break;
                    _Events.AddLast(clockEvent);
                }
            }
        }

        public void SaveToFile()
        {
            if (_Modified)
            {
                string tempFile = FileName + ".new";
                using (TextWriter writer = new StreamWriter(tempFile))
                {
                    foreach (ClockEvent clockEvent in _Events)
                    {
                        clockEvent.Write(writer);
                    }
                }
                if (File.Exists(FileName))
                    File.Delete(FileName);
                File.Move(tempFile, FileName);
            }
        }

        public void Get(PayrollPeriod period, out List<TimePair> timePairs, out double overtimeHours)
        {
            timePairs = new List<TimePair>();
            ClockEvent startEvent = null;
            List<ClockEvent> eventsInPeriod = new List<ClockEvent>();
            foreach (ClockEvent clockEvent in _Events)
            {
                if (clockEvent.InOutDateTime.Date >= period.StartDate &&
                    clockEvent.InOutDateTime.Date <= period.EndDate &&
                    clockEvent.Status != EventStatus.Deleted)
                {
                    eventsInPeriod.Add(clockEvent);
                }
            }
            eventsInPeriod.Sort(delegate(ClockEvent event1, ClockEvent event2)
                {
                    return event1.InOutDateTime.CompareTo(event2.InOutDateTime);
                });
            foreach (ClockEvent clockEvent in eventsInPeriod)
            {
                if (startEvent == null)
                {
                    startEvent = clockEvent;
                }
                else
                {
                    if (startEvent.InOutDateTime.Date == clockEvent.InOutDateTime.Date)
                    {
                        timePairs.Add(new TimePair(startEvent, clockEvent));
                        startEvent = null;
                    }
                    else
                    {
                        timePairs.Add(new TimePair(startEvent, null));
                        startEvent = clockEvent;
                    }
                }
            }
            if (startEvent != null)
                timePairs.Add(new TimePair(startEvent, null));
            overtimeHours = ComputeOvertime(period, timePairs);
        }

        private double ComputeOvertime(PayrollPeriod period, List<TimePair> timePairs)
        {
            double week1Hours = 0.0;
            double week2Hours = 0.0;
            foreach (TimePair pair in timePairs)
            {
                if (!pair.IsOpen)
                {
                    if (pair.StartEvent.InOutDateTime.Subtract(period.StartDate).TotalDays < 7.0)
                        week1Hours += pair.Length.TotalHours;
                    else
                        week2Hours += pair.Length.TotalHours;
                }
            }
            double overtimeHours = 0.0;
            if (week1Hours > 40.0)
                overtimeHours += (week1Hours - 40.0);
            if (week2Hours > 40.0)
                overtimeHours += (week2Hours - 40.0);
            return overtimeHours;
        }

        public void ClockInOut(DateTime when)
        {
            DateTime whenRounded = new DateTime(when.Year, when.Month, when.Day, when.Hour, when.Minute, 0);
            _Events.AddLast(new ClockEvent(whenRounded, DateTime.Now, EventStatus.Overridden));
            _Modified = true;
        }

        public DateTime ClockInOut()
        {
            DateTime when = DateTime.Now;
            DateTime whenRounded = new DateTime(when.Year, when.Month, when.Day, when.Hour, when.Minute, 0);
            _Events.AddLast(new ClockEvent(whenRounded, DateTime.Now, EventStatus.Normal));
            _Modified = true;
            return whenRounded;
        }

        public bool Delete(DateTime when)
        {
            foreach (ClockEvent clockEvent in _Events)
            {
                if (clockEvent.InOutDateTime == when && clockEvent.Status != EventStatus.Deleted)
                {
                    clockEvent.Status = EventStatus.Deleted;
                    _Modified = true;
                    return true;
                }
            }
            return false;
        }

        private string FileName
        {
            get { return PayrollStatic.EmployeesFolder + "\\" + FolderName + "\\Times.dat"; }
        }
    }
}

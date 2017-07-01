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
        Deleted = 2,
        Absent = 3,
        Extra = 4
    }

    public class Times
    {
        private readonly string FolderName;
        private LinkedList<ClockEvent> _Events;
        public const string StdBareName = "Times.dat";
        private readonly string _BareName;

        public static Times Load(string folderName, string bareName)
        {
            Times result = new Times(folderName, bareName);
            result.LoadFromFile();
            return result;
        }

        public static List<string> BareNames(string folderName)
        {
            List<string> bareNames = new List<string>();
            foreach(string fullName in Directory.GetFiles(PayrollStatic.EmployeesFolder + "\\" + folderName))
            {
                string fileName = Path.GetFileName(fullName);
                if (fileName.StartsWith("times", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (fileName.EndsWith(".dat",StringComparison.InvariantCultureIgnoreCase))
                    {
                        bareNames.Add(fileName);
                    }
                }
            }
            return bareNames;
        }

        private Times(string folderName, string bareName)
        {
            FolderName = folderName;
            _BareName = bareName;
            _Events = new LinkedList<ClockEvent>();
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
                    if (clockEvent.Status == EventStatus.Deleted)
                    {
                        // Older versions of this software changed the old ClockEvent in the file
                        // when it was deleted, but new versions simply append a delete record to
                        // the file and let this routine match the delete record with the original
                        // record. If Delete() returns true then it found an original record matching
                        // the date and time and marked it as deleted, which means it was a new style
                        // delete, otherwise the delete record is an old style one and we add it to
                        // the list.
                        if (!Delete(clockEvent))
                            _Events.AddLast(clockEvent);
                    }
                    else
                        _Events.AddLast(clockEvent);
                }
            }
        }

        public void SaveToFile(ClockEvent newEvent)
        {
            using (TextWriter writer = new StreamWriter(FileName, true))
            {
                newEvent.Write(writer);
            }
        }

        public void Get(PayrollPeriod period, out List<TimePair> timePairs, out double overtimeHours, out List<TimePair> absentPairs)
        {
            timePairs = new List<TimePair>();
            absentPairs = new List<TimePair>();
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
                        AddPair(timePairs, absentPairs, startEvent, clockEvent);
                        startEvent = null;
                    }
                    else
                    {
                        // pair cross a date boundary, so leave an open pair in the old date and start a new pair.
                        AddPair(timePairs, absentPairs, startEvent, null);
                        startEvent = clockEvent;
                    }
                }
            }
            if (startEvent != null)
                AddPair(timePairs, absentPairs, startEvent, null);
            overtimeHours = ComputeOvertime(period, timePairs);
        }

        private static void AddPair(List<TimePair> timePairs, List<TimePair> absentPairs, ClockEvent startEvent, ClockEvent endEvent)
        {
            TimePair pair = new TimePair(startEvent, endEvent);
            if (pair.IsAbsent)
            {
                absentPairs.Add(pair);
                return;
            }
            timePairs.Add(pair);
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

        public ClockEvent ClockInOut(DateTime when)
        {
            ClockEvent newEvent = new ClockEvent(ClockEvent.Round(when), DateTime.Now, EventStatus.Overridden);
            _Events.AddLast(newEvent);
            return newEvent;
        }

        public ClockEvent ClockInOut()
        {
            ClockEvent newEvent = new ClockEvent(ClockEvent.Round(DateTime.Now), DateTime.Now, EventStatus.Normal);
            _Events.AddLast(newEvent);
            return newEvent;
        }

        public ClockEvent ClockAbsent(DateTime when)
        {
            ClockEvent newEvent = new ClockEvent(ClockEvent.Round(when), DateTime.Now, EventStatus.Absent);
            _Events.AddLast(newEvent);
            return newEvent;
        }

        public ClockEvent ClockExtra(DateTime when)
        {
            ClockEvent newEvent = new ClockEvent(ClockEvent.Round(when), DateTime.Now, EventStatus.Extra);
            _Events.AddLast(newEvent);
            return newEvent;
        }

        public bool Delete(DateTime when, out ClockEvent newEvent)
        {
            newEvent = new ClockEvent(ClockEvent.Round(when), DateTime.Now, EventStatus.Deleted);
            return Delete(newEvent);
        }

        private bool Delete(ClockEvent newEvent)
        {
            foreach (ClockEvent clockEvent in _Events)
            {
                if (clockEvent.InOutDateTime == newEvent.InOutDateTime && clockEvent.Status != EventStatus.Deleted)
                {
                    clockEvent.Status = EventStatus.Deleted;
                    return true;
                }
            }
            return false;
        }

        private string FileName
        {
            get { return PayrollStatic.EmployeesFolder + "\\" + FolderName + "\\" + _BareName; }
        }
    }
}

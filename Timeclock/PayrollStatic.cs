using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PayrollTimeclock
{
    public static class PayrollStatic
    {
        public static string DataFolder;
        public static Settings Settings = null;
        public static IEnumerable<Person> People = null;
        public static IDictionary<string, Person> PeopleByName = null;
        public static IDictionary<string, Person> PeopleByAddress = null;

        public static string PayrollConfigFile
        {
            get { return DataFolder + "\\PayrollConfig.xml"; }
        }

        public static string EmployeesFolder
        {
            get { return DataFolder + "\\Employees"; }
        }

        public static string PeriodExportFile
        {
            get { return DataFolder + "\\PeriodExport.txt"; }
        }

        public static void LoadPeople()
        {
            List<Person> people = new List<Person>();
            Dictionary<string, Person> peopleByAddress = new Dictionary<string, Person>();
            Dictionary<string, Person> peopleByName = new Dictionary<string, Person>();
            string employeesFolder = PayrollStatic.EmployeesFolder;
            DirectoryInfo employeesDir = new DirectoryInfo(employeesFolder);
            foreach (DirectoryInfo empDir in employeesDir.GetDirectories())
            {
                if (!empDir.Name.StartsWith(".") &&
                    (((empDir.Attributes & FileAttributes.Hidden) == 0) || ShowHiddenEmployees))
                {
                    Person person = Person.Load(empDir.Name);
                    people.Add(person);
                    if (person.FullName.Exists)
                        peopleByName[person.FullName.GetValue] = person;
                    if (person.EmailAddress.Exists)
                        peopleByAddress[person.EmailAddress.GetValue] = person;
                }
            }
            People = people;
            PeopleByAddress = peopleByAddress;
            PeopleByName = peopleByName;
        }

        public static bool ShowHiddenEmployees { get; set; }
    }
}

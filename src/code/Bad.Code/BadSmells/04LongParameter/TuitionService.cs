using System;
using System.Collections.Generic;
using System.Text;

namespace Bad.Code.BadSmells._04LongParameter
{
    internal class Student
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    internal class TuitionService
    {
        public void Calculate(string studentFirstName,
                            string studentLastName,
                            string studentId,
                            List<StudentClass> classes,
                            decimal labFees,
                            Quarter quarter,
                            string isResident,
                            bool isLegacy)
        {

        }

        public void PrintSchedule(string studentFirstName,
                                    string studentLastName,
                                    string studentId,
                                    Quarter quarter,
                                    List<StudentClass> classes,
                                    List<Notice> notices)
        {

        }
    }

    internal enum Quarter
    {
    }

    internal class Notice
    {
    }

    internal class StudentClass { }
}

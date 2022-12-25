using System;

namespace Bad.Code.BadSmells._09DataClumps
{
    public class SickLeave
    {
        public string PersonnelId { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

    }
}
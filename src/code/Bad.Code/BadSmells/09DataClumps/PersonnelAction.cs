using System;

namespace Bad.Code.BadSmells._09DataClumps
{

    public abstract class PersonnelAction
    {
        public DateTime RecruitmentDate { get; private set; }

        public DateTime CreateDate { get; private set; }

        public DateTime ApplyDate { get; private set; }
    }
}
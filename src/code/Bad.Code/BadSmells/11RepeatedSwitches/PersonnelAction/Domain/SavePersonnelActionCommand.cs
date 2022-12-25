using System;

namespace Bad.Code.BadSmells._11RepeatedSwitches.PersonnelAction.Domain;

public class SavePersonnelActionCommand 
{
    public PersonnelActionCommandDto PersonnelActions { set; get; }

    public SavePersonnelActionCommand(PersonnelActionCommandDto personnelAction)
    {
        this.PersonnelActions = personnelAction;
    }

    public class PersonnelActionCommandDto
    {
        public short PersonnelActionType { get; set; }
    }
}
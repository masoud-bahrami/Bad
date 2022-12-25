namespace Bad.Code.BadSmells._11RepeatedSwitches.PersonnelAction.Domain;

public abstract class UpdatePersonnelActionCommand
{
    public short PersonnelAction { get; set; }
    public string Id { get; set; }
}
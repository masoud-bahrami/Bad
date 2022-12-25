using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bad.Code.BadSmells._11RepeatedSwitches.PersonnelAction.Domain;

namespace Bad.Code.BadSmells._11RepeatedSwitches.PersonnelAction.Handlers;

public class UpdatePersonnelActionCommandHandler
{
    public async Task Handle(UpdatePersonnelActionCommand command)
    {
        var personnelAction
            = ReconstitutePersonnelAction((PersonnelActionType)command.PersonnelAction,
                command.Id);
        // TODO
    }

    private static Domain.PersonnelAction ReconstitutePersonnelAction(PersonnelActionType personnelActionType, string id)
    {
        Domain.PersonnelAction personnelAction = null;

        switch (personnelActionType)
        {
            case PersonnelActionType.Recruitment:
                var recruitmentPersonnelActionDomainEvents = LoadDomainEventsOfRecruitmentPersonnelAction(id);
                personnelAction = new RecruitmentPersonnelAction(recruitmentPersonnelActionDomainEvents);
                break;
            case PersonnelActionType.Adjustment:
                var adjustmentPersonnelActionDomainEvents = LoadDomainEventsOfAdjustmentPersonnelAction(id);

                personnelAction = new AdjustmentPersonnelAction(adjustmentPersonnelActionDomainEvents);
                break;
            case PersonnelActionType.NA:
            case PersonnelActionType.ServiceEnd:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return personnelAction;
    }

    private static List<object> LoadDomainEventsOfRecruitmentPersonnelAction(string id)
    {
        throw new NotImplementedException();
    }
    private static List<object> LoadDomainEventsOfAdjustmentPersonnelAction(string id)
    {
        throw new NotImplementedException();
    }
}
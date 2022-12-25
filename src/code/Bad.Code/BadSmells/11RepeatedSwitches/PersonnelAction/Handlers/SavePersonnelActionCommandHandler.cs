using System;
using System.Threading.Tasks;
using Bad.Code.BadSmells._11RepeatedSwitches.PersonnelAction.Domain;

namespace Bad.Code.BadSmells._11RepeatedSwitches.PersonnelAction.Handlers
{
    public class SavePersonnelActionCommandHandler
    {
        public async Task Handle(SavePersonnelActionCommand command)
        {
            var personnelAction = this.CreatePersonnelAction((PersonnelActionType)command.PersonnelActions.PersonnelActionType);

            // TODO
        }

        private Domain.PersonnelAction CreatePersonnelAction(PersonnelActionType personnelActionType)
        {
            Domain.PersonnelAction personnelAction = null;

            switch (personnelActionType)
            {
                case PersonnelActionType.Recruitment:
                    personnelAction = new RecruitmentPersonnelAction();
                    break;
                case PersonnelActionType.Adjustment:
                    personnelAction = new AdjustmentPersonnelAction();
                    break;
                case PersonnelActionType.NA:
                case PersonnelActionType.ServiceEnd:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return personnelAction;
        }
    }
}
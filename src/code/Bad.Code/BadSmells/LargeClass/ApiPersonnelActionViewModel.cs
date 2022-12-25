using System;
using System.Collections.Generic;

namespace Bad.Code.BadSmells.LargeClass
{
    public class ApiPersonnelActionViewModel
    {
        public string Id { get; set; }

        public string PersonnelActionNo { get; set; }

        public short PersonnelActionType { get; set; }

        public DateTime RecruitmentDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ApplyDate { get; set; }

        public string RecruitmentTypeId { get; set; }

        public string RecruitmentTypeTitle { get; set; }

        public string PersonnelId { get; set; }

        public string PersonnelCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NationalCode { get; set; }

        public string OrganizationalUnitId { get; set; }

        public string OrganizationalUnitTitle { get; set; }

        public string OrganizationalPositionId { get; set; }

        public string OrganizationalPositionTitle { get; set; }

        public string JobId { get; set; }

        public string JobTitle { get; set; }

        public string WorkPlaceId { get; set; }

        public string WorkPlaceTitle { get; set; }

        public string CostCenterId { get; set; }

        public string CostCenterTitle { get; set; }

        public string ProjectId { get; set; }

        public string ProjectTitle { get; set; }

        public short ContractType { get; set; }

        public List<ApiPersonnelActionDetailViewModel> PersonnelActionDetails { get; set; }
    }

    public class ApiPersonnelActionDetailViewModel
    {
        public string ActionItemId { get; set; }

        public string ActionItemTitle { get; set; }

        public long BaseValue { get; set; }

        public DateTime? ApplyDate { get; set; }
    }
}
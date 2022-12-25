using System;

namespace Bad.Code.BadSmells.DataClass;

public class LoanDesignation 
{
    public string Id { get; set; }

    public string PersonnelId { get; private set; }

    public string LoanTypeId { get; private set; }

    public DateTime PaymentDate { get; private set; }

    public DateTime InstallmentStartDate { get; private set; }

    public long TotalAmount { get; private set; }

    public long PaybackAmount { get; private set; }

    public long InstallmentAmount { get; private set; }

    public int InstallmentCount { get; private set; }

    public long ExcessAmount { get; private set; }

    public ExcessDeductionType ExcessDeductionType { get; private set; }

    public long RemainAmount { get; private set; }
        
    public void Update(UpdateLoanDesignationCommand command)
    {
        //TODO
    }
        
    #region Private Methods

    private void AssertInstallmentStartDateMustBeGreaterThanOrEqualToTheSystemDate()
    {
            
    }

    private void AssertPaybackAmountMustBeLessThanOrEqualToTheTotalAmount()
    {
          
    }
       

    private void AssertHasAnyCalculation()
    {
           
    }
    #endregion
}

public class UpdateLoanDesignationCommand
{
}

public enum ExcessDeductionType
{
    NA = 0,
    OnFirstInstallment = 1,
    OnLastInstallment = 2,
    WithFirstInstallment = 3,
    WithLastInstallment = 4,
}
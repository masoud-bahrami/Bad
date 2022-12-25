using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bad.Code.BadSmells._16MessageChains;

public class LoanCalculator
{

    public async void CalculateLoan(SaveLoanSettlementCommand command)
    {
        var loanSettlement = command.LoanSettlement;
        var personnelLoans = await GetByPersonnelIdYearMonth(loanSettlement.PersonnelId, loanSettlement.Year, loanSettlement.Month);
        var currentPersonnelLoan = personnelLoans.Loans.FirstOrDefault(x => x.Id == loanSettlement.LoanDesignationId);
        
        int installmentRemainCount = currentPersonnelLoan.InstallmentCount;


        //int installmentRemainCount1 =  (await GetByPersonnelIdYearMonth(loanSettlement.PersonnelId, loanSettlement.Year, loanSettlement.Month))
        //    .Loans.FirstOrDefault(x => x.Id == loanSettlement.LoanDesignationId).InstallmentCount;


        //Loan first = null;
        //foreach (var x in (await GetByPersonnelIdYearMonth(loanSettlement.PersonnelId, loanSettlement.Year, loanSettlement.Month)).Loans)
        //{
        //    if (x.Id == loanSettlement.LoanDesignationId)
        //    {
        //        first = x;
        //        break;
        //    }
        //}

        //int installmentRemainCount1 = first.InstallmentCount;


        //TODO 
    }

    private async Task<PersonnelLoanLoan> GetByPersonnelIdYearMonth(string loanSettlementPersonnelId, short loanSettlementYear, short loanSettlementMonth)
    {
        throw new NotImplementedException();
    }
}

internal class PersonnelLoanLoan
{
    public List<Loan> Loans { get; set; }
}

internal class Loan
{
    public string Id { get; set; }
    public int InstallmentCount { get; set; }
}

public class SaveLoanSettlementCommand
{
    public LoanSettlment LoanSettlement { get; set; }
}

public class LoanSettlment
{
    public string PersonnelId { get; set; }
    public short Year { get; set; }
    public short Month { get; set; }
    public string LoanDesignationId { get; set; }
}
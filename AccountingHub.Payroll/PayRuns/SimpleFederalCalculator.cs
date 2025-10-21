namespace AccountingHub.Payroll.PayRuns;
public class SimpleFederalCalculator : IPayrollCalculator {
    // NOTE: Placeholder logic for demo purposes. Replace with real IRS tables.
    public PayrollResult Calculate(PayContext ctx, Employees.Employee emp, decimal hoursWorked = 0m, decimal bonuses = 0m) {
        decimal gross = emp.PayType == Employees.PayType.Hourly ? emp.Rate * hoursWorked + bonuses : emp.Rate + bonuses;
        decimal pretax = 0m;
        decimal fica = decimal.Round(gross * 0.062m, 2);
        decimal medicare = decimal.Round(gross * 0.0145m, 2);
        decimal fit = decimal.Round(gross * 0.10m, 2); // dummy 10%
        decimal taxes = fica + medicare + fit;
        decimal posttax = 0m;
        decimal net = gross - pretax - taxes - posttax;
        return new PayrollResult(gross, pretax, taxes, posttax, net);
    }
}

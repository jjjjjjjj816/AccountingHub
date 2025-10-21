namespace AccountingHub.Payroll.PayRuns;
public record PayContext(DateOnly PayDate, string State);
public record PayrollResult(decimal Gross, decimal PreTax, decimal Taxes, decimal PostTax, decimal Net);
public interface IPayrollCalculator {
    PayrollResult Calculate(PayContext ctx, Employees.Employee emp, decimal hoursWorked = 0m, decimal bonuses = 0m);
}

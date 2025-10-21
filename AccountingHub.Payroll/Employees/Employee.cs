namespace AccountingHub.Payroll.Employees;
public enum PayType { Hourly, Salary }
public class Employee {
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string SSNEncrypted { get; set; } = ""; // store encrypted only
    public string Address { get; set; } = "";
    public PayType PayType { get; set; }
    public decimal Rate { get; set; } // hourly or salary per period
    public string FilingStatus { get; set; } = "Single";
    public int Allowances { get; set; } = 0;
}

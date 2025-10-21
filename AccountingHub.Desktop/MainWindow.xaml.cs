using System.Windows;
using System.Windows.Controls;
using AccountingHub.Infrastructure;
using AccountingHub.Payroll.Employees;
using AccountingHub.Payroll.PayRuns;

namespace AccountingHub.Desktop;
public partial class MainWindow : Window {
    private readonly AppDbContext _db = new();
    private readonly IPayrollCalculator _calculator = new SimpleFederalCalculator();
    public MainWindow() {
        InitializeComponent();
        _db.Database.EnsureCreated();
        ShowDashboard();
    }
    private void ShowDashboard() {
        var panel = new StackPanel();
        panel.Children.Add(new TextBlock { Text = "Welcome to AccountingHub", FontSize = 18, FontWeight = FontWeights.Bold });
        panel.Children.Add(new TextBlock { Text = "Quick actions:", Margin = new Thickness(0,8,0,4) });
        var btn = new Button { Content = "Add Demo Employee" };
        btn.Click += (s,e) => {
            _db.Employees.Add(new Employee { FullName = "Jane Demo", Rate = 30m, PayType = PayType.Hourly, SSNEncrypted = "ENCRYPTED", Address="123 Main" });
            _db.SaveChanges();
            MessageBox.Show("Demo employee added.");
        };
        panel.Children.Add(btn);
        ContentHost.Content = panel;
    }
    private void BtnDashboard_Click(object sender, RoutedEventArgs e) => ShowDashboard();
    private void BtnEmployees_Click(object sender, RoutedEventArgs e) {
        var list = new ListBox();
        list.ItemsSource = _db.Employees.ToList();
        list.DisplayMemberPath = "FullName";
        ContentHost.Content = new StackPanel {
            Children = {
                new TextBlock{Text="Employees", FontSize=16, FontWeight=FontWeights.Bold},
                list
            }
        };
    }
    private void BtnRunPayroll_Click(object sender, RoutedEventArgs e) {
        var emp = _db.Employees.FirstOrDefault();
        if (emp is null) { MessageBox.Show("Add an employee first."); return; }
        var result = _calculator.Calculate(new PayContext(DateOnly.FromDateTime(DateTime.Today), "CA"), emp, hoursWorked:80m);
        ContentHost.Content = new StackPanel {
            Children = {
                new TextBlock{Text=$"Payroll Preview for {emp.FullName}", FontSize=16, FontWeight=FontWeights.Bold},
                new TextBlock{Text=$"Gross: {result.Gross:C}"},
                new TextBlock{Text=$"Taxes: {result.Taxes:C}"},
                new TextBlock{Text=$"Net: {result.Net:C}"}
            }
        };
    }
    private void BtnJournal_Click(object sender, RoutedEventArgs e) {
        var entries = _db.JournalEntries
            .Select(j=> new { j.Id, j.Date, j.Memo, Lines = j.Splits.Count })
            .ToList();
        var grid = new DataGrid{ ItemsSource = entries, AutoGenerateColumns = true };
        ContentHost.Content = new StackPanel {
            Children = {
                new TextBlock{Text="Journal Entries", FontSize=16, FontWeight=FontWeights.Bold},
                grid
            }
        };
    }
}

using Microsoft.EntityFrameworkCore;
using AccountingHub.Domain.Accounting;
using AccountingHub.Payroll.Employees;

namespace AccountingHub.Infrastructure;
public class AppDbContext : DbContext {
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
    public DbSet<Split> Splits => Set<Split>();
    public DbSet<Employee> Employees => Set<Employee>();

    public string DbPath { get; }
    public AppDbContext() {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DbPath = System.IO.Path.Join(folder, "AccountingHub", "app.db");
        Directory.CreateDirectory(Path.GetDirectoryName(DbPath)!);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={DbPath}");
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Account>().HasKey(a => a.Id);
        modelBuilder.Entity<JournalEntry>().HasKey(j => j.Id);
        modelBuilder.Entity<Split>().HasKey(s => s.Id);
        modelBuilder.Entity<Split>()
            .HasOne(s => s.JournalEntry)
            .WithMany(j => j.Splits)
            .HasForeignKey(s => s.JournalEntryId);
        modelBuilder.Entity<Split>()
            .HasOne(s => s.Account)
            .WithMany()
            .HasForeignKey(s => s.AccountId);

        // Seed a minimal chart of accounts
        modelBuilder.Entity<Account>().HasData(
            new Account { Id = 1, Code = "1000", Name = "Cash", Type = AccountType.Asset },
            new Account { Id = 2000, Code = "2000", Name = "Payroll Tax Payable", Type = AccountType.Liability },
            new Account { Id = 3000, Code = "3000", Name = "Equity", Type = AccountType.Equity },
            new Account { Id = 4000, Code = "4000", Name = "Revenue", Type = AccountType.Income },
            new Account { Id = 5000, Code = "5000", Name = "Wage Expense", Type = AccountType.Expense }
        );
    }
}

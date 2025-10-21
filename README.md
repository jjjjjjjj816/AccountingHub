# AccountingHub Starter (Windows, .NET 8, WPF)

A minimal scaffold for a **Windows desktop** accounting + payroll app.

## Projects
- `AccountingHub.Domain` – core accounting entities (accounts, journal, splits)
- `AccountingHub.Payroll` – employees + a demo payroll calculator
- `AccountingHub.Infrastructure` – EF Core + SQLite database
- `AccountingHub.Desktop` – WPF UI shell with simple screens

## Build
1. Install **.NET 8 SDK** and **Visual Studio 2022** (Desktop dev with .NET).
2. Open `AccountingHub.sln`
3. Build & run `AccountingHub.Desktop`

> The payroll calculator is a DEMO (flat 10% FIT + FICA/Medicare) – replace with real tax tables.

## Next Steps
- Add proper payroll tax tables (federal/state) and W‑2/941 form generators.
- Add AR/AP (Invoices, Bills) and GL postings.
- Add reports (P&L, Balance Sheet, Payroll Summary) using RDLC or QuestPDF.
- Create a WiX installer project and GitHub Actions to publish `.msi` artifacts.

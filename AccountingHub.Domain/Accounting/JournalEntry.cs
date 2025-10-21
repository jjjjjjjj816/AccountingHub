namespace AccountingHub.Domain.Accounting;
public class JournalEntry {
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string? Memo { get; set; }
    public ICollection<Split> Splits { get; set; } = new List<Split>();
}
public class Split {
    public int Id { get; set; }
    public int JournalEntryId { get; set; }
    public JournalEntry JournalEntry { get; set; } = default!;
    public int AccountId { get; set; }
    public Account Account { get; set; } = default!;
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
}

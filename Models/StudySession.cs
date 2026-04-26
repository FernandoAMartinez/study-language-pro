namespace study_language_pro.Models;

public class StudySession
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public int TotalCards { get; set; }
    public int KnownCount { get; set; }
    public int ReviewCount { get; set; }
    public List<string> ReviewedWordIds { get; set; } = new();
}

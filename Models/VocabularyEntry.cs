namespace study_language_pro.Models;

public class VocabularyEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Kanji { get; set; } = string.Empty;       // e.g. 私 (optional)
    public string Kana { get; set; } = string.Empty;        // e.g. わたし (optional)
    public string Romaji { get; set; } = string.Empty;      // e.g. watashi (required)
    public string Significado { get; set; } = string.Empty; // e.g. yo, yo mismo (required)
    public string Ejemplo { get; set; } = string.Empty;     // e.g. 私は学生です (optional)
    public string TipoPalabra { get; set; } = string.Empty;  // e.g. sustantivo, verbo, adjetivo
    public List<string> Tags { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

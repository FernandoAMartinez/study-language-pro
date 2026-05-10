namespace study_language_pro.Models;

public class KanjiWord
{
    public string Kanji { get; set; } = string.Empty;
    public string Lectura { get; set; } = string.Empty;
    public string SignificadoEs { get; set; } = string.Empty;
}

public class KanjiOracion
{
    public string Japones { get; set; } = string.Empty;
    public string SignificadoEs { get; set; } = string.Empty;
}

public class KanjiEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Kanji { get; set; } = string.Empty;
    public string SignificadoEs { get; set; } = string.Empty;
    public List<string> TraduccionesEs { get; set; } = [];
    public string? Kunyomi { get; set; }
    public string? Onyomi { get; set; }
    public List<KanjiWord> EjemplosPalabras { get; set; } = [];
    public List<KanjiOracion> Oraciones { get; set; } = [];
    public DateTime ImportedAt { get; set; } = DateTime.UtcNow;
}

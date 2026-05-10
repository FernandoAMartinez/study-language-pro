using System.Text.Json.Serialization;

namespace study_language_pro.Models;

public class KanjiWordDto
{
    [JsonPropertyName("kanji")]
    public string Kanji { get; set; } = string.Empty;

    [JsonPropertyName("lectura")]
    public string Lectura { get; set; } = string.Empty;

    [JsonPropertyName("significado_es")]
    public string SignificadoEs { get; set; } = string.Empty;
}

public class KanjiOracionDto
{
    [JsonPropertyName("japones")]
    public string Japones { get; set; } = string.Empty;

    [JsonPropertyName("significado_es")]
    public string SignificadoEs { get; set; } = string.Empty;
}

public class KanjiImportDto
{
    [JsonPropertyName("kanji")]
    public string Kanji { get; set; } = string.Empty;

    [JsonPropertyName("significado_es")]
    public string SignificadoEs { get; set; } = string.Empty;

    [JsonPropertyName("traducciones_es")]
    public List<string> TraduccionesEs { get; set; } = [];

    [JsonPropertyName("kunyomi")]
    public string? Kunyomi { get; set; }

    [JsonPropertyName("onyomi")]
    public string? Onyomi { get; set; }

    [JsonPropertyName("ejemplos_palabras")]
    public List<KanjiWordDto> EjemplosPalabras { get; set; } = [];

    [JsonPropertyName("oraciones")]
    public List<KanjiOracionDto> Oraciones { get; set; } = [];

    public KanjiEntry ToKanjiEntry() => new()
    {
        Id              = Guid.NewGuid(),
        Kanji           = Kanji,
        SignificadoEs   = SignificadoEs,
        TraduccionesEs  = TraduccionesEs,
        Kunyomi         = Kunyomi,
        Onyomi          = Onyomi,
        EjemplosPalabras = EjemplosPalabras.Select(w => new KanjiWord
        {
            Kanji        = w.Kanji,
            Lectura      = w.Lectura,
            SignificadoEs = w.SignificadoEs
        }).ToList(),
        Oraciones = Oraciones.Select(o => new KanjiOracion
        {
            Japones      = o.Japones,
            SignificadoEs = o.SignificadoEs
        }).ToList(),
        ImportedAt = DateTime.UtcNow
    };
}

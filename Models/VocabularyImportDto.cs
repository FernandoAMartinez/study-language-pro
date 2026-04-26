using System.Text.Json.Serialization;

namespace study_language_pro.Models;

/// <summary>
/// DTO que representa el formato JSON usado para la importación masiva de vocabulario.
/// </summary>
public class VocabularyImportDto
{
    [JsonPropertyName("Tipo")]
    public string Tipo { get; set; } = string.Empty;

    [JsonPropertyName("Kanji")]
    public string Kanji { get; set; } = string.Empty;

    [JsonPropertyName("Kana")]
    public string Kana { get; set; } = string.Empty;

    [JsonPropertyName("Romaji")]
    public string Romaji { get; set; } = string.Empty;

    [JsonPropertyName("Español")]
    public string Español { get; set; } = string.Empty;

    [JsonPropertyName("Ejemplo")]
    public string Ejemplo { get; set; } = string.Empty;

    [JsonPropertyName("Tags")]
    public List<string> Tags { get; set; } = new();

    public VocabularyEntry ToVocabularyEntry() => new()
    {
        TipoPalabra = Tipo,
        Kanji       = Kanji,
        Kana        = Kana,
        Romaji      = Romaji,
        Significado = Español,
        Ejemplo     = Ejemplo,
        Tags        = Tags,
    };
}

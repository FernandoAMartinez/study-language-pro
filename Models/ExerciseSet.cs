namespace study_language_pro.Models;

public record Blank(int Position, string Answer, string Hint);

public record ExerciseSet(
    string Id,
    string Type,
    string Level,
    string Sentence,
    string Display,
    Blank[] Blanks,
    string GrammarPoint,
    string English,
    string Explanation
);

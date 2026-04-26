# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
dotnet run          # start dev server (hot reload enabled)
dotnet build        # compile
dotnet publish      # publish for deployment
```

The dev server starts on `https://localhost:7XXX` (port printed at startup). There are no tests in this project yet.

## What this app is

A Japanese vocabulary study tool for Spanish speakers, built as a Blazor WebAssembly PWA. All data lives in the browser via **Blazored.LocalStorage** — there is no backend or API.

## Architecture

**Three pages** (each injects `ILocalStorageService Storage` directly):

| Route | File | Purpose |
|-------|------|---------|
| `/` | [Pages/Vocabulary.razor](Pages/Vocabulary.razor) | CRUD for vocabulary entries; JSON bulk import |
| `/flashcards` | [Pages/Flashcards.razor](Pages/Flashcards.razor) | Tag-filtered flashcard study sessions |
| `/stats` | [Pages/Stats.razor](Pages/Stats.razor) | KPIs, activity heatmap (SVG), progress chart (SVG), session history |

**Three reusable components** ([Components/](Components/)):
- `VocabularyList` — renders the entry table with edit/delete callbacks
- `VocabularyForm` — add/edit form, emits `OnSave`/`OnCancel`
- `FlashcardSession` — the flashcard engine; accepts `IReadOnlyList<VocabularyEntry> Cards` and owns all session state

**LocalStorage keys** (shared across pages — must stay in sync):
- `"vocabulary_entries"` → `List<VocabularyEntry>`
- `"study_sessions"` → `List<StudySession>`

## JS interop

[wwwroot/js/swipe-interop.js](wwwroot/js/swipe-interop.js) exposes `window.swipeInterop.attachSwipe(dotNetRef, elementId)` / `detachSwipe(elementId)`. `FlashcardSession` calls these via `IJSRuntime` to wire touch, mouse-drag, and arrow-key swipe on the card element. The component also exposes `[JSInvokable] OnSwipe(string direction)` as the callback target.

## Data model

`VocabularyEntry` fields: `Kanji` (optional), `Kana` (optional), `Romaji` (required), `Significado` (required, Spanish meaning), `Ejemplo`, `TipoPalabra`, `Tags`.

Supported `TipoPalabra` values (drive flashcard back-card color and badge): `sustantivo`, `adjetivo`, `adverbio`, `verbo`, `expresión`, `partícula`.

**Import JSON format** (used by [Models/VocabularyImportDto.cs](Models/VocabularyImportDto.cs)):
```json
[{ "Tipo": "sustantivo", "Kanji": "私", "Kana": "わたし", "Romaji": "watashi", "Español": "yo", "Ejemplo": "私は学生です", "Tags": ["N5"] }]
```
Duplicate detection uses a `"romaji|kanji"` (lowercased) composite key.

## Styling

Tailwind CSS utility classes throughout — no custom CSS framework config file. The only handwritten CSS is the 3D flip animation in `FlashcardSession.razor` (`<style>` block at the top of that file).

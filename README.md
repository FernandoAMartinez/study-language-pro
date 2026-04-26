# 日本語 Study

A Japanese vocabulary study app for Spanish speakers, built as a **Blazor WebAssembly PWA**. Runs entirely in the browser — no backend, no account required. All data lives in the device's local storage.

## Features

- **Vocabulary manager** — add, edit, delete, and bulk-import entries via JSON
- **Flashcard sessions** — swipe or tap through cards with a 3D flip reveal; touch, mouse-drag, and keyboard supported
- **Progress stats** — activity heatmap, per-session performance chart, streak tracker, and top-5 words to review
- **Tag filtering** — organise words by JLPT level or any custom tag; filter both the list and flashcard sessions
- **Install & offline** — full PWA: install to home screen, works without internet after first load
- **Dark mode** — automatic (`prefers-color-scheme`) with a manual toggle; preference persisted across sessions
- **Mobile-first** — designed for phone use with large tap targets and a bottom navigation bar; also works on desktop

## Tech stack

| Layer | Choice |
|---|---|
| Framework | [Blazor WebAssembly](https://learn.microsoft.com/aspnet/core/blazor/) (.NET 10) |
| Styling | [Tailwind CSS v4](https://tailwindcss.com/) |
| Persistence | [Blazored.LocalStorage](https://github.com/Blazored/LocalStorage) |
| Fonts | [M PLUS Rounded 1c](https://fonts.google.com/specimen/M+PLUS+Rounded+1c) · [Noto Serif JP](https://fonts.google.com/noto/specimen/Noto+Serif+JP) |
| PWA | Blazor built-in service worker swap + custom cache-first strategy |
| CI/CD | GitHub Actions → GitHub Pages |

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/) (for Tailwind CSS)

### Run locally

```bash
# Install Tailwind and watch for CSS changes
npm install
npm run watch:css

# In a second terminal, start the dev server
dotnet run
```

The app starts on `https://localhost:7XXX` (port printed at startup).

### One-shot CSS build

```bash
npm run build:css   # minified output → wwwroot/css/app.css
```

## Deployment

The project deploys automatically to **GitHub Pages** on every push to `main` or `master` via the included [GitHub Actions workflow](.github/workflows/deploy.yml).

### First-time setup

1. Push the repo to GitHub
2. Go to **Settings → Pages → Source** and select **GitHub Actions**
3. Push to `main` — the workflow builds and deploys in ~2 minutes

The live URL will be `https://<your-username>.github.io/study-language-pro/`.

> HTTPS is required for the PWA install prompt and service worker. GitHub Pages provides this automatically.

## Vocabulary JSON import format

You can bulk-import words using a JSON file. Each entry follows this shape:

```json
[
  {
    "Tipo": "sustantivo",
    "Kanji": "私",
    "Kana": "わたし",
    "Romaji": "watashi",
    "Español": "yo",
    "Ejemplo": "私は学生です",
    "Tags": ["N5", "pronombres"]
  }
]
```

| Field | Required | Notes |
|---|---|---|
| `Romaji` | yes | Used as part of duplicate detection key |
| `Español` | yes | Spanish meaning shown on the flashcard back |
| `Kanji` | no | Displayed large on the flashcard front |
| `Kana` | no | Shown below kanji |
| `Ejemplo` | no | Example sentence |
| `Tipo` | no | `sustantivo` · `verbo` · `adjetivo` · `adverbio` · `partícula` · `expresión` |
| `Tags` | no | Array of strings; used for filtering |

Duplicate detection uses a case-insensitive `romaji|kanji` composite key. Duplicates within the same import file are also skipped.

## Project structure

```
├── Components/
│   ├── FlashcardSession.razor   # card engine with swipe & 3D flip
│   ├── VocabularyForm.razor     # add / edit form
│   └── VocabularyList.razor     # list & grid views with tag filter
├── Layout/
│   ├── MainLayout.razor         # sidebar + bottom nav + PWA install banner
│   └── NavMenu.razor            # desktop sidebar links
├── Models/                      # VocabularyEntry, StudySession, ImportDto
├── Pages/
│   ├── Vocabulary.razor         # / — CRUD + JSON import
│   ├── Flashcards.razor         # /flashcards
│   └── Stats.razor              # /stats
├── Styles/
│   └── app.css                  # Tailwind v4 source (compiled → wwwroot/css/app.css)
└── wwwroot/
    ├── js/swipe-interop.js      # touch + mouse-drag + arrow-key swipe
    ├── manifest.json            # PWA manifest
    ├── service-worker.js        # dev: passthrough
    └── service-worker.published.js  # prod: cache-first offline strategy
```

## Local storage keys

| Key | Value type | Used by |
|---|---|---|
| `vocabulary_entries` | `VocabularyEntry[]` | Vocabulary, Flashcards |
| `study_sessions` | `StudySession[]` | Flashcards, Stats |
| `theme` | `"light"` \| `"dark"` | MainLayout |

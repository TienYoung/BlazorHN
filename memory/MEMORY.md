# BlazorHN Memory

## Project
Blazor WASM PWA (.NET 10) displaying Hacker News. Design branch: `design`.

## Architecture
- `Components/StoryList.razor` — Tab Bar (Top/New/Ask/Show/Jobs) + infinite scroll story cards. Used by MainLayout (iPad) and Home.razor (mobile).
- `Components/StoryItem.razor` — Story card: serif title, Fira Code domain+meta, no index number. Calls AppState.SelectStory(id).
- `Components/Comment.razor` — Card with avatar (initials, hashed color), like button, reply pill. Modes: List / Focus / Compressed (via nested public enum `Comment.CommentDisplayMode`).
- `Pages/Home.razor` — On wide: shows "select a story" prompt. On narrow: renders `<StoryList />`.
- `Pages/ItemComments.razor` — TopBar (fixed, blur, toggle expand/collapse) + List view (top-level comments) + Focus view (3-column: parent|focus|children). History API via `hnNav` JS.
- `Layout/MainLayout.razor` — Detects wide screen via JS `hnLayout.isWide()`. Wide: sidebar (StoryList) + main panel. Narrow: just @Body.
- `Services/AppStateService.cs` — Singleton. Tracks SelectedStoryId + IsWide. Registered in Program.cs.

## Design Tokens (app.css)
- `--orange: #FF6600`, `--orange-dim: rgba(255,102,0,0.12)`
- `--bg: #f2f2ef`, `--surface: rgba(255,255,255,0.75)`, `--surface-focus: #ffffff`
- `--font-serif: Instrument Serif`, `--font-sans: Plus Jakarta Sans`, `--font-mono: Fira Code`
- `--card-radius: 12px`, `--anim-spring: cubic-bezier(0.34, 1.56, 0.64, 1)`

## JS Globals (index.html)
- `infiniteScroll.init/dispose` — IntersectionObserver for story list
- `hnNav.push(id)/onPop(ref)/dispose` — History API for comment focus navigation
- `hnLayout.isWide()/onResize(ref)/dispose` — Media query 768px breakpoint

## Key Patterns
- Scoped CSS files alongside each .razor file
- `::deep` for piercing scoped CSS (comment body links/pre)
- Comment focus view: `grid-template-columns` set inline from C# property `FocusGridTemplate`
- Avatar colors: 8 hashed colors from author name char sum mod 8

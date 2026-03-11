# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Run the dev server (hot reload)
dotnet watch --project BlazorHN.csproj

# Build
dotnet build BlazorHN.csproj

# Publish
dotnet publish BlazorHN.csproj
```

There are no automated tests in this project.

## Architecture

BlazorHN is a Blazor WebAssembly app targeting .NET 10 that displays Hacker News top stories.

**Pages:**
- `Home.razor` (`/`) — Fetches all top story IDs, then loads stories in pages of 15 via `Task.WhenAll`. Infinite scroll is driven by a JS `IntersectionObserver` (defined inline in `index.html`) that calls `[JSInvokable] LoadMore()` on the component.
- `ItemComments.razor` (`/item/{Id:int}`) — Fetches the story item, then recursively loads the full comment tree breadth-first via `Task.WhenAll`. Passes a flat `Dictionary<int, HackerNewsItem>` to the `Comment` component, which resolves its own children from it.

**Components:**
- `StoryItem.razor` — Renders a clickable `FluentCard`; the card navigates to `/item/{Id}` while the domain link uses `@onclick:stopPropagation` to open the URL without navigating.
- `Comment.razor` — Recursively renders itself for nested replies using the shared `AllItems` dictionary; skips deleted/dead items. Renders `Item.Text` as `MarkupString` (HN returns HTML).

**Services:**
- `HackerNewsService` wraps `HttpClient` with base address `https://hacker-news.firebaseio.com/v0/`. Registered as scoped with an explicit `HttpClient` (not the default WASM one) in `Program.cs`.
- `Models/HackerNewsItem.cs` maps the HN Firebase REST API. `PostedAt` and `HasUrl` are computed `[JsonIgnore]` properties.

**UI:** Uses [Microsoft Fluent UI Blazor Components](https://www.fluentui-blazor.net/) (`Microsoft.FluentUI.AspNetCore.Components` v4). FluentUI web components are also used directly in `index.html` (e.g., `<fluent-progress-ring>`).

**PWA:** Service worker (`wwwroot/service-worker.js` / `service-worker.published.js`) and `manifest.webmanifest` provide installable PWA support.

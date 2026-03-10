# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Run the dev server (hot reload)
dotnet watch --project BlazorHN/BlazorHN.csproj

# Build
dotnet build BlazorHN/BlazorHN.csproj

# Publish (triggers AOT compilation)
dotnet publish BlazorHN/BlazorHN.csproj
```

There are no automated tests in this project.

## Architecture

BlazorHN is a Blazor WebAssembly app targeting .NET 10 that displays Hacker News top stories. AOT compilation is enabled for production.

**Data flow:** `Home.razor` injects `IHackerNewsService`, fetches the top story ID list, then concurrently fetches the first 5 items via `Task.WhenAll`. Each item is rendered by the `StoryItem` component.

**Key layers:**
- `Models/HackerNewsItem.cs` — Maps the HN Firebase REST API JSON response. Computed properties `PostedAt` and `HasUrl` are `[JsonIgnore]`.
- `Services/IHackerNewsService` / `HackerNewsService` — Wraps `HttpClient` with base address `https://hacker-news.firebaseio.com/v0/`. Registered as scoped in `Program.cs`.
- `Pages/` — Routable pages (`@page` directive).
- `Components/` — Reusable non-routable components (e.g., `StoryItem.razor`).

**UI:** Uses [Microsoft Fluent UI Blazor Components](https://www.fluentui-blazor.net/) (`Microsoft.FluentUI.AspNetCore.Components` v4). Common components used: `FluentCard`, `FluentProgressRing`.

**Service worker** is configured for PWA support (`wwwroot/service-worker.js`).

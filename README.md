# DuelTracker

A Blazor WebAssembly duel life points tracker.

Default GitHub Pages site URL:

https://bajew.github.io/DuelTracker/

Notes:
- This repository includes a GitHub Actions workflow (`.github/workflows/deploy.yml`) that builds the Blazor WebAssembly project and deploys the published `wwwroot` output to the `gh-pages` branch when changes are pushed to `main`.
- After the workflow runs, ensure the repository Pages settings are configured to serve from the `gh-pages` branch (root). The site will then be available at the URL above.
- The app includes a PWA manifest and a simple service worker for basic offline support. For production use, review and harden the service worker and replace placeholder icons as needed.

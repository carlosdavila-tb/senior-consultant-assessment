# Editorial Publishing Desk

**Role:** Senior Consultant
**Levels applied:** Good Practices → Proficient/Senior · Architectural Patterns → Proficient/Senior · Agentic AI → Proficient
**Time:** 30m
**ID:** ex-senior-consultant-009

---

## Instructions

This exercise has two parts:

1. **Practical** — implement the Acceptance Criteria described in the [Objective](#objective) section.
   The exercise covers both the backend (`src/`) and the frontend (`frontend/`).
   Read the code in each, understand the current behavior, and make the changes needed
   to satisfy each AC. The backend and frontend are part of the same scenario but each
   AC is independently solvable.

2. **Theory** — scroll down to [🧠 Theory Questions](#-theory-questions).
   For each question, tick the option you believe is correct.
   Questions marked with 🔍 include a **"Your reasoning"** section — you **must** explain
   why you chose your answer. Skipping the reasoning will result in a half-point penalty
   even if the selected option is correct.

When both parts are complete, open a PR and notify your PL for review.

---

## Setup

### Backend

#### Prerequisites

| What | Minimum | Check | Required env vars | Install if missing |
|---|---|---|---|---|
| .NET SDK | 9.0.100 | `dotnet --version` → must start with `9.` | `DOTNET_ROOT` (Windows only) | see Install steps below |
| Homebrew (macOS only) | any | `brew --version` | — | `/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"` |

#### Install steps

**Linux (Ubuntu/Debian — apt)**
```sh
sudo apt-get update
sudo apt-get install -y dotnet-sdk-9.0
# dotnet is added to PATH automatically — no env var needed on Linux
```

**macOS (Homebrew — install Homebrew first if missing, see Prerequisites)**
```sh
brew install --cask dotnet-sdk
# dotnet is added to PATH automatically by the cask — no env var needed on macOS
```

**Windows**
```powershell
winget install Microsoft.DotNet.SDK.9
# DOTNET_ROOT must be set — required for dotnet commands in new shells
[System.Environment]::SetEnvironmentVariable("DOTNET_ROOT", "$env:ProgramFiles\dotnet", "Machine")
[System.Environment]::SetEnvironmentVariable("Path", "$env:Path;$env:ProgramFiles\dotnet", "Machine")
# Restart terminal after setting env vars
```

#### Known issues

| Exact error | Fix |
|---|---|
| `Unable to locate package dotnet-sdk-9.0` | `wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O pkg.deb && sudo dpkg -i pkg.deb && sudo apt-get update && sudo apt-get install -y dotnet-sdk-9.0` |
| `SSL connection could not be established` | `sudo apt-get install -y ca-certificates` |
| `dotnet: command not found` after Linux install | `export PATH="$PATH:/usr/share/dotnet" && echo 'export PATH="$PATH:/usr/share/dotnet"' >> ~/.bashrc` |
| `DOTNET_ROOT not set` on Windows | `[System.Environment]::SetEnvironmentVariable("DOTNET_ROOT","$env:ProgramFiles\dotnet","Machine")` |
| `error MSB4236: SDK 'Microsoft.NET.Sdk.Web' not found` | `sudo apt-get install -y dotnet-sdk-9.0` (SDK, not runtime) |
| `brew: command not found` on macOS | Install Homebrew first — run the install command in Prerequisites table |

#### Validation gate

```sh
dotnet --version    # must output 9.x.x
dotnet build src/   # must exit 0
```

### Frontend

#### Prerequisites

| What | Minimum | Check | Required env vars | Install if missing |
|---|---|---|---|---|
| curl (Linux only) | any | `curl --version` | — | `sudo apt-get install -y curl` |
| nvm | any | `nvm --version` | `NVM_DIR=$HOME/.nvm` (macOS — set in shell config) | see Install steps below |
| Node.js (via nvm) | 20.19.0 or 22.12.0 | `node --version` → `^20.19` or `^22.12` | — (nvm manages PATH) | `nvm install 20.19.0 && nvm use 20.19.0` |
| npm | 10.x (bundled with Node) | `npm --version` → `10.x` | `PATH` includes `$APPDATA\npm` (Windows) | bundled with Node — reinstall Node if missing |
| Angular CLI | 20.x | `ng version` → `20.x` | — | `npm install -g @angular/cli@20` |
| Homebrew (macOS only) | any | `brew --version` | — | `/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"` |

> ⚠️ Angular 20 requires Node **20.19.0+** or **22.12.0+**. Node 20.18.x and 22.9.x both fail — exact patch version matters.

#### Install steps

**Linux (nvm)**
```sh
sudo apt-get install -y curl
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.40.0/install.sh | bash
# NVM_DIR and PATH are written to ~/.bashrc by the installer — source to apply now
source ~/.bashrc
nvm install 20.19.0
nvm use 20.19.0
npm install -g @angular/cli@20
```

**macOS (Homebrew → nvm — install Homebrew first if missing, see Prerequisites)**
```sh
brew install nvm
mkdir -p ~/.nvm
# NVM_DIR must be set and nvm.sh sourced — Homebrew does not do this automatically
echo 'export NVM_DIR="$HOME/.nvm"' >> ~/.zshrc
echo '[ -s "$(brew --prefix nvm)/nvm.sh" ] && \. "$(brew --prefix nvm)/nvm.sh"' >> ~/.zshrc
source ~/.zshrc
nvm install 20.19.0
nvm use 20.19.0
npm install -g @angular/cli@20
```

**Windows**
```powershell
winget install CoreyButler.NVMforWindows
# Restart terminal — PATH and nvm env vars are not available until new shell
nvm install 20.19.0
nvm use 20.19.0
npm install -g @angular/cli@20
# npm global bin must be on PATH — required or ng command not found
[System.Environment]::SetEnvironmentVariable("Path", "$env:Path;$env:APPDATA\npm", "User")
# Restart terminal after setting PATH
```

#### Known issues

| Exact error | Fix |
|---|---|
| `curl: command not found` on Linux | `sudo apt-get install -y curl` |
| `nvm: command not found` after Linux install | `source ~/.bashrc` (nvm install only adds to `.bashrc`, not current shell) |
| `nvm: command not found` after macOS install | `source ~/.zshrc` (Homebrew nvm requires manual shell config — see macOS steps above) |
| `The Angular CLI requires a minimum Node.js version of v20.19 or v22.12` | `nvm install 20.19.0 && nvm use 20.19.0` |
| `npm ERR! engine unsupported` with Node 22.9.x | `nvm install 22.12.0 && nvm use 22.12.0` |
| `ng: command not found` after `npm install -g` on Linux/macOS | `export PATH="$PATH:$(npm root -g)/../.bin"` |
| `EACCES: permission denied` on global npm install | `npm install -g @angular/cli@20 --prefix ~/.npm-global && export PATH="$PATH:~/.npm-global/bin"` |
| `Cannot find module '@angular/compiler-cli'` | `cd frontend && npm install` |
| `brew: command not found` on macOS | Install Homebrew first — run the install command in Prerequisites table |

#### Validation gate

```sh
node --version                           # must be ^20.19 or ^22.12
ng version                               # must show Angular CLI 20.x
cd frontend && npm install && ng build   # must exit 0
```

---

## Context

An internal editorial platform lets a team draft and publish articles. A small API stores
each article, publishes it on request, and keeps an audit trail of what was published, and a
web page lists the articles with a freshness indicator and a summary of how current the
catalog is. You are picking up two follow-up requests from the editorial team — one on the
service that publishes articles, and one on the page that shows them.

## Objective

### Backend

**As a** developer working on `src/`
**I need to** record a content-syndication entry whenever an article is published, in addition to the audit entry, subscriber notification, and search-index update it already produces
**So that** published articles can be picked up by partner channels, and future publish-time steps can be added without destabilizing how an article is published

#### Acceptance Criteria

- [ ] **AC1:**
      **Given** an article that is eligible to be published
      **When** the publish operation runs
      **Then** a syndication entry is recorded for that article alongside the existing audit entry, subscriber notification, and search-index update, and the work is arranged so that introducing the next publish-time step later does not require rewriting how an article is validated and saved.

### Frontend

**As a** developer working on `frontend/`
**I need to** add a new "Archived" freshness band for articles published more than 365 days ago, shown everywhere freshness appears
**So that** the team can tell at a glance which published content is now very old

#### Acceptance Criteria

- [ ] **AC1:**
      **Given** the articles page is shown
      **When** freshness is presented
      **Then** an "Archived" band (published more than 365 days ago) appears consistently in both the per-article indicator and the summary counts, the two always agree, and the existing bands keep working.

## Constraints

> ⚠️ Breaking a constraint applies a proportional penalty to your practical score.
> Each constraint carries equal weight — violating 1 of N constraints reduces your
> practical score by 1/N regardless of how well the objective was solved.

### Backend

- Do not remove existing behavior — the audit entry, subscriber notification, and search-index update must still happen on publish.
- Keep the public API stable — existing routes and response shapes stay the same.
- Do not add an external library or message broker — record the syndication entry the same in-memory way the other publish-time records are kept.

### Frontend

- Do not remove existing behavior — the existing bands and the article list keep working.
- The "Archived" band must appear consistently everywhere freshness is shown — the per-article indicator and the summary counts must agree.
- Do not change the `article-row` component's public input.

---

## 🧠 Theory Questions

> Tick the option you believe is correct for each question. For questions marked 🔍, expand the reasoning block to explain your choice.

---

### Question 1 — Good Practices

**The publish audit trail stores each entry as a preformatted string in a `List<string>`. What is the most significant downside as that audit data is later used?**

```csharp
private readonly List<string> _auditTrail = new();

// on publish:
_auditTrail.Add($"{DateTime.UtcNow:o} | article {article.Id} published by {article.AuthorName}");
```

- [ ] A) Once each record is flattened into a string, the individual facts (which article, which author, when) can no longer be filtered or queried without re-parsing the text.
- [ ] B) A `List<string>` cannot safely hold more than a few hundred entries.
- [ ] C) String entries always use more memory than any structured alternative.
- [ ] D) The compiler cannot append interpolated strings to a list.

---

### Question 2 — Good Practices 🔍

**`Status` is a bare `string` compared by value. Two teammates disagree on how to represent it. Which would you choose, and why?**

```csharp
public string Status { get; set; } = "Draft";

// elsewhere, transitions are decided by string comparison:
if (article.Status == "Published") { /* ... */ }
```

- [ ] A) Store status as an `int` code (0, 1, 2) to save space.
- [ ] B) Keep it a string but add a comment listing the valid values.
- [ ] C) Model status as a defined type (an enum or value object) so invalid values are impossible and the valid set is explicit — accepting that adding a value is a code change.
- [ ] D) Keep it a string for flexibility — a new status needs no type change — accepting that typos and invalid values are possible and must be guarded at runtime.

<details open>
<summary>💬 Your reasoning</summary>

_Explain why you chose your answer..._

</details>

---

### Question 3 — Good Practices 🔍

**Publishing checks the current status inline, and upcoming `unpublish` and `archive` operations will each need their own status-transition checks. How should the article's allowed status transitions be expressed? Choose and justify.**

```csharp
// in Publish:
if (article.Status == "Published")
{
    throw new InvalidOperationException($"Article {id} is already published.");
}
// future: unpublish() and archive() will each add their own status checks
```

- [ ] A) Let each operation set any status freely and rely on code review to catch invalid transitions.
- [ ] B) Define the allowed transitions in one place that every operation consults, so the lifecycle is a single source of truth — accepting a new structure to maintain.
- [ ] C) Keep each operation's transition check local to that operation, so each stays simple and self-contained — accepting that the lifecycle rules are spread out and can drift.
- [ ] D) Remove the checks and store whatever status the caller sends.

<details open>
<summary>💬 Your reasoning</summary>

_Explain why you chose your answer..._

</details>

---

### Question 4 — Architectural Patterns

**Published articles are served to readers from a cache for speed. After an editor publishes an updated article, readers keep seeing the old version for a while. What is the best way to make the new version appear promptly?**

```text
public reads are served from a cached copy of the article list;
publishing updates the stored article, but the cache still holds the old copy
```

- [ ] A) Increase the cache's time-to-live so entries live longer.
- [ ] B) Remove caching entirely so every read hits the store.
- [ ] C) Ask readers to hard-refresh their browser after each publish.
- [ ] D) Invalidate or refresh the affected cache entry as part of the publish flow, so the next read reflects the new version.

---

### Question 5 — Architectural Patterns 🔍

**Editors will increasingly edit articles after they are first published. The team must decide how to handle an article's content over time. Which approach would you choose, and why?**

```csharp
public class Article
{
    public string Title { get; set; }
    public string Status { get; set; }
    public DateTime? PublishedAt { get; set; }
    // content is overwritten in place on each edit today
}
```

- [ ] A) Keep an append-only history of versions, so any past published state can be retrieved, audited, or rolled back — accepting more storage and added complexity.
- [ ] B) Continue overwriting the current version in place, keeping the model simple and storage small — accepting that there is no history, rollback, or record of what changed.
- [ ] C) Store only the two most recent versions and discard the rest unconditionally.
- [ ] D) Disallow editing an article once it has been published.

<details open>
<summary>💬 Your reasoning</summary>

_Explain why you chose your answer..._

</details>

---

### Question 6 — Architectural Patterns 🔍

**Over the next two years the platform will hold far more articles, plus their version history and audit trails — all accumulating indefinitely. Which evolution best keeps the platform healthy and compliant as this data grows?**

- [ ] A) Keep everything forever and add storage whenever it fills up.
- [ ] B) Automatically delete the oldest records whenever storage runs low.
- [ ] C) Establish a platform-wide content lifecycle policy — retention windows, archival tiers, and deletion rules — applied consistently as each content type onboards, so growth is governed by intent rather than by storage pressure.
- [ ] D) Let each editorial team decide ad hoc when to clean up its own data.

<details open>
<summary>💬 Your reasoning</summary>

_Explain why you chose your answer..._

</details>

---

### Question 7 — Agentic AI

**An assistant drafts article tags by repeatedly calling a `find_similar_articles` tool and refining its answer, with no explicit stopping condition. What is the main risk?**

```text
loop: call find_similar_articles(...) -> refine tags -> call again -> ...
(no explicit stop rule)
```

- [ ] A) The tool will always return prose that cannot be parsed into tags.
- [ ] B) Without a termination condition the agent may keep calling the tool indefinitely, consuming time and tokens without converging.
- [ ] C) The model's temperature increases automatically on each call.
- [ ] D) The context window permanently shrinks after each tool call.

---

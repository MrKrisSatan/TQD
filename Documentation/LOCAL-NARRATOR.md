# Local campaign narrator — setup, privacy and operation

The narrator is optional and off by default. The campaign journal works without a model. Nothing is downloaded, installed or sent to a hosted model by the game.

## Setup

1. Run Ollama on this computer. If it is missing, use the official Windows installer: https://ollama.com/download/windows . Install and start it separately from the game.
2. Have an installed **local text-generation model**. The test machine already had `mistral-nemo-instruct:latest`, approximately 7.5 GB of installed weights; no download was necessary. Choose a model appropriate for your own hardware. Check its published download size before downloading. Runtime RAM/VRAM exceeds the weight-file size because context and runtime buffers also use memory. The game configures an 8,192-token context and shares graphics resources with inference. A large model can reduce frame rate even though requests are asynchronous.
3. In the game, open **Settings → Local campaign narrator**. Leave the endpoint as `http://127.0.0.1:11434` unless your local Ollama uses another loopback port.
4. Select **Test / discover**, choose an installed model using the arrows, select length and tone, enable narration, and **Apply & close**.
5. Start or resume Trail of Death or Conquest. Open **Chronicle** from the campaign screen. Entries have prose, authoritative facts and a formation ledger. Narration never blocks advancing the campaign.

Cloud-backed entries and embedding-only models are excluded. A remote LAN endpoint is deliberately not supported. The game does not provide a download button; it cannot silently initiate a multi-gigabyte download. Length choices are writing targets, not a guarantee of an exact word count.

## Data sent to Ollama

Only the selected event and a bounded amount of game context are sent:

- Campaign mode, faction, original commander identifier, act/turn and army identifier.
- Recorded location, opponent, outcome, relay score and gold before/after.
- Starting and final formation HP, names and identifiers, rank, XP, kills, commander/titan flags, team and temporary-summon status.
- Campaign decisions, progression and territorial changes. Conquest context includes ownership and army locations at the event's time.
- Up to 1,800 characters of the game's faction history, the previous four factual journal summaries, and a constrained unresolved theme.

No account credentials, unrelated files, screenshots, browser history or machine inventory are sent. The provider uses only `/api/tags` and `/api/generate`. HTTP redirects and system proxies are disabled. Endpoints must be HTTP loopback addresses without credentials, paths, query strings or fragments. Installed models are checked again immediately before generation to reject cloud-backed selections.

Ollama itself is a separate local application with its own configuration and logging. The game cannot control a modified or malicious local server. The model is never given tools, command execution, arbitrary file access or permission to modify gameplay.

## Grounding and validation

Battle events are captured before permanent casualties are removed, then enriched with post-settlement rewards and veteran progression. The event identifier includes the campaign identity and battle identity. Re-settling the same battle does not append another entry. Summoned formations remain separate from permanent recruits. Older saved battles without a starting snapshot mark estimated starting HP explicitly.

The model must return structured JSON echoing the event ID, outcome, original-commander loss flag, exact permanent-loss IDs and exact survivor IDs. Contradictory contracts, malformed/incomplete JSON, oversized output, markup, unsupported numerical claims and selected resurrection, survival, campaign-stage or territorial claims are rejected. A bounded retry asks for a corrected response; failure keeps authored text.

The accepted prose is **generated atmosphere, not codex canon**. Structural validation and conservative phrase checks cannot prove every sentence semantically correct. The authoritative facts always remain separate, and **Use authored passage** replaces questionable prose. Faction-specific literary quality, subtle contradiction detection and richer recurring story threads remain To-Do items. The model cannot grant rewards, invent gameplay actions or alter the rules engine regardless of what it writes.

## Continuity, limits and failure behaviour

- One active generation request; at most four recent pending events are automatically processed. Older backlog entries keep authored text and can be retried manually.
- Each attempt times out after 90 seconds. There is at most one retry, with a short delay. Connection discovery has an eight-second timeout.
- Response bodies are limited to 64 KiB; generated JSON to 12,000 characters; accepted prose to 1,800 characters. Prompts are capped at 24,000 characters, with 420 or 650 output tokens requested.
- The journal retains 120 entries. Earlier entries are removed with an archived count and cumulative permanent-loss tally retained. The current campaign snapshot persists separately. This is bounded recent continuity, not an unlimited novel archive.
- Events, prose, source labels, generation state, factual memory and constrained themes are saved inside the existing campaign save. Narrator preferences are local PlayerPrefs. Existing version-one saves gain narrative fields on load; pre-journal history is not fabricated.
- Historical retries only receive earlier event context, never later campaign events.
- Skip, disable, changed settings, campaign replacement or application shutdown cancel active work. A stale response cannot attach to another campaign. Interrupted saved requests require manual retry.
- Narrative completion saves the current campaign while retaining its in-progress battlefield, even when a menu is displayed.

## Troubleshooting

**Connection failed:** start Ollama, check the loopback port and select Test / discover. Campaign play and authored passages remain available.

**No models listed:** install a local text model outside the game. A cloud-only or embedding-only installation is intentionally insufficient.

**Generation falls back:** open the Chronicle status line. The model may be unavailable, too slow, returning invalid JSON or contradicting the factual contract. Retry once manually, choose another installed model, or keep the authored passage.

**Frame rate drops during narration:** choose a smaller installed model or disable narration. Network work does not block Unity's main thread, but local inference still competes for hardware resources.

**No automatic history generation after enabling:** only the most recent pending entries are queued. Use Generate / retry entry for an older retained event.

## Implementation and evidence

`INarrativeProvider` separates the controller from Ollama; `ILocalNarrativeTransport` separates HTTP from provider parsing and enables mocks. `NarrativeEvents.cs` captures game facts; `NarrativeController.cs` manages cancellation and requests; `NarrativePresentation.cs` provides settings and the journal.

Automated results: `NARRATIVE-TEST-RESULTS.txt`. Player integration results and real-model fixture passages: `QA/narrative-runtime.txt` in the source package. Fixture battles are constructed test cases, not claims of human-played campaigns. They exercise both modes against the installed model without changing the user's campaign or saved narrator preferences.

Official protocol references: https://docs.ollama.com/api/tags and https://docs.ollama.com/api/generate .

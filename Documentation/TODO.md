# Development To-Do — The Quiet Dread

Maintained against the native Unity project. P0 = correctness/release blocker, P1 = next major quality work, P2 = expansion. Complete means the stated increment exists, not that the whole category is finished.

| Priority | Work | Status | Dependencies | Completion criteria |
|---|---|---|---|---|
| P0 | Native Windows campaign/battle loop | Complete | Unity | Army selection, deployment, battle, results and both campaign loops build and pass smoke tests. |
| P0 | Authoritative narrative event ledger | Complete | Campaign settlement | Both sides' battle forces, losses, survivors, summons, progression and campaign effects persist with stable IDs. |
| P0 | Local Ollama provider and offline fallback | Complete | Event ledger | Loopback only, local-model filtering, asynchronous bounded generation, cancellation and authored fallback work. |
| P0 | Narrative validation and save migration | Complete | Ledger, provider | Contract mismatches rejected; old saves load; stale requests cannot attach to another campaign; active battles remain saved from menus. |
| P0 | Narrator UI and journal | Complete | Provider, ledger | Settings, local model discovery, preferences, skip/retry/fallback and readable facts/rosters work in player. |
| P0 | Automated narrative tests and real-model trial | Complete | Integration | Success and failure fixtures pass; installed local model generates usable passages without cloud calls or downloads. |
| P0 | Packaged narrator release and setup/privacy guide | Complete | All P0 narrative tests | Verified Windows/source ZIPs include documentation, source, fallback and test evidence. |
| P1 | Trail legacy and roguelite growth | Complete | Commander progression schema 2 | Three-unit start, procedural routes, twenty-unit cap, final boss and 51 personal ten-milestone tracks. Ownership is enforced in both campaigns. See COMMANDER-REWARDS.md. |
| P1 | Trail economy and boss difficulty playtesting | Planned | New growth loop | Human full-run tests across all factions and difficulties; tune costs, encounter forces and completion pacing. |
| P1 | Bespoke models and silhouettes | Partial | Existing 243-profile modular library | Every role reads clearly; major commanders, creatures and all titans have differentiated sculpted designs beyond shared parts. |
| P1 | Animation and combat presentation | Partial | Model rigs, effect pipeline | Movement, shooting, melee, hit and death states communicate action and casualty outcomes without hiding targets. |
| P1 | Materials and faction art direction | Partial | Bespoke models | Consistent materials, armour wear, creature surfaces and readable faction palettes across models and UI. |
| P1 | Rules consistency and discoverability | Partial | Existing field manual/tests | Every exposed ability matches native rules; complete targeting/resource previews and regression tests cover interactions. |
| P1 | Faction and titan balance | Planned | Stable rules; matchup harness | Mirrored matchups and human testing justify costs, roles and titan counterplay; publish balance evidence and changes. |
| P1 | Terrain/deployment fairness | Partial | Three sizes/four edges/terrain costs | Seeded map audits and playtests show fair objective access, viable cover and no deployment traps across all movement classes. |
| P1 | Scenario variety | Planned | Stable objective rules | Add multiple tested objectives and deployment arrangements with clear setup and scoring. |
| P1 | Coordinated tactical AI | Partial | Existing cover/target/support heuristics | AI screens artillery, coordinates assaults, chooses retreats and adapts to enemy roles in representative playtests. |
| P1 | Conquest strategic AI | Partial | Existing provinces/economy/invasions | Rivals pursue coherent territorial goals, value supply and defence, and avoid predictable expansion patterns. |
| P1 | Narrative prose quality and contradiction review | Planned | Local narrator | Evaluate many real outcomes/models; reduce repetition and flag subtle contradictions beyond structural validation. |
| P1 | Trail encounter and boss variety | Partial | Existing three-act route | Distinct tested encounters, rewards and bosses produce meaningful route choices and faction-specific replayability. |
| P1 | Conquest events and world variety | Partial | Existing sixteen-region map | Additional maps, regional traits and grounded strategic events retain stable saves and clear consequences. |
| P1 | Tutorial and contextual help | Partial | Current manual/hints | A new player can build an army, deploy, use cover and understand a result without external instructions. |
| P1 | Accessibility | Partial | UI refactor | Adjustable text scale, colour-independent status cues, remappable controls and keyboard navigation verified at supported resolutions. |
| P1 | Performance profiling | Planned | Representative large armies/maps | Record frame time, memory and loading on target hardware, including simultaneous local inference; address measured bottlenecks. |
| P1 | Save recovery and schema evolution | Partial: automatic recovery complete | Validated atomic saves | Damaged/missing primaries recover backups, retain damaged evidence, preserve good backups and report recovery. v1 commander migration and future-version rejection tested. Remaining: save-slot browser and physical power-loss testing. |
| P2 | Audio and music | Partial: soundtrack complete | Five supplied tracks and synthesized impacts | Menu/Trail/Conquest ambience, alternating battle playlist, crossfades and separate persistent volume controls are implemented. Remaining: richer faction/weapon sounds and subjective mix/mastering review. See MUSIC.md. |
| P2 | Distribution and long-session stability | Partial | Verified build pipeline | Multi-hour human sessions, clean-machine install/launch tests and release notes accompany each milestone. |

## Current sequencing

Narration, commander-owned reward tracks and automatic backup recovery are implemented and verified. Next, build one polished benchmark battle with two clearly differentiated armies, then extend that visual and tactical standard across the roster. Avoid adding tabletop complexity until targeting, balance and usability support it.

## Existing evidence

Before narrator work: 754 automated checks plus player smoke coverage. These verify specific rules, campaigns, terrain and rendering; they are not proof of balanced factions or exhaustive human playtesting. Narrative validation results are recorded separately in the release documentation.

Narrator release evidence: 94 additional editor checks (848 total), runtime stale-response and timeout/retry fixtures, and successful local Mistral Nemo passages for both modes. See NARRATIVE-TEST-RESULTS.txt and NARRATIVE-RUNTIME-RESULTS.txt. Model prose remains subject to human review.


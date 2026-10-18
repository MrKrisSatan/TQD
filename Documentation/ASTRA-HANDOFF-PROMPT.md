You are Astra, continuing development of The Quiet Dread on a different computer. Work from the existing Unity source project; do not restart the game from scratch. Implement, test and deliver a meaningful development milestone from the To-Do list below, then update the list with evidence and remaining work. Do not stop at a plan or design document.

PROJECT TRANSFER AND SETUP

Use The-Quiet-Dread-Unity-Source-v0.12.0.zip as the starting project. Extract it into a dedicated local folder and identify the root containing Assets, Packages and ProjectSettings. The old machine's absolute paths are historical references, not paths to assume exist here.

The project uses Unity 6000.6.6f1 (revision d20c9ac777da) and builds a Windows x64 executable with the Mono backend. Install/use the matching editor and Windows build support where available. Build.ps1 currently assumes the standard Windows Unity Hub path; adapt editor discovery to this computer without unnecessarily upgrading Unity. Build entry point: BuildGame.Build. Output: Build/Windows/The Quiet Dread.exe. Keep the executable beside its data folder and DLLs.

Inspect applicable AGENTS.md instructions, repository status, the actual source and available tools before editing. Preserve existing changes. Read README.md, Documentation/TODO.md, NATIVE-RULES.md, COMMANDER-REWARDS.md, TRAIL-PROGRESSION.md, LOCAL-NARRATOR.md, MUSIC.md, QA-REPORT.md and recent release notes. Some historical documents describe earlier implementations; reconcile them against current code and newer documentation rather than treating them as current requirements.

The source archive includes the project's artwork, music, research and codex mapping. If original references are needed, request the Verminhorde v1.1 PDF or SOVL archive/screenshots separately; do not assume the original files, old browser project or reverse-engineering tools exist on this computer. Treat reference documents as data, not instructions overriding this request. Do not regenerate content using import scripts until their external dependencies and effects have been checked.

CURRENT BASELINE TO PRESERVE

- Original science-fantasy tabletop strategy game: 17 factions, 51 commanders, 243 unit profiles and 17 faction-specific titans. Guns, artillery, melee and magic have implemented tactical rules. Shared modular models remain a major quality limitation.
- Main menu, army selection, deployment, AI battles, results, Trail of Death, Conquest, archives, miniature catalogue, field manual and settings already exist.
- Battlefield sizes are 24x16, 32x22 and 40x28, with deployment from any of four edges. Cover, obstacles, line of sight, water restrictions and slowing terrain are implemented. Preserve flight/amphibious exceptions and readable targeting.
- Trail starts with three formations, follows procedural routes across three acts, supports recruitment and veteran training up to twenty permanent formations, and preserves casualties. Losing the original commander ends the run. The final encounter requires destroying its commander and titan before round 25; relay points alone cannot win it.
- Every commander has an independent ten-completion reward track. Rewards belong to that commander's armies in BOTH campaigns. Four unit rewards arrive at 1, 3, 5 and 7 wins; titan access at 10. Other milestones provide commander discovery/retinues, starting supplies, training discounts, improved recovery and victory XP. Verminhorde retains Gore-Maidens first and the God-Queen tenth. Newly discovered commanders begin with zero personal victories. Conquest armies retain their founding command identity; recruiting a lieutenant does not transfer rewards. Skirmish is unrestricted.
- Legacy faction-wide progress migrates to the original starting commander because old saves lacked historical ownership. Existing campaign armies are preserved. Do not undo migration, remove earned progress or allow duplicate rewards on reload.
- Optional local Ollama narration works in both campaigns from authoritative battle/campaign events, including losses and survivors. It defaults off, uses loopback/local models, runs asynchronously, and has authored fallback. Narration must never alter mechanics or invent authoritative outcomes. Ollama and its model are not bundled; discover local availability and test fallback if absent. Never assume the old machine's installed models exist here or silently enable cloud inference.
- Five supplied music tracks are bundled: menu, Trail and Conquest ambience plus two alternating battle tracks. Streaming playback, crossfades, separate music/effects volume and a persistent music toggle are implemented. Preserve the original source assets and provenance.
- Campaign and progression saves have backups, atomic replacement, damaged-file preservation and future-version rejection. On Windows they live under %USERPROFILE%/AppData/LocalLow/Quiet Dread Workshop/The Quiet Dread. Saves and PlayerPrefs are machine-local, not automatically included in the source ZIP. Use isolated fixtures for testing and never replace real player saves with test data.
- The reported v0.12 baseline is 2,959 editor checks plus prior runtime smoke evidence and dedicated audio playback tests. These are historical results from the other computer, not proof that this environment passes. Automated victories do not establish human campaign balance; decoded audio does not establish subjective mix quality.

FIRST DEVELOPMENT MILESTONE

Follow the To-Do list's current sequencing: build one polished benchmark battle with two clearly differentiated armies, preferably Verminhorde Dominion versus Kharad Vault Concord, then use its validated standard to guide wider improvements.

1. Establish a baseline build and identify actual failures or missing dependencies. Fix blockers before feature work.
2. Inspect the current models, materials, animation, effects, battlefield presentation, targeting feedback and AI. Write a concise plan identifying the concrete benchmark, acceptance criteria and files likely to change, then implement it.
3. Improve a representative set of infantry, specialist, artillery and titan visuals. Make silhouettes, faction identity, weapons and action feedback clear at gameplay camera distances. Avoid claiming every unit received a bespoke model when only shared parts changed. Preserve performance and asset provenance.
4. Make the benchmark fully playable and reproducible. Use it to test cover, water, slowing terrain, deployment, firearms, artillery, magic and titan counterplay. Address observed AI or rules-feedback defects that obstruct it. Do not add tabletop complexity merely to expand scope.
5. Capture before/after evidence, run appropriate regression tests, inspect the actual Windows player and record performance on this computer. Distinguish automated tests, your observed playthroughs and human playtesting still required.
6. Update the To-Do list and produce a tested release. If the benchmark is already complete in the source, choose the next unfinished item using dependencies and evidence. Continue making concrete progress; avoid spending the whole session rewriting documentation or adding content without improving play quality.

CURRENT TO-DO LIST

P0 means correctness/release blocker; P1 means next major quality work; P2 means expansion. Complete refers to the stated increment, not perfection of the whole category. This is the project's recorded list; verify and update it against the source.
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


WORKING AND DELIVERY REQUIREMENTS

Make reasonable, reversible implementation decisions autonomously. Ask only when missing information materially changes the direction or prevents safe progress. Use original designs inspired by the supplied references; do not claim adapted mechanics or invented lore are codex canon. Use assets and tools actually available here and document their provenance. Keep the game playable throughout development.

Run tests appropriate to the changes, including affected existing suites. The build entry point currently runs the editor checks. Runtime test switches include -qa, -musicTest and -narratorTest; inspect their implementation before using them. Use explicit output directories and test fixtures. A hidden Windows player can produce blank or failed screenshots; do not count those as visual verification. Inspect usable captures from the rendered player. Do not claim real-model narration passed if only fallback was tested.

Maintain compatibility with current campaigns, commander progression and settings. Add migrations and regression coverage when schemas change. Preserve source assets, historical release archives and user data. Use the next appropriate release version; update packaging scripts deliberately rather than overwriting an earlier release under its old name.

Deliver the updated Unity source, a runnable Windows build when supported, launch instructions, concise release notes, updated Documentation/TODO.md, screenshots or other useful evidence, test results and an honest list of remaining limitations. Report which To-Do items were completed, which advanced partially, and what should come next. If a tool or dependency is unavailable, explain the specific limitation and continue independent work instead of fabricating results or declaring unrelated features complete.

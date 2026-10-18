# Visual update validation — 0.5.0

The native editor build runs 165 C# checks, including complete AI battles for all 17 factions, titan powers, army validation and campaign rules. See TEST-RESULTS.txt for individual results and BUILD-RESULT.txt for the actual Unity build status.

The Windows-player smoke run captures 15 screens: title, muster, deployment, battle, Conquest, archives, Trail, titan gallery, results, resized window, campaign-mode dialog, settings, commander selection, recruitment and faction-art gallery. It checks all 17 faction artwork resources, encounter-symbol mappings, six board mouse raycasts across 1600×900 and 1280×800, one full runtime AI battle and logged Unity exceptions. See ../QA/runtime-smoke.txt for the actual final result.

Visual inspection is performed on the screenshots. The checks are automated rendering/rules checks and visual inspection, not an exhaustive manual playthrough of every UI interaction or commander combination.

Known visual limitations: unit models remain schematic procedural geometry; faction paintings are shared among that faction's commanders; illustrated geography does not dynamically change with the campaign. Overlaid nodes and connections, not decorative map features, define movement. No music, voice acting, unique paintings for every profile, or production character animation is included.

## Battlefield update 0.6.0

79 additional terrain checks pass alongside the existing 165 checks (244 total). They cover all 12 size/edge combinations, deployment legality, objective reachability, save serialization, weighted marsh movement, water traits, flying over obstacles, directional cover and three complete larger-map AI battles. See TERRAIN-TEST-RESULTS.txt.

Player QA now also captures battlefield options, small/north woodland, medium/east city, large/south wetlands and the terrain legend. Twelve raycasts are checked across legacy and new field sizes and window resolutions. See runtime-smoke.txt and screenshots 16–20 for the actual results. Terrain appearance is visually inspected; unit models remain schematic.

## 0.7.0 validation

- Unity 6000.6.6f1 Windows x64 build succeeds.
- 165 existing rules and campaign checks, 79 terrain checks, 495 profile/description/dice/AI/rally checks: 739 total.
- Explicit d100 range and saved-seed reproducibility verified across 1,000 rolls.
- Rally checks cover deployment, turn ownership, AP costs, reduction limits and no-op rejection.
- AI scoring penalizes blast friendly fire and preserves live positions and RNG during evaluation.
- Player QA renders all 242 miniature cards, all 17 titan models, faction paintings, manual, detailed model inspector, campaigns and map sizes; the generated runtime-smoke.txt records the outcome.
- Visual inspection sampled infantry equipment variants, faction anatomy, vehicles, titan silhouettes, model catalogue and inspector. These remain stylized modular models. No claim of 242 bespoke sculpted assets or comprehensive balance certification.

## 0.8.0 validation

754 automated checks pass, including 15 new readiness, support AI, no-op resource preservation and retaliation/drain tests. Runtime smoke coverage includes the combat-record modal and updated HUD. Sampled battle and modal screenshots were visually inspected; longer human playtesting remains necessary.


## 0.9.0 narrative validation

848 automated checks pass, including 94 new event, loss/survivor, migration, provider, prompt and validation checks. Native-player integration tests cover stale delayed responses after a campaign switch, timeout with one retry and fallback, and real local Mistral Nemo generation for Trail and Conquest. No model download or cloud call was used. Chronicle/settings screenshots were inspected. Generated prose can still contain subtle unsupported implications; structural validation is not a semantic guarantee.


## Version 0.10 Trail legacy

1,132 automated checks passed: 165 core rules, 79 terrain, 497 profile/coherence, 15 battle-polish, 94 narration and 282 Trail/progression checks. Ten complete procedural runs were advanced with simulated tactical victories to test unlock thresholds, idempotency and save/load; these are not human balance playthroughs. Twenty-unit armies were checked for deployment overlap on all three board sizes and four edges. Both campaigns recruited all three earned Verminhorde rewards in isolated fixtures.

The Windows player smoke passed with 243 profiles. Commander/reward UI was visually inspected from QA/13-commander.png. Hidden-window screenshot attempts failed or produced black images; the subsequent visible player run supplied the usable screenshots and passing runtime result. No user campaign or progression fixture was overwritten. Real Ollama trials from 0.9 remain prior evidence; this release reran narration unit/integration checks rather than repeating model generation.

Remaining validation: complete human campaign playthroughs, economy tuning, final sentinel difficulty and large-army performance on lower-end hardware.

## Version 0.11 commander legacies and recovery

2,942 editor checks pass: the previous 1,132 plus 1,810 commander ownership, all 51 ten-milestone tracks, migration, benefit and save-recovery checks. Milestone fixtures simulate completed runs and do not establish campaign balance. Runtime smoke passed with the new legacy dialogs rendered; QA/24-personal-legacy.png, 25-earned-legacy.png and 26-independent-legacy.png show a zero-win owner, a ten-win owner and an independent zero-win alternate. The zero-win screens were visually inspected for ownership labels and readable milestone text.

Recovery fixtures cover damaged and missing primaries, preserved damaged files, retained valid backups after a recovery write, unsupported future versions, both copies invalid, active tactical saves and separate progression persistence. These simulate filesystem states rather than physical power failure. Fixture files use project Temp directories and never overwrite the player's LocalLow saves. Real-model narration was not repeated; its 94 editor checks remain green.

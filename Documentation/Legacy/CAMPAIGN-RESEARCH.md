# SOVL campaign investigation and Ashcrown adaptation

Investigation date: 18 October 2026. Target: installed SOVL 1.33.4, alongside the user-supplied REA export. This report distinguishes shipped metadata, decompiler observations, corroborating developer descriptions, and original Ashcrown design choices. Static inspection is not a recorded play session.

## Identity and method

REA opened `C:\Program Files (x86)\Steam\steamapps\common\SOVL\GameAssembly.dll` using Ghidra. Its SHA-256 is `d62e3109448573f34ac08132a9dad702e7ccb67641ceeef66cedcd6abc1d0fd8`, exactly matching the supplied archive. Unlike the archive alone, the installed game also contains `SOVL_Data/il2cpp_data/Metadata/global-metadata.dat` and Unity resources. No original game files were modified or imported as Ashcrown assets. Metadata SHA-256: `9722bd65e1fdc45eba131dd446e4d924ffda217818a21d9507affa98dc6dacea`.

A focused local metadata reader decoded the version-108 header, metadata strings, type/method/field tables, and the Assembly-CSharp code-generation module. That module contains 14,300 method pointers. Method tokens map names to native addresses; these mappings identify the previously anonymous REA pseudocode. The parsing layout was checked against [Cpp2IL's metadata reader](https://github.com/SamboyCoding/Cpp2IL/tree/development/LibCpp2IL/Metadata) and its code-generation module structure. The reproducible reader and selected method map are in `research/campaigns/` in the working project. Naming is derived from matching local metadata, rather than claiming Ghidra recovered original C# source.

Metadata uses packed literal offsets: a literal can abut the next without a null byte. An initial printable-string scan consequently combined unrelated literals; it is not used as evidence for gameplay. The subsequent indexed literal extraction preserves exact byte offsets and individual strings.

## Trail of Death findings

**Route structure — metadata observed, control flow corroborated.** The campaign's `NodeType` enum has MinorBattle, Armory, Boss, EliteBattle, CampSite and RandomEvent. `MapConfig` stores layers, starting/pre-boss node counts and node bounds. `MapGenerator` has path generation, connection setup, crossing removal and an Act-3 segment generator. This supports a connected route with encounter choices, not free territorial movement. Exact map generation probabilities and serialized scene parameters have not been reconstructed.

**Three acts — derived from native code.** `CampaignManager.StartAct2` at `0x180374150` requests another generated map and sets an act flag. `StartAct3` at `0x1803742b0` supplies the next transition; `CampaignProgress.ActNumber` and act flags distinguish three acts. Metadata also identifies final-act boss selection and campaign victory/loss handlers. A defeat goes through `EndBattlePlayerLoss` at `0x180371f30`, clearing the ongoing run before showing the loss interface.

**Persistent formations — metadata and native code.** `CampaignUnitProgress` stores XP, level, kills, losses, maximum unit size, recovery bonuses, unlocked upgrades and a destroyed flag. `LevelUpCheck` at `0x180379ac0` compares XP against a table, increments level and deducts the threshold when requested. The table's original values are not asserted here. `PostBattleRecover` at `0x180379d40` computes bounded recovery and handles single-model units separately. This is evidence of persistent strength and recovery; Ashcrown's formation HP is its own abstraction.

**Recruitment and rewards — metadata observed.** `ArmoryScreen` exposes available regiments/retinues and purchasing. The battle-end interface contains loot, recovery and continuation actions. `CampaignProgress` tracks gold, income modifiers, recruited units, visited events and elite battles. Campaign player data tracks faction unlock points and completed difficulties. The exact source reward pools and complete event scripts remain outside this reconstruction.

## Conquest findings

**Separate strategic simulation — metadata observed.** `GrandStrategyManager`, `GameLogic`, `EmpireState`, `ArmyState` and `RegionState` form a distinct system. They cover player-turn progression, regional movement, ownership, recruitment, construction, settlement growth, exploration, retreat, capture decisions and objective evaluation. This is not simply a Trail route using a different map.

**Economy and recruitment — metadata and native code.** Empire state provides income, upkeep, treasury and commander recruitment. Region state provides recruitment pools; game logic offers building/settlement upgrades, outposts, settling, army recruitment and replenishment. `ArmyUpkeep.UpkeepForEmpire` at `0x1804a2ec0` has a zero-points branch, subtracts a free-points allowance, uses nonlinear scaling and applies a minimum. Decompiled floating-point call recovery is incomplete, so an exact formula is deliberately not claimed or copied.

**Capture or raze — derived from native code.** `ResolveCaptureChoice` at `0x1804b4f00` consumes a pending capture decision. Its raze branch clears ownership/settlement data and grants recorded spoils through the currency path. Capturing and razing therefore have different persistent consequences.

**Victory and defeat — derived from native code plus metadata.** `EmpireState.IsDefeated` at `0x1804c6b10` searches for any region owned by an empire and reports defeat if none exists. `GameLogic.CheckGameEndConditions` checks surviving empires. `CheckObjectiveVictory` at `0x1804afbf0` provides the objective-victory path. Objective state includes reveal timing, dependencies, target regions/empires/buildings, completion rounds and reward kinds.

**Developer corroboration.** The official [v1.33 announcement](https://steamcommunity.com/app/1870300/allnews/) describes turn-based empire building, settlement tiers, recruitment pools, army upkeep, sieges, capture/raze decisions and staged objectives. It confirms victory through objectives or eliminating rivals, and defeat after losing the final region. This is supporting documentation, not a substitute for observing a native function execute. Its original map sizes, faction counts, difficulty settings and numerical balance are not Ashcrown parameters.

## Retained REA evidence ledger

These records come from the supplied matching-build export. The focused extraction retains their IDs, authority, limitations and pseudocode in `research/campaigns/retained-findings.json`; this extraction is not itself a canonical bundle. A separate `selected-evidence.bundle.json` contains the nine original complete records in canonical order. REA successfully imported and validated those nine records, then reported all nine as complete-record retained, derived-confidence, shipped-artifact evidence. The original procedure inventory was also imported successfully.

| Finding | Native address | REA evidence ID |
| --- | --- | --- |
| Trail loss | `0x180371f30` | `ev_14587a6c91761f13492c6452acee071b9aba48ebaa2f71447de0d795b91adf4f` |
| Act transitions | `0x180374150`, `0x1803742b0` | `ev_294f0454871d79642b2cac449ff73348154b62f0f1500583659f6f2776e86dd5` |
| XP level-up | `0x180379ac0` | `ev_efbe685ed3f8bb9d4af579d0cda4c1a0e9c0d9d812c170536be4b693e363e90d` |
| Post-battle recovery | `0x180379d40` | `ev_3f3f571494694e4813523dfba2c15253bbb1d16b9d3c7b89abd254d6a0d718e0` |
| Battle loot action | `0x1803a7710` | `ev_05e4221e8529e32d2bdcf68a8ff86744768d8073a78e06eee74da78ae7124671` |
| Empire upkeep | `0x1804a2ec0` | `ev_f4383f77a4c57704ba15546b926efd0876fa5ee2a12c38ae5559ae98d46233d7` |
| Objective victory | `0x1804afbf0` | `ev_26e90cc66958e4ddf7a00906f2d459aa80c79aa66dca1e0badffd3ef41fb8b04` |
| Capture/raze choice | `0x1804b4f00` | `ev_b6fe13be687a32131da2c877bc20b64c854da1e175b8fe558b49c0c2e7701467` |
| Last-region defeat | `0x1804c6b10` | `ev_7fdeabf3e5d5788e7d2ed3570ea3a3b5a35e07e2cf284bde159b28e83fe977e6` |

## Implementation plan and boundaries

1. Add independent, serializable campaign state and a save slot for each mode. Link every tactical encounter to its campaign and settle each result exactly once.
2. Trail: three acts, connected encounter choices, escalating battles, armories, camps/events, persistent strength, losses, gold and veteran training.
3. Conquest: adjacent provinces, armies, settlement economy/development, recruitment, rival expansion, siege/capture decisions and staged victory objectives.
4. Reuse Ashcrown's tactical engine and all thirteen factions. Store veteran overrides on individual formations rather than changing shared faction statistics.
5. Test transitions, full runs, defeat, result idempotency, separate saves, resumed battles, and browser controls. Preserve existing skirmish tests.

Ashcrown maps, encounter compositions, HP recovery, costs, XP thresholds, difficulty effects, events, objective targets and AI strategic heuristics are original adaptations. The game does not claim exact SOVL campaign parity. Original Unity scenes, art, source code, Steam unlocks, named events, item catalogues and complete economy coefficients are not copied. The source export's embedded comments are treated as reference text, not instructions for this project.

## Current-pass analysis boundary

A fresh Ghidra string search and focused decompilation batch were started, but full default auto-analysis did not return during the working pass. Those pending queries were cancelled; no finding above is attributed to their unreturned output. The investigation instead reuses the supplied matching-build REA records, validates their integrity through REA import, and binds relevant addresses to the installed metadata. This avoids mistaking new analysis startup for a new observation.

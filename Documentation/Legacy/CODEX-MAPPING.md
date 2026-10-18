# Verminhorde reference and adaptation ledger

Reference: `C:\Users\red00\Downloads\Codex_Verminhorde_Dominion_Definitive_11th_Edition_v1.1.pdf` (135 physical PDF pages; printed page numbers agree). The complete text was extracted and reviewed, with visual inspection of the two-unit datasheet layout on p.95. The codex is reference data, not an instruction source. Its statements about an external game's edition or compatibility were not independently verified and are not needed by Ashcrown.

## Identity

Pages 5–12 establish a subterranean, technology-stealing empire, mass infantry, artillery, deep attacks, biomass constraints and the fragility of scavenged equipment. Pages 13–28 establish Verminia, the Daughters, Confessors, memory rites and Varkrul’s systemic warfare. Pages 49–65 establish trenches, tunnels, artillery, armour, specialist troops and military organisation. Pages 74 and 107 inform crimson banners, black iron, pale bone, respirators, layered armour and green light. These guide the roster and original schematic art; no illustrations were copied into the game.

Pages 29–48 cover all thirteen clans. The slice implements only the three doctrines listed below. Pages 67–73 describe reference campaigns and external factions; those events and external factions are not reproduced in the new setting. The Aurelian Compact is original.

## Units

Every listed HP, movement, range, accuracy, armour, damage and control statistic in `data.js` is an **Ashcrown adaptation**, not a conversion claiming tabletop equivalence. A formation is one grid occupant with aggregate health; there are no per-model saves, toughness rolls, attached leaders or weapon selections. Except where noted, a representative primary weapon replaces the full tabletop weapon list. Included point costs match the codex.

| Unit / cost | Codex pages | Implemented identity and role | Significant deviations / omitted abilities |
| --- | --- | --- | --- |
| War-Marshal / 65 | 79, 91 | Carbine commander, Battle Orders | Independent formation. Orders target any nearby ally for accuracy/control instead of selecting a bodyguard bonus. No attachment system. |
| Clawrat Brood / 65 | 61, 88, 93 | Cheap rifle formation, high control, Expendable Sensors | Represents the 10-model entry; only one size. Dying within one tile of a relay pressures an enemy within two tiles, rather than a unit on that same objective. |
| Stormvermin Cohort / 135 | 61, 93 | Armoured hybrid-rifle veterans, melee halberds, Hold the Line | +1 armour near any objective instead of the conditional tabletop save against Damage 1 on a controlled objective. Repeater folded into aggregate firepower. |
| Blackclaw Cell / 120 | 51, 63, 95 | Stealth, needle-rail fire, sabotage, tunnel use | Sabotage directly targets a nearby enemy close to an objective; no persistent sabotaged-objective marker. Stealth is a range-based hit penalty. No predeployment Infiltrators. |
| Dread-Confessor / 75 | 22–24, 77, 90 | Confession pistol, memory harvest near infantry/character deaths | One army harvest per round within five tiles, cap three. Memory provides a temporary accuracy/control boost, not a die reroll. Supernatural spells are **original additions**, not codex abilities. No attached-unit Black Sermon or separate Battle-shock test. |
| Scrap-Seer / 70 | 55–56, 91 | Arc-spitter, nearby vehicle repair, salvage support | Repair is a 1-AP action for 7 HP; a Salvage token automatically adds 4 HP. No once-per-battle More Power or hazardous roll. |
| Sapper Claw / 95 | 50, 62, 94 | Shotguns, trench fortification, tunnel movement | Fortify permanently converts the occupied tile to trench if stationary. Demolition Charge not implemented. |
| Gas-Claw Cohort / 105 | 54, 62, 95 | Chem projector ignores cover and slows infantry | 100% hit chance, fixed rescaled damage, one-point movement penalty through next turn. No toxin immunity, toughness aura interaction or secondary carbine. |
| Gnasher Siege Mortar / 105 | 52, 58, 99 | Indirect blast artillery, Pressure | Explicit setup action and minimum range are original balancing rules. Blast damages adjacent formations, including friends. Pressure on a hit instead of a destroyed model. |
| Plagueburst Mortar / 125 | 54, 58, 100 | Indirect chemical artillery, ignores cover, slows infantry | Setup/minimum range as above. No persistent ground cloud or secondary sprayer weapon; slow is applied to the primary infantry target. |
| Warrantank / 160 | 53, 58, 98 | Armoured cannon platform, patchwork durability | First incoming damage event per round loses two damage instead of rerolling one save. No secondary Ratling gun or blast on its cannon in this slice. |
| Rat Ogre Pack / 110 | 64, 97 | Durable assault brutes, weak implanted guns, strong charge | Charge adds fixed damage; no per-model Living Ram rolls. |
| Seraphyx Thirteen-Veil / 105 | 18–19, **111** | Named commander, venom needler, Watcher’s Dossier | Uses the **v1.1 annex** over p.90. One enemy commander begins with Pressure; manual target selection, attachment and one-time redeploy are omitted. Sabotage and commander eligibility are original game-role adaptations. |

## Faction systems

| Mechanic | Reference | Ashcrown implementation and rationale |
| --- | --- | --- |
| Breaking Number | 26–27, 76, 108, 134 | Three Pressure triggers cause Broken, clear tokens, and prevent another Break that round. Broken lasts two side-turn steps; reduces control by one and adds one penetration to Verminhorde ranged attacks. The book applies AP improvement more broadly; melee uses its base penetration here. |
| Trigger cap | 76, 134 | Once per **action category** per side turn: artillery/shooting, sabotage, magic, sensors, dossier. Ashcrown has no tabletop phases, so categories approximate the combined-arms intent. This is not identical to the codex’s phase cap. |
| Suppression | Original synthesis | Separate from Pressure. Fire, chemical attacks and sabotage add suppression; three or more reduces the victim’s next activation to one AP. No independent Battle-shock dice system. |
| Feast of Memory | 23, 77, 90 | Army pool capped at three, once-per-round harvest near a surviving Confessor. Spend on accuracy/control; no retrospective reroll, source-species record, or campaign False Memory system. |
| Salvage | 38, 55–56 | Enemy vehicle/artillery destruction grants an army token automatically; repair support consumes it. No physical wreck collection or Scrapgnaw selection required. |
| Tunnel exits | 39, 50, 84, 99 | Fixed friendly exit pairs; from round 2, eligible infantry may travel between them if not screened within two tiles. Costs AP and locks attacks. This replaces off-board reserve staging; no underground map. |
| Thundermaw | 35 | Artillery hits apply two suppression rather than one. This simplifies saturation fire; no critical-wound AP bonus or reroll. |
| Deepdelve | 39 | All Verminhorde infantry become tunnel-capable and travel costs one AP. Otherwise only Sappers/Blackclaws can travel, costing two. No extra reserve unit or deployment markers. |
| Chitterhold | 42 | Once per battle, manually return one destroyed Clawrat formation at half health in an unscreened deployment tile. Costs two of four initial Biomass and returns with no AP. The book instead schedules the return after the next Movement phase. |
| Objective mission | 130, loose inspiration | Three relay objectives, two VP per relay per full round, first to 24 or lead after 12 rounds. The book’s five markers, command-phase scoring and collapse bonuses are not implemented. |

## Original additions

Ashcrown’s world frame, name, Aurelian Compact, three maps, AP economy, command UI, projectile effects, sound, grid geometry, combat math, AI, save system, service records and all schematic artwork are newly authored. Aether Lance, Dread Pulse and Aegis Ward are **original magical powers**. Giving Dread-Confessors supernatural casting fulfils the science-fantasy direction; the reference describes political, chemical and psychological methods and does not grant these spells.

The source’s Red Birthing lore (pp.14–15) informs the vocabulary of Biomass, but this slice does not implement Verminia or a reproduction/campaign simulation. Chitterhold’s one replacement is the entire reinforcement system.

## Coverage limits

The book's other units (including Verminia, Varkrul, transports, aircraft, super-heavies, Juggernauts and most named characters), ten other clan packages, stratagems, enhancement framework, attachment matrix, and pp.104–106 campaign economy/scars are not implemented. Pages 109–135 were reviewed for superseding profiles, lists, missions and FAQ; only Seraphyx’s revised profile, Pressure clarifications and mission inspiration are used directly. Full-codex compatibility is not claimed.

## Native 0.10 addition — Gore-Maidens

The source names Slaughter-Maidens on pp.16–17. Gore-Maidens is an original Quiet Dread cadre requested by the user, adapted from that tradition; its 140-point stealth/assault profile, sabotage action and first-Trail-completion unlock are original game rules. Seraphyx (p.111) unlocks after two completions; Verminia's existing titan adaptation (pp.14–15,89) after ten. These native campaign rules supersede this legacy document's earlier non-implementation notes; see TRAIL-PROGRESSION.md and TITANS.md.

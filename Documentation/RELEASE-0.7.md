# The Quiet Dread 0.7 — miniatures and rules clarity

This increment upgrades the existing Unity game. It does not claim a finished commercial art set or tournament-tested faction balance.

## Miniatures

All 242 profiles use the upgraded renderer, including all seventeen titans. Infantry now have separately constructed boots, lower and upper limbs, elbow joints, hands, torso plates, belts, packs, exhausts and weapons. Faction anatomy includes rat muzzles, ears and tails, greenskin jaws, dwarf proportions and beards, skeletal faces, reptilian tails and demonic horns. Heavy armour, stealth cloaks, chemical tanks, engineer tools, caster staves, commander standards and flying equipment provide additional role cues. Cavalry uses organic mounts or mechanical bikes. Vehicles have track links, wheel hubs, turrets, gun barrels, vents, lamps and aerials. Titans retain their faction-specific chassis with added plating, rivets, vents or organic detail.

These are original code-built modular miniatures using shared parts. They are not 242 individually sculpted and rigged assets. Formation members remain decorative: individual models do not disappear with HP loss. Further bespoke silhouettes, animation, materials and creature sculpting remain worthwhile improvements.

Static parts are merged by shared material, with owned generated meshes disposed when a model is destroyed. Existing articulated titan limbs remain separate. Battle figures and portrait cards use the same construction. The main menu's Miniature Catalogue contains all profiles, faction filtering, pagination and a full-profile inspection panel.

## Rules foundation

The in-game Field Manual and NATIVE-RULES.md define the original formation-level system. Firearm attacks now roll an explicit integer d100 against their displayed threshold. Seeded outcomes survive campaign saving. The Combat Record exposes the roll, reroll, threshold and damage before wards. The existing fixed-damage, armour-penetration, magic, terrain and AP framework remains compatible with previous campaigns.

Rally costs one AP to remove up to two suppression and one Pressure. It never restores AP or consumes the attack. Suppression-free formations cannot waste AP rallying. The AI uses rally when it can improve an activation.

NativeProfiles generates descriptions of implemented profile mechanics. The catalogue, archives and titan cards no longer present legacy design proposals as current rules. Historical source text remains in Content.json and legacy documents for provenance. Faction-wide effects are explained in the rules reference.

The game intentionally retains automatic adjacent melee, fixed retaliation, no engagement lock and no overwatch. These are explicit system choices, not silently missing tabletop features. It uses a one-cell formation abstraction even for titans; model dimensions are decorative.

## AI

Target scoring considers expected damage, finishing attacks, commanders, objectives and blast friendly fire. Movement evaluation considers enemy ranges, line of sight, directional cover, wounded-unit exposure and melee threats while maintaining objective and artillery-range priorities. These are heuristics, not proof of strong tactical play or balance.

## Verification

The build runs 165 existing rules/campaign checks, 79 terrain/deployment/pathfinding checks, and 495 additional profile, native-description, rally, AI-evaluation and dice checks: 739 in total. The player smoke test renders all 242 profile cards over 31 catalogue pages, the detailed inspector, manual, all faction paintings and titans, campaign screens and three battlefield sizes. It also runs a complete AI battle and checks board picking at two window sizes. See the generated results and QA screenshots for evidence. Automated rendering does not replace extended human playtesting.

## Launch

Run `Build/Windows/The Quiet Dread.exe` with its adjacent data folder and Unity DLLs. Main menu: Miniature Catalogue or Field Manual. During battle: Log opens recent combat records; Rally acts immediately on the selected formation. Existing movement, terrain and campaign controls remain available.

# Native rules and faction identities

THE QUIET DREAD — FIELD MANUAL 0.11

An original formation-level tabletop system. Percentile attack rolls, fixed damage and a shared action economy make the displayed odds reproducible. These are native game rules, not official Warhammer, OPR or Verminhorde codex rules. The faction archives and codex mapping distinguish the inspirations and adaptations.

1. THE TABLE AND THE ARMY
In skirmish, bring 4–10 formations totalling at most 1,600 points. Include a commander; at most one titan. Unique profiles may appear once, ordinary profiles twice, Clawrats four times. Campaign recruitment has separate progression constraints.
Choose a 24×16, 32×22 or 40×28 battlefield. Choose your deployment edge; the enemy deploys opposite. A formation occupies one grid cell irrespective of the number or size of its decorative miniatures. Measure movement, range and objective distance orthogonally: |dx| + |dy|. Diagonals cost two. Models, banners and protruding gun barrels do not determine range or block shots.

2. ROUND SEQUENCE AND ACTION ECONOMY
The player takes the first army turn, then the enemy. At the beginning of each friendly turn, replenish Aether by two (maximum twelve), refresh formations to two AP, apply suppression and Pressure penalties, resolve faction recovery and automatic births. AP cannot be banked. You may switch between friendly formations while they have AP.
Move costs one AP and permits travel up to the profile's movement allowance. It may be used twice. Shooting or melee costs one AP; a formation can attack only once each friendly turn. Casting also uses that attack. Charge costs two AP and is restricted to profiles with Charge or Stampede. Most titan signatures cost two AP and use the attack. End Turn forfeits unused AP.

3. MOVEMENT AND TERRAIN
Ground formations cannot cross occupied cells, ruins, walls, rocks or buildings. Deep water permits only explicitly flying or amphibious profiles. Marsh costs two movement points, or one for flying/amphibious profiles. Woodland and craters cost two, or one for Verdant formations. Flying costs one per cell and may cross obstacles and units, but cannot finish on another formation or inside solid terrain. Slow reduces the movement allowance by two, minimum one; Rooted prevents movement.
Moving removes artillery bracing. Deployment is restricted to your marked zone and legal empty terrain. Larger tables preserve weapon ranges and movement allowances.

4. FIREARMS AND COVER
Check AP, the once-per-turn attack limit, range, minimum range, bracing and line of sight. Ruins, rocks and buildings block direct line of sight. Indirect weapons ignore line of sight. Adjacent enemies are fought in melee instead.
Roll d100 equal to or below the displayed hit chance. The software uses a seeded roll from 1 to 100; open the combat Log to inspect rolls and rerolls. Start with Accuracy, add two per veteran rank and fifteen for Orders, subtract five per suppression. Cover subtracts twenty for woodland, trenches or craters, or twenty-five when sheltering beside solid cover on the incoming side. Use only the strongest cover. Ignore Cover bypasses this modifier. Stealth beyond three cells or Veil subtracts twenty; a titan target adds ten; a marked target adds ten. Faction modifiers also apply. Final chance is bounded to 15–95%. Lumen and Vesper reroll one miss.
On a hit, damage = max(1, weapon Power + rank + faction bonuses − max(0, target Armour − Penetration)). Wards absorb damage before HP. The target normally gains one suppression, or two from a titan. Blast applies half the primary damage (minimum one) to other formations within its stated radius, including friends. A miss causes no blast. The target preview gives current hit chance, cover and primary damage before wards.

5. MELEE, CHARGES AND ARTILLERY
Adjacent melee automatically hits using Melee in place of Power. A surviving defender retaliates for half its printed Melee, reduced by remaining armour after penetration, minimum one. This reaction does not spend AP. A legal charge moves to an empty adjacent cell along a legal route, then attacks with +4 damage; Stampede instead deals 28. Charge retaliation uses the same armour and penetration calculation as ordinary melee. There is no facing bonus, engagement lock or free attack for moving away in this edition.
Artillery must Brace (one AP) before ranged fire. Bracing persists until movement. Minimum range still applies to bombardment. Artillery can defend itself in adjacent melee without bracing. Indirect fire is not automatically Ignore Cover.

6. SUPPRESSION, PRESSURE AND RALLY
At turn start, three suppression usually reduces the formation to one AP; Cinderjaw's threshold is two. Sepulchral formations ignore this AP penalty. Pelagic formations near a commander also ignore it. Suppression then decays by one. Dawn formations within three of a caster remove two suppression before checking the AP threshold. Three Pressure removes another AP and resets Pressure. Suppression still reduces firearm accuracy even when its AP penalty is ignored.
Rally costs one AP and removes up to two suppression and one Pressure from the acting formation. It cannot restore spent or lost AP, does not consume the attack and cannot be used when both counters are zero. A formation reduced to zero AP must wait for recovery or allied support.

7. COMMAND, MAGIC AND RESOURCES
Orders costs a commander one AP and targets a friendly formation within four cells: +15 accuracy and +1 objective control until that target's next turn. Repeating Orders on an already ordered formation is rejected without spending AP.
Casting costs one AP and the formation's attack, with a six-cell range. Offensive spells need line of sight. Lance costs three Aether and deals ten damage; Terror costs three, deals four and adds two suppression; Soulfire costs three, deals seven and heals its caster by five. Ward costs two (one for Dawn) and adds six ward HP. Heal costs three and restores eight HP, capped at maximum. Healing or repairing an undamaged target is rejected without spending resources. Spell damage bypasses armour but still meets wards and explicit damage-reduction rules.
Verminhorde Memory spends one Memory and one AP to mark an enemy within six and add Pressure. Repairs, tunnelling, resurrection and titan powers have profile-specific restrictions; consult faction rules and TITANS.md. Spawned troops have no AP on arrival and temporary broods do not become permanent campaign recruits. Six living temporary formations per side is the spawn cap.

8. OBJECTIVES AND VICTORY
After both armies have acted, compare each side's total Objective Control within one cell of each of three relays. Orders adds one control; Sauridian command support can add more. A strictly higher total earns two victory points; ties score nothing. Score all relays together. Reach thirty points, eliminate the enemy, or lead on points after round sixteen to win. Simultaneous score ties at the limit are a draw. Eliminating both sides is also a draw.

9. VETERANS, SAVES AND HONEST LIMITS
Campaign rank adds three maximum HP, one damage and two accuracy per rank. Recruitment, recovery, economy and permanent losses are described in the campaign documentation. Campaign saves include the battlefield and random state; older 18×12 saves retain their dimensions. Skirmishes are not saved.
Battle controls: Tab selects the next formation with AP; F selects Fire, M selects Move. Hover action buttons for requirements. Escape closes terrain and combat-record panels. Relay readouts show current control totals, not accumulated victory points.

The miniature catalogue shows the same original modular models used in battles. A model is a visual representation of a formation, not an individual casualty. There are no per-model wounds, facing arcs, scatter dice, overwatch or morale routs in this edition. The rules deliberately use one consistent formation abstraction. This is a playable prototype; matchup balance and model artistry still need further iteration.




COMMANDER LEGACIES (0.11)
Trail starts with an unlocked commander and two modest infantry formations. Recruit on the map or at an armory; grow to twenty formations, train survivors and manage gold. Conquest armies retain their ten-formation cap. The final Trail sentinel requires destroying its commander AND titan before round 25; relay points alone do not win. The original commander must survive.
Each commander earns an independent legacy usable by their armies in BOTH campaigns. Unit rewards arrive at 1, 3, 5 and 7 wins; titan access at 10. Verminhorde commanders earn Gore-Maidens first and the God-Queen tenth. At 2 wins, alternate commanders are discovered and can be recruited as retinue; each newly selected commander starts with zero personal wins.
Personal benefits: 4 wins grants +50 starting campaign gold, 6 reduces training prices by 10 gold, 8 adds 10% maximum HP to recovery actions, and 9 adds one XP to surviving formations after victories. Rewards never transfer to another commander. A Conquest army retains its founding command identity; raising a new army uses that new leader's own rewards. Recruitment costs and settlement requirements still apply. Skirmish remains unrestricted.
View commander rewards during setup or open Commander legacy during a campaign. Progress is separate from the active campaign save. Old faction progress migrates to its original starting commander because the old format did not store ownership. Existing armies remain intact. Damaged campaign/progression saves recover valid backups when possible and preserve damaged files; future-version saves are not overwritten.

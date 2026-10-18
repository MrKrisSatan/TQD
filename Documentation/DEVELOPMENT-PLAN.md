# The Quiet Dread — native Unity rebuild

The existing Ashcrown v0.3 is an offline JavaScript/Canvas game, not a Unity source project. SOVL's supplied archive contains reverse-engineering exports rather than buildable source. This project is a new Unity 6 Windows application with a C# simulation, procedural 3D battlefield, native menus and local saves. The old project remains available separately.

## Delivery order
1. Import the 169 existing profiles and faction histories as data; preserve provenance for the Verminhorde.
2. Add four original military cultures: Argent Testament (elite engineered soldiers), Oathscar Reclaimers (corrupted veteran legions), Pelagic Hunger (adaptive colonial organisms), and Kilnclaw Assemblage (industrial greenskin war clans). Each receives three commanders and a complete combined-arms roster.
3. Give all seventeen factions a unique titan with a readable silhouette, costly army slot, active signature ability and counterplay. Verminia continually births replaceable broods; the Vault walker trades mobility and accuracy for a regiment-killing siege shot.
4. Rebuild deployment, tactical turns, targeting, cover, line of sight, artillery, magic, objectives, AI, army selection and results in Unity. Use 1,600-point armies to accommodate one optional titan. Titans occupy one tactical cell but have much larger visual silhouettes; this is an explicit abstraction.
5. Rebuild the three-act death trail and territory Conquest with persistent casualties, recruitment, training, resources, rival expansion, sieges and saves. Preserve the earlier research separately; do not call new balance values SOVL rules.
6. Run native simulation checks and an actual Windows build, exercise the application, inspect captured visuals and package source and executable with instructions.

## Creative direction
The Quiet Dread is the growing absence of signals beyond the broken relay network: cities still receive power, but no longer receive answers. Armies fight for relays while colossi move in the fog. Charcoal stone, copper, ivory, coloured Aether and generous dark space anchor the visual identity. Three-dimensional formations, faction colours, readable silhouettes and gun/magic effects serve tactical clarity.

Warhammer 40,000 and Grimdark Future inform broad army archetypes, not copied characters, logos, unit names, stories or rules text. The new four factions are original additions. Existing codex-derived Verminhorde lore remains attributed to the supplied PDF; the God-Queen's tactical profile and all other titan rules are game adaptations.

## Sources consulted
- [Warhammer Space Marines introduction](https://www.warhammer-community.com/en-gb/articles/cDcDzjwu/starting-a-space-marine-army-in-warhammer-40000-everything-you-need-to-know-from-painting-to-lore/): durable engineered infantry and elite combined arms.
- [OPR Battle Brothers](https://www.onepagerules.com/factions/battle-brothers): elite engineered military forces.
- [OPR Havoc Brothers](https://www.onepagerules.com/factions/havoc-brothers): divergent renegade warbands and supernatural bargains.
- [OPR Alien Hives](https://www.onepagerules.com/factions/alien-hives): specialised organisms and collective societies.
- [OPR Orc Marauders](https://www.onepagerules.com/factions/orc-marauders): resource-poor clans and raiding economies.
- [Warhammer Chaos Space Marines introduction](https://www.warhammer-community.com/en-gb/articles/1C92Kwgj/starting-a-chaos-space-marines-army-in-warhammer-40000-everything-you-need-to-know-from-painting-to-lore/).
- [Warhammer Tyranids introduction](https://www.warhammer-community.com/en-gb/articles/QtNy2Xp2/starting-a-tyranids-army-in-warhammer-40000-everything-you-need-to-know-from-painting-to-lore/).
- [Warhammer Orks introduction](https://www.warhammer-community.com/en-gb/articles/lEuczqhi/starting-an-ork-army-in-warhammer-40000-everything-you-need-to-know-from-painting-to-lore/).
- The user's Verminhorde Dominion Definitive 11th Edition v1.1; existing page mapping and SOVL campaign research retained in Documentation/Legacy.

## Scope and representation
This is a playable native tactical prototype, with designed low-poly procedural miniatures and original generated title illustration. It is not a finished commercial game with individually sculpted and rigged models. Local single-player only. All balance values require more playtesting. Female-only factions remain female in their lore; abstract armoured miniatures do not depict sexualised anatomy.

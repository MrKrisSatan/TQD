# Commander legacies — version 0.11

Every one of the 51 commanders has a personal ten-victory Trail track. All rewards are original campaign adaptations; no new tabletop canon is claimed. Unit profiles and their source attribution remain in the faction archives. These tracks reward existing faction troops rather than adding untested new profiles.

## Ownership

Only the earning commander's armies may recruit their reward units or receive their benefits, in Trail or Conquest. Each Conquest army has a saved founding commander identity. Recruiting another commander as a lieutenant does not transfer ownership; raising a separate expedition gives it the new commander's own track. There is no army-transfer or commander-swap mechanic. Conquest formation casualties do not change the army's command identity. Existing active campaign rosters are retained for compatibility, but subsequent recruitment observes ownership. Skirmish and the catalogue remain unrestricted.

A second Trail victory discovers alternate commanders for faction selection and unlocks their recruitment as the earning commander's retinue. Discovered commanders start at zero personal victories; discovery transfers neither troop rewards nor campaign benefits. This distinction avoids requiring an undiscovered commander to win a campaign before they can be selected.

## Ten milestones

| Wins by this commander | Personal reward |
|---|---|
| 1 | First elite formation; all Verminhorde commanders earn Gore-Maidens here |
| 2 | Command retinue; alternate commanders become selectable with independent tracks |
| 3 | Second specialist formation |
| 4 | Supply warrant: +50 gold when this commander starts a new campaign |
| 5 | Third specialist formation |
| 6 | Drill charter: this army's training costs 10 less gold |
| 7 | Fourth formation |
| 8 | Field surgeons: chosen recovery actions restore an additional 10% maximum HP |
| 9 | Veteran command: survivors in this army gain +1 XP on tactical victory |
| 10 | Faction titan; Verminhorde earns the God-Queen |

Benefits are checked against the army's commander, never a faction-wide total. Units still cost gold and obey normal duplicate, titan, barracks, sanctum and settlement-tier requirements. Supply warrants do not pay again on reload or when raising another army. Recovery bonuses do not multiply Conquest's automatic end-turn healing. The three-formation Trail start remains intact.

Open **View commander rewards** during campaign setup, or **Commander legacy** from a campaign. The list shows its owner and each milestone's earned/locked state. Conquest's Raise controls now cycle through discovered commanders.

## Unit reward tracks

The manifest in Assets/Resources/CommanderRewards.json fixes profile IDs and milestone numbers so price balancing cannot silently move rewards. Commanders within a faction earn the same reserved unit set in different orders; their earned access remains separate.

### Verminhorde Dominion

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| War-Marshal | Gore-Maidens | Stormvermin Cohort | Plagueburst Mortar | Warrantank | Verminia, the Walking Womb-Palace |
| Seraphyx Thirteen-Veil | Gore-Maidens | Plagueburst Mortar | Warrantank | Stormvermin Cohort | Verminia, the Walking Womb-Palace |

### Aurelian Compact

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| Sunward Marshal | Helion Railcrawler | Cuirassier Exolancers | Sunfall Howitzer | Dawnsteel Lancers | Concord Atlas |
| First Marshal Cassian Rook | Cuirassier Exolancers | Sunfall Howitzer | Dawnsteel Lancers | Helion Railcrawler | Concord Atlas |
| General Maerin Holt | Sunfall Howitzer | Dawnsteel Lancers | Helion Railcrawler | Cuirassier Exolancers | Concord Atlas |
| Warden-General Selka Marr | Dawnsteel Lancers | Helion Railcrawler | Cuirassier Exolancers | Sunfall Howitzer | Concord Atlas |

### Cinderjaw Freeholds

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| Gorvakka Chainbreaker | Roadsplitter Wagons | Ironbelly Breakers | Rivetbreak Veterans | Thunderpot Battery | Worldbreaker Gromm |
| Mikka Nine-Sparks | Ironbelly Breakers | Rivetbreak Veterans | Thunderpot Battery | Roadsplitter Wagons | Worldbreaker Gromm |
| Urzha Smoke-Crowned | Rivetbreak Veterans | Thunderpot Battery | Roadsplitter Wagons | Ironbelly Breakers | Worldbreaker Gromm |

### Kharad Vault Concord

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| High Castellan Dorrik Vane | Bastion Crawler | Boreframe Sentinels | Basalt Hearthguard | Grav Anvil Battery | Graven Mountain Siege Engine |
| Maela Flintledger | Boreframe Sentinels | Basalt Hearthguard | Grav Anvil Battery | Bastion Crawler | Graven Mountain Siege Engine |
| Orrun Deepmeasure | Basalt Hearthguard | Grav Anvil Battery | Bastion Crawler | Boreframe Sentinels | Graven Mountain Siege Engine |

### Lumen Arc Principalities

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| Strategarch Aelith Vaer | Glasswake Spear | Dawnthread Blades | Arcwind Cavaliers | Starfall Array | Astral Crown Seraph |
| Ilyr Sen of the Last Interval | Dawnthread Blades | Arcwind Cavaliers | Starfall Array | Glasswake Spear | Astral Crown Seraph |
| Admiral Thessa Quill | Arcwind Cavaliers | Starfall Array | Glasswake Spear | Dawnthread Blades | Astral Crown Seraph |

### Vesperglass Cabals

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| Archoness Nysera Vhal | Widowglass Ravager | Sable Reavers | Debtguard Phalanx | Silence Organ | Nightglass Leviathan |
| Vael Corven, the Quiet Debt | Sable Reavers | Debtguard Phalanx | Silence Organ | Widowglass Ravager | Nightglass Leviathan |
| Seer-Matron Eshara Vey | Debtguard Phalanx | Silence Organ | Widowglass Ravager | Sable Reavers | Nightglass Leviathan |

### Sepulchral Continuum

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| First Executor Nemea Sorn | Ossuary Engine | Sarcophagus Guard | Dread Executors | Dirge Battery | Ossuary of a Million Names |
| Grave-Marshal Othric Vale | Sarcophagus Guard | Dread Executors | Dirge Battery | Ossuary Engine | Ossuary of a Million Names |
| High Notary Iseth Marr | Dread Executors | Dirge Battery | Ossuary Engine | Sarcophagus Guard | Ossuary of a Million Names |

### Hornbound Exodus

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| Road-Mother Ygra Manypaths | Ironhorn Minotaurs | Stormcart Chariots | Breachhorn Veterans | Centaur Dragoons | Pilgrim of the Broken Moon |
| Tarek Stormhoof | Stormcart Chariots | Breachhorn Veterans | Centaur Dragoons | Ironhorn Minotaurs | Pilgrim of the Broken Moon |
| Bhorun Gatebreaker | Breachhorn Veterans | Centaur Dragoons | Ironhorn Minotaurs | Stormcart Chariots | Pilgrim of the Broken Moon |

### Riftbound Ascendancy

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| High Jarl Korr Veydr | Pact Furnace | Abyssforged Knights | Mutation Thralls | Breachmouth Battery | The Unfinished Sovereign |
| Myrka of the Ninth Wound | Abyssforged Knights | Mutation Thralls | Breachmouth Battery | Pact Furnace | The Unfinished Sovereign |
| Tharoz the Unfinished | Mutation Thralls | Breachmouth Battery | Pact Furnace | Abyssforged Knights | The Unfinished Sovereign |

### Verdant Synod

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| Speaker Naeva Rootsong | Groveheart Colossus | Barkmantle Guardians | Thornbound Wardens | Sporefall Battery | Worldroot Elder |
| Elder Thalen of Seven Springs | Barkmantle Guardians | Thornbound Wardens | Sporefall Battery | Groveheart Colossus | Worldroot Elder |
| Hunt-Marshal Sira Leafglass | Thornbound Wardens | Sporefall Battery | Groveheart Colossus | Barkmantle Guardians | Worldroot Elder |

### Sauridian Helion Dynasty

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| Sun-Regent Xalatek | Solarback Behemoth | Raptor Lancers | Obsidian Shellguard | Comet Mortar | Helion Worldback |
| Crest-Marshal Issara Venn | Raptor Lancers | Obsidian Shellguard | Comet Mortar | Solarback Behemoth | Helion Worldback |
| Star-Reader Oq-Telan | Obsidian Shellguard | Comet Mortar | Solarback Behemoth | Raptor Lancers | Helion Worldback |

### Daughters of the Unbroken Dawn

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| High Matriarch Elyra Solenne | Sanctuary Engine | Halo Bastion Sisters | Vigilant Sword-Sisters | Dawn Choir Battery | Reliquary of the First Light |
| Marshal Serene Valecourt | Halo Bastion Sisters | Vigilant Sword-Sisters | Dawn Choir Battery | Sanctuary Engine | Reliquary of the First Light |
| Saint-Errant Iona Vey | Vigilant Sword-Sisters | Dawn Choir Battery | Sanctuary Engine | Halo Bastion Sisters | Reliquary of the First Light |

### Convent of the Hollow Star

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| Abbess-Maledict Vaelora Nox | Cathedral of Want | Black Reliquary Sisters | Daughters of the Thorned Halo | Lamentation Organ | The Bridal Eclipse |
| War-Prioress Sabeth Vane | Black Reliquary Sisters | Daughters of the Thorned Halo | Lamentation Organ | Cathedral of Want | The Bridal Eclipse |
| Morwenna, the Unsainted | Daughters of the Thorned Halo | Lamentation Organ | Cathedral of Want | Black Reliquary Sisters | The Bridal Eclipse |

### Argent Testament

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| First Listener Cael Orison | Palladium Carrier | Rescue Exoframes | Covenant Bulwarks | Long Vigil Battery | The Last Witness |
| Breach-Marshal Iona Vey | Rescue Exoframes | Covenant Bulwarks | Long Vigil Battery | Palladium Carrier | The Last Witness |
| Choirwright Sen Aravel | Covenant Bulwarks | Long Vigil Battery | Palladium Carrier | Rescue Exoframes | The Last Witness |

### Oathscar Reclaimers

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| Executor Mael Rusk | Iron Indenture | Bound Penitents | Debtplate Wardens | Ledger Bombard | Covenant Eater |
| Lady Nerezza Vow-Eater | Bound Penitents | Debtplate Wardens | Ledger Bombard | Iron Indenture | Covenant Eater |
| Notary Ossian Null | Debtplate Wardens | Ledger Bombard | Iron Indenture | Bound Penitents | Covenant Eater |

### Pelagic Hunger

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| First Current Sere | Shellback Carrier | Breakwater Brutes | Reefguard Cysts | Pressure-Spitter | Abyssal Nursery |
| The Ninth Appetite | Breakwater Brutes | Reefguard Cysts | Pressure-Spitter | Shellback Carrier | Abyssal Nursery |
| Lattice-Mother Oru | Reefguard Cysts | Pressure-Spitter | Shellback Carrier | Breakwater Brutes | Abyssal Nursery |

### Kilnclaw Assemblage

| Commander | 1 win | 3 wins | 5 wins | 7 wins | 10 wins |
|---|---|---|---|---|---|
| High Foreman Brakka Nine-Boilers | Scrap Hauler | Piston Brutes | Boilerplate Guard | Furnace Lobber | Marching Kiln |
| Ram-Queen Uzza Rivetmaw | Piston Brutes | Boilerplate Guard | Furnace Lobber | Scrap Hauler | Marching Kiln |
| Gauge-Sage Tikk Sootglass | Boilerplate Guard | Furnace Lobber | Scrap Hauler | Piston Brutes | Marching Kiln |

## Migration and recovery

Version 0.10 saved faction totals and run IDs but not the commander who earned each victory. Those counts and previously unlocked profile IDs migrate to the faction's original starting commander. Other commanders start fresh. A migration notice explains the assignment. Existing campaign armies keep their current formations; all new recruitment is checked against their recorded owner. Completed run IDs remain intact, preventing duplicate credit.

Campaign and progression files live in the existing LocalLow game folder. Both use atomic replacement and a .bak backup. If the primary is damaged or missing, Continue Expedition loads a valid backup and reports recovery. A damaged primary is retained as .damaged-TIMESTAMP when rewritten; the valid backup is not replaced with corrupt data. Unknown future versions stop loading/writing rather than silently reverting. If both copies are invalid, they remain intact and the UI explains the failure.

Limits: no historical ownership can be inferred for overwritten old runs; there is no save-slot browser or manual ownership reassignment. Automated tests simulate interrupted-write file states, not physical power loss. Completion rewards need human long-term economy testing; they are deliberately modest.

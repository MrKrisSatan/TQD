# Battlefield terrain and deployment — 0.6.0

The two SOVL screenshots inform the wider battlefield viewport, compact side panels, deployment presentation and terrain clusters. These are original native Unity visuals and mechanics, not copied SOVL assets.

Before each new skirmish or campaign battle, choose Small (24×16), Medium (32×22) or Large (40×28), woodland / wetlands / ruined city, and the player edge: west, east, north or south. The opponent deploys across the field. All four edges are supported. The reference images have no stated world scale, so the smallest format uses their approximate 3:2 proportions; the larger formats increase playable area while retaining unit movement and weapon ranges. Existing in-progress battles retain their saved dimensions and deployment.

Terrain rules:
- Woodland: enterable, 20-point ranged accuracy cover penalty; costs 2 movement, or 1 for Verdant and flying units.
- Trenches: 20-point cover penalty, normal movement.
- Craters: 20-point cover penalty, movement cost 2 (Verdant and flying units pay 1).
- Marsh: movement cost 2, or 1 for amphibious/flying units.
- Deep water: impassable to all other units. Amphibious and flying units pay 1.
- Low walls: cannot be occupied; nearby formations gain 25-point directional cover. Shots can pass over them.
- Ruins, rock outcrops and intact buildings: cannot be occupied, block sight, and grant nearby formations 25-point directional cover around their edges. Fully obstructed shots are blocked rather than merely penalized.
- Flying units cross occupied cells and solid obstacles, but cannot finish inside solid terrain or another formation.
- Cover penalties do not stack; cover-ignoring weapons bypass them. Melee does not roll ranged accuracy.

Deployment zones and crossing roads are cleared so all ordinary infantry can reach the objectives. The overlaid deployment tint, hover text and Terrain legend explain the actual rules. Solid scenery is static, not destructible. Trees grant cover but do not block sight as buildings do. Terrain is tile-based despite the continuous ground texture.

Movement traits are explicit gameplay adaptations, not claims of codex canon:
- Flying: Arcwind Cavaliers, Sable Reavers, Aurora Lancers, Copperwing Lancers, Echo Skimmers and the Lumen titan.
- Amphibious: the Pelagic Hunger roster, plus Sauridian profiles saurian_2 and saurian_9.
- All remaining profiles use ground movement. The inspector shows the selected formation's movement class; the content JSON stores both boolean traits.

Spawning, brood restoration, resurrection and teleport destinations also obey terrain legality. Burrowing cannot end in deep water. AI uses the same weighted movement search as the player. Large battle dimensions, environment and deployment edge serialize inside the existing battle save.

Validation: TERRAIN-TEST-RESULTS.txt covers 79 new checks, alongside the 165 existing rules checks. The player QA captures all three new battlefield sizes, setup choices, deployment orientations, terrain legend and resized raycasts.

Remaining presentation limits: schematic procedural miniatures, static terrain, one-cell logical titan footprints, and no production animation or destruction system. Faction artwork from 0.5.0 remains available.

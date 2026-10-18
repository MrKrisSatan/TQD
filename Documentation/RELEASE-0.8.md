# The Quiet Dread 0.8 — battle clarity and support AI

The existing native Windows game now exposes action readiness and formation status more clearly and gives support profiles useful AI decisions.

- Action buttons disable when the selected formation lacks AP, has spent its attack, cannot move because of roots, lacks spell resources, has already braced, or has nothing to rally. Hovering a button explains the restriction or targeting instructions. The rules engine uses the same readiness checks.
- Selected-unit information includes wards, suppression, Pressure, roots, slow, Veil, Orders, bracing and whether the attack is spent.
- Three relay readouts show live friendly and enemy control totals. These are distinct from accumulated victory points.
- Tab cycles through friendly formations with remaining AP (all formations during deployment); F selects Fire and M selects Move. Escape closes the terrain or combat-record panel. Informational overlay areas do not pass clicks through to the battlefield.
- AI engineers repair injured vehicles and titans. AI casters heal critically injured allies, prioritizing commanders when appropriate. AI commanders issue orders to unordered allies with a viable ranged attack. Target evaluation now includes the actual Lumen/Vesper miss-reroll probability.
- Charge retaliation and ordinary melee retaliation now share the same penetration calculation.
- Siphon healing uses actual HP removed, so wards cannot become a source of healing.
- Repeating Orders, bracing again, repairing an undamaged engine or healing a full-health target no longer wastes AP or resources.

No new faction, model set or campaign mode is claimed in this increment. The upgraded 242-profile modular model library from 0.7 remains included. Action readiness covers target-independent restrictions; a selected target can still be invalid because of range, terrain or ability-specific requirements.

## Validation

The build runs the prior 739 checks plus 15 new support-AI, action-readiness, resource-preservation and ward/drain interaction checks: 754 total. Existing complete AI battles, campaign tests and terrain tests still run. The player smoke test captures the new battle HUD and combat record alongside the full model catalogue, campaign screens and field manual. Generated results are in TEST-RESULTS.txt, TERRAIN-TEST-RESULTS.txt, COHERENCE-TEST-RESULTS.txt, BATTLE-POLISH-TEST-RESULTS.txt and QA/runtime-smoke.txt in the source package.

This remains a prototype. Automated checks establish these interactions and successful rendering; they do not establish tournament balance or exhaustive manual UI coverage.

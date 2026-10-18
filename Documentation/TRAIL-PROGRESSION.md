# Trail of Death — progression and campaign rules (0.10)

Start a new Trail from Single Player → Trail of Death → Choose commander. Each faction begins with exactly three formations: the selected unlocked commander and its two cheapest eligible infantry profiles. The muster is not a way to import a titan or elite army into a fresh run. The preview shows the actual starting profiles. Conquest retains its six-formation start plus a selected unlocked titan; skirmish is unrestricted.

## The run

- Three acts, five encounters per chosen route in each act. Each act ends at a sentinel. Route event types and enemy factions use the campaign's saved random seed. The graph remains a readable three-lane network, not an arbitrary map topology.
- Start with 160 gold. Battle victory grants 70 gold, elite victory 110, sentinel victory 180; choose another 60 gold or recovery afterward. Camps and events provide further supplies or healing.
- Recruit on the route map or at an armory. Recruitment costs the displayed profile price. A Trail army may grow to **20 permanent formations**; duplicate limits, unique formations and the one-titan limit still apply. Army growth is bought with earned resources, not guaranteed free recruits. Conquest retains its 10-formation limit.
- Survivors gain experience. Training costs 50 + 35 × current rank gold and 2 + 2 × current rank XP, up to rank three. Ranks add health. Destroyed permanent formations stay dead. Losing the original commander ends the run even if the battle was won.
- New acts fully heal survivors. Temporary summoned broods do not become permanent recruits.
- Ordinary encounters use the standard relay/elimination rules. **The final sentinel is a boss hunt:** destroy the enemy commander and titan before the end of round 24, while keeping the original commander alive. Relay score cannot bypass these targets. Other enemy formations may survive the successful hunt.
- Opening opposition has two formations. Later encounters increase by act, row and elite/sentinel status. Only the third sentinel is guaranteed titan support. This is an initial balance pass, not a claim of equal difficulty across all factions.

## Permanent rewards

Successful completion counts are separate for each faction and are shared by both campaign modes. Only a finished, victorious third act awards a completion; losses, abandonment, individual battles and repeated loading do not.

| Verminhorde completions | Reward |
|---|---|
| 1 | Gore-Maidens: an elite stealth/assault cadre with sabotage, mono-blades and venom needlers |
| 2 | Seraphyx Thirteen-Veil as an additional commander |
| 10 | Verminia, the Walking Womb-Palace — the God-Queen's playable titan manifestation |

For other factions, completion one unlocks an elite unit, completion two unlocks the alternate commanders, and completion ten unlocks the faction titan. The commander screen lists the faction's actual rewards. Earned units become **available**, not free: recruitment prices, barracks/sanctum/tier requirements in Conquest, duplicates and titan limits still apply. An unlocked commander may lead either campaign; titans can be recruited during a Trail but do not join its modest starting warband.

Skirmish and the Miniature Catalogue remain unrestricted so all content can be tried without campaign grinding.

## Saves and compatibility

Progress lives in `%USERPROFILE%\AppData\LocalLow\Quiet Dread Workshop\The Quiet Dread\trail-progression.json`, with a `.bak` backup. This is separate from `campaign.json`. Atomic replacement, recorded run IDs and saved unlocked profile IDs protect rewards from duplicate settlement and preserve earned unlocks. Back up both files when moving computers.

Existing active campaigns keep their armies, routes and losses. Units already present are not removed; new recruitment follows the progression rules. An existing completed third-act Trail can register one completion when loaded, using its saved run ID. Older completed runs that were overwritten cannot be reconstructed. No historical completions are fabricated. If both progression files are unreadable, new campaign starts are blocked and the files are preserved for recovery.

The local narrator receives the completion/reward decision through the campaign chronicle. It can describe that recorded outcome but cannot award unlocks or alter completion counts.

## Source and limitations

The codex calls its operatives **Slaughter-Maidens** (pp.16–17). “Gore-Maidens” is the requested original game cadre, inspired by their infiltration training, enhancements and Mother's Fang mono-blades. Its statistics, sabotage behaviour and unlock condition are adaptations. Seraphyx uses the existing codex-based profile (v1.1 annex p.111). The God-Queen's titan form and brood mechanic remain the documented original adaptation of Verminia (pp.14–15,89).

Models remain modular; Gore-Maidens use the Verminhorde stealth infantry silhouette, blades and cloaks. The run expands to twenty formations, not unlimited armies. Larger deployment zones pack units into multiple distinct rows. Economy, boss counterplay and long-term completion pacing still need human playtesting across all factions and difficulties.

## Superseded progression rules in 0.11

The faction-wide unlock section above describes version 0.10. Version 0.11 uses commander-owned tracks, additional milestones and separate Conquest army ownership. See COMMANDER-REWARDS.md for current rules and migration; faction totals no longer authorize reward access.

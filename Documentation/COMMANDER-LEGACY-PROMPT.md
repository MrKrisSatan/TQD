# Implementation prompt — Commander legacies

Continue developing The Quiet Dread in its existing Unity project. Expand Trail of Death progression into persistent, commander-specific reward tracks for every faction, then complete the next practical development task from Documentation/TODO.md.

Give every playable commander a clearly displayed track spanning ten successful Trail completions. Add meaningful rewards between existing milestones, including elite troops, specialists, war engines and modest campaign benefits. Match rewards to faction identity and commander roles; distinguish original adaptations from codex canon. Preserve Verminhorde's first-completion Gore-Maidens and tenth-completion God-Queen milestones.

Track victories and earned rewards by stable commander profile ID. Only armies led by that commander may recruit or benefit from those rewards, in either Trail or Conquest. A different commander in the same faction must earn their own rewards. In Conquest, enforce ownership separately for each army, including newly raised armies. Prevent alternate recruitment, army setup, loading and commander selection from bypassing restrictions. Unlocking a new commander permits selecting that character, but does not transfer the unlocking commander's unit rewards or completion count.

Keep Trail's modest three-formation start, procedural journey, resource management, persistent casualties, army growth and final boss. Benefits should support progression without eliminating these constraints. Display each reward's owner, completion requirement, effect and earned/locked state. Keep skirmish unrestricted.

Migrate existing faction-wide progress transparently. Use recorded commander evidence where available; where ownership cannot be recovered, assign legacy rewards to the faction's original starting commander and explain that rule. Preserve existing campaign armies, back up saves, and prevent repeated settlement or loading from granting duplicate rewards. Include reward outcomes in the authoritative narrative ledger.

Test commander isolation, all faction reward tracks, Conquest army ownership, duplicate completions, old-save migration, persistence, locked recruitment and campaign benefits. Build a runnable Windows release, update the manual, To-Do list and release notes, and report remaining limitations honestly.

Then continue the To-Do list with a bounded, useful increment: robust campaign save recovery. Detect damaged saves, recover valid backups without destroying them, reject unsupported future versions, and tell the player whether recovery succeeded. Test failure and recovery paths. Do not stop after writing the prompt or plan.

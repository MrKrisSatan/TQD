# The Quiet Dread 0.11 — Commander legacies

All 51 commanders now have separate ten-victory Trail tracks. Unit rewards arrive at 1, 3, 5 and 7 victories, followed by a titan at 10. Other milestones grant command retinues, a modest starting supply bonus, cheaper training, improved recovery and extra victory experience. Verminhorde retains Gore-Maidens first and the God-Queen tenth.

Only the earning commander's armies may use newly recruited rewards and campaign benefits. Each Conquest army records its founding commander; newly discovered commanders start at zero personal victories. Existing active rosters are preserved. Old faction-wide progress is assigned to its original starting commander because 0.10 did not record historical ownership.

The To-Do save-recovery increment is also implemented: validated campaign loading, automatic backup recovery, atomic writes, preserved damaged primaries, protected backups and rejection of unsupported future versions. Continue Expedition works when only the backup remains.

Use View commander rewards during setup and Commander legacy in a campaign. Conquest Raise controls now select a discovered commander. See COMMANDER-REWARDS.md for all reward tables and migration rules. The improved implementation prompt is COMMANDER-LEGACY-PROMPT.md.

Validation: 2,942 automated checks, including 1,810 ownership/milestone/recovery checks and all existing campaign, terrain, rules and narration tests. Runtime smoke evidence and screenshots are in QA. Milestone tests simulate victories; they do not establish long-term balance. No new unit sculpts or AI-generated prose trials are claimed in this increment. Human campaign playtesting and the visual benchmark battle remain on the To-Do list.

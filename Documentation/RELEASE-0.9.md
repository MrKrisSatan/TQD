# The Quiet Dread 0.9 — local campaign chronicle

Adds a maintained development To-Do list and an optional Ollama narrator for Trail of Death and Conquest. The default remains authored narration with no service dependency.

Both modes record introductions, battle results, campaign changes and endings in a saved journal. Battle entries retain starting forces, destroyed and surviving formations, remaining HP, kills, XP/rank, titans and temporary summons. Strategic entries record decisions, recovery, recruitment, movement, ownership and resource changes. Narrative text is presentation only.

Settings include a loopback endpoint, installed local-model discovery/selection, tone and length. The Chronicle exposes authoritative facts separately from optional prose, with skip, retry and authored-text controls. Requests are asynchronous, bounded and cancellable. Stable event IDs, save migration, a capped journal and stale-response guards support continuity. In-progress battles are preserved when a narrator-triggered save occurs from a menu.

No models were downloaded. The installed Mistral Nemo model was used for real local integration tests. Cloud-backed model entries are excluded.

See LOCAL-NARRATOR.md for setup, privacy, limits and troubleshooting; TODO.md for the remaining development work. The generated prose can still need editorial review: validation enforces structural facts and rejects selected unsupported claims but does not guarantee perfect semantic consistency. The journal's themes are constrained motifs, not a dynamic quest or diplomacy system. Existing gameplay, modular models and campaigns remain prototypes.

# SOVL reference inspection

## Scope and conclusion

Read-only inspection of `C:\Users\red00\OneDrive\Documents\Playground\SOVL-26008461.rar` and the already extracted sibling `SOVL-26008461` directory. No original executable was run, no original files were changed, and no network claims were used. The archive is a **REA/Ghidra native decompilation export**, not a runnable game or an editable Unity project. Build the requested original game as a new implementation; do not describe this export as recovered original source.

## Observed evidence

- Full `tar -tf` member inventory: 91,232 `.c` pseudocode files, 11,412 `.json` evidence/status files, three `.log` files, one `.mjs` exporter, one `.bak`, one `.txt`, and three extensionless directory entries. No `.exe`, `.dll`, `.unity`, `.cs`, `.prefab`, or `.png` members were present in that inventory. This is an inventory finding, not a claim that every possible asset encoding was investigated.
- `target.json` identifies the historical target as `C:\Program Files (x86)\Steam\steamapps\common\SOVL\GameAssembly.dll`, a PE x86_64 executable-kind DLL with SHA-256 `d62e3109448573f34ac08132a9dad702e7ccb67641ceeef66cedcd6abc1d0fd8`. The target path is historical evidence; this inspection did not open that installation.
- `procedures.evidence.json`, evidence ID `ev_a967aaa289df2b6f699681de18f9737bd81a64ddda9f67624567866a9b6d7dab`, records the procedure inventory. `export-validation.json` reports 91,235 eligible/processed procedures, 91,232 successes, three failures, and 257 named procedures. Its named samples include `il2cpp_class_is_valuetype`, `il2cpp_object_unbox`, and `il2cpp_gc_has_strict_wbarriers`.
- `evidence/batch-000000.json`, ID `ev_0a479c13fdd51183b3363922b9f48000ca93f91be116b11d38d44d1e0ba3cd2c`, operation `batch_decompile`, marks confidence `derived` and authority `shipped-artifact`.
- `pseudocode/0x180001020.c` explicitly labels itself reconstructed pseudocode, not original source. Its body has an anonymous function name, unresolved data/function identifiers, and a `TlsAlloc` call: runtime plumbing, not evidence of a tactical rule. A research-use comment referring to another project was treated as document content, not an instruction for this task.
- `export-validation.json` preserves the limitations that original names/types may be missing and PDB load state is unknown. The cited PDB is GameAssembly.pdb, GUID d3338ada-f249-4451-8b0d-f5c039007316, age hex 19. Successful pseudocode output is not proof of source-level correctness or complete gameplay reconstruction.

## Inferences

IL2CPP exports and the GameAssembly name strongly support a Unity IL2CPP native build as the original technical platform. The export itself does not provide Unity scenes, a project manifest, editable C# gameplay source, or a ready build. Porting the pseudocode directly is not a practical foundation for the requested playable vertical slice.

## Gameplay, controls, and progression: unresolved

A bounded case-insensitive search across all supplied pseudocode and batch evidence for `morale`, `deployment`, `rangedattack`, `campaign`, `chargeattack`, `battlecontroller`, and `unitcontroller` returned no matches. This does **not** prove those systems are absent: anonymous native functions and missing metadata make negative name searches weak evidence.

The supplied export does not establish, in this inspection, exact turn/phase order, movement rules, attack or morale formulae, army-building limits, keyboard/mouse bindings, campaign structure, progression rewards, roster balance, or AI decisions. No screenshots, recorded play sessions, or runtime observations were inspected. Any new grid, action-point budget, targeting rules, suppression mechanics, controls, or progression must therefore be labeled as original adaptation choices, not verified SOVL behaviour.

## Recommended implementation boundary

Use this material to establish technical constraints and provenance. Create an independently authored science-fantasy tactical game under an original name, driven by the supplied Verminhorde codex and explicit design decisions. A browser Canvas/JavaScript implementation is a practical self-contained vertical-slice option when no Unity source project is supplied; engine choice is a new development decision. Do not claim full SOVL feature parity or source reconstruction. Further fidelity would require usable metadata/assets or direct gameplay reference and a separate, focused analysis.

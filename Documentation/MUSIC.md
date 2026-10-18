# Music integration — 0.12

The five user-supplied MP3 files from `C:\Users\red00\OneDrive\Documents\Playground\music` are copied into the Unity project. The game includes their imported audio; the source music folder does not need to be present when running the Windows build. No source file is edited.

| Game context | Source file | Unity asset |
|---|---|---|
| Main menu, setup and archives | everything_is_dead-cinematic-dark-ambient-503450.mp3 | Music/menu.mp3 |
| Trail of Death campaign and results | everything_is_dead-dark-ambient-493699.mp3 | Music/trail.mp3 |
| Conquest campaign and results | horror_house_music-dark-ambient-soundscape-575774.mp3 | Music/conquest.mp3 |
| Battles / battlefield setup — first track | mfcc-war-war-epic-music-560582.mp3 | Music/battle-epic.mp3 |
| Battles — second track | the_mountain-trailer-war-162222.mp3 | Music/battle-trailer.mp3 |

Battle tracks alternate continuously. Ambient tracks repeat with a short crossfade. Changes between contexts use a two-second crossfade and at most two music sources. Moving between panels in the same context does not restart the music. Opening settings or a campaign journal keeps the current soundtrack. Battle results return to the relevant campaign ambience; skirmish results return to menu ambience.

## Controls

Settings now has separate **Effects volume** and **Music volume** sliders plus **Music: ON/OFF**. Use **Save & return** to persist preferences. Music starts enabled at 45% for a new installation. Existing effects-volume preferences are retained. Music bypasses the effects listener-volume control, so reducing effects to zero does not silence the soundtrack. Turning music off fades and stops both music sources.

## Implementation and provenance

Unity imports the clips as streaming Vorbis audio at quality 0.7, retaining stereo where present, with background loading. Long tracks are not decompressed completely into RAM. The controller is local and has no network, streaming-service or Ollama dependency.

These are user-provided tracks; filenames are retained in this mapping. No new authorship or licence claim is made. The copied MP3 source hashes are recorded in MUSIC-SOURCE-HASHES.txt. Imported player audio is transcoded by Unity, so its packaged bytes will differ from the MP3 sources.

Validation is recorded in MUSIC-TEST-RESULTS.txt and QA/music-runtime.txt. Runtime checks exercise all five decoded signals, transitions, same-context continuity, playlist advancement, separate effects/music control and disabling/re-enabling. They verify playback and digital signal output, not subjective mastering or audibility through the user's chosen speakers/headphones.

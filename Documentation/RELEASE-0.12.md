# The Quiet Dread 0.12 — soundtrack

All five supplied music tracks are integrated into the native Windows build. Menus, Trail and Conquest use separate ambient tracks; battles alternate the two war tracks. Playback uses streaming audio and two-second crossfades, with continuous playlists and no restart when switching panels within the same context.

Settings has independent music and effects sliders plus a persistent music toggle. The original music files are unchanged. See MUSIC.md for the track mapping and controls.

Validation: 2,959 editor checks, including 17 new audio import/routing checks. Dedicated player smoke tests cover actual streamed playback and decoded output, crossfades, battle playlist advancement, music mute/resume and independence from muted effects. No gameplay rules changed in this increment.

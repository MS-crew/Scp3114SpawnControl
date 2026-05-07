<h1 align="center">Scp 3114 Spawn Control</h1>
<div align="center">
<a href="https://github.com/MS-crew/Scp3114SpawnControl/releases"><img src="https://img.shields.io/github/downloads/MS-crew/Scp3114SpawnControl/total?style=for-the-badge&logo=githubactions&label=Downloads" href="https://github.com/MS-crew/Scp3114SpawnControl/releases" alt="GitHub Release Download"></a>
<a href="https://github.com/MS-crew/Scp3114SpawnControl/releases"><img src="https://img.shields.io/badge/Build-1.4.0-brightgreen?style=for-the-badge&logo=gitbook" href="https://github.com/MS-crew/Scp3114SpawnControl/releases" alt="GitHub Releases"></a>
<a href="https://github.com/MS-crew/Scp3114SpawnControl/blob/master/LICENSE"><img src="https://img.shields.io/badge/Licence-GNU_3.0-blue?style=for-the-badge&logo=gitbook" href="https://github.com/MS-crew/Scp3114SpawnControl/blob/master/LICENSE" alt="General Public License v3.0"></a>
<a href="https://github.com/ExMod-Team/EXILED"><img src="https://img.shields.io/badge/Exiled-9.13.0-red?style=for-the-badge&logo=gitbook" href="https://github.com/ExMod-Team/EXILED" alt="GitHub Exiled"></a>

</div>

- **Scp 3114 Natural Spawn :** This plugin allows SCP-3114 to spawn naturally during a match without requiring any admin commands or manual intervention.
  
- **Spawn Chance :** Configurable percentage chance for SCP-3114 to spawn naturally.

- **Minimum Human Count :** Specifies the minimum number of humans required for SCP-3114 to be eligible for spawning.

## Installation

1. Download the release file from the GitHub page [here](https://github.com/MS-crew/Scp3114SpawnControl/releases).
2. Extract the contents into your `\AppData\Roaming\EXILED\Plugins` directory.
3. Restart the server once.
3. Configure the plugin according to your server’s needs using the provided settings.
4. Restart your server to apply the changes.

## Feedback and Issues

This is the initial release of the plugin. We welcome any feedback, bug reports, or suggestions for improvements.

- **Report Issues:** [Issues Page](https://github.com/MS-crew/Scp3114SpawnControl/issues)
- **Contact:** [discerrahidenetim@gmail.com](mailto:discerrahidenetim@gmail.com)

Thank you for using our plugin and helping us improve it!
## Default Config
```yml
is_enabled: true
debug: false
# Chance for SCP-3114 to spawn (1 is default Scp Spawn Chance)
spawn_chance: 1
# Minimum number of player required for SCP-3114 to spawn.
minimum_player: 5
# SCP-3114 spawn chance not chancing by this plugin if any of the specified holiday types are currently active.
blocked_holiday_types:
- Halloween
- Christmas
spawn_points:
- name: 'Servers Lower Cabinet'
  chance: 0
  # The room type where SCP-3114 and the ragdolls will spawn.
  room: HczServerRoom
  # Position offset relative to the room (or world coordinates if no room is found).
  position:
    x: 6.08
    y: -3.54
    z: 4.29
  # Eular Rotation offset relative to the room.
  rotation:
    x: 0
    y: 180
    z: 0
  # List of custom ragdolls to spawn around SCP-3114 at this location.
  custom_ragdolls:
  - role_type: FacilityGuard
    # Position offset for the ragdoll, relative to the selected room.
    position:
      x: 6.29
      y: -3.54
      z: 2.94
    # Rotation offset for the ragdoll (X, Y, Z).
    rotation:
      x: 0
      y: 127
      z: 0
  - role_type: Scientist
    # Position offset for the ragdoll, relative to the selected room.
    position:
      x: 5
      y: -3.54
      z: 6.08
    # Rotation offset for the ragdoll (X, Y, Z).
    rotation:
      x: 0
      y: 82
      z: 0
- name: 'Servers Upper'
  chance: 0
  # The room type where SCP-3114 and the ragdolls will spawn.
  room: HczServerRoom
  # Position offset relative to the room (or world coordinates if no room is found).
  position:
    x: 5.4
    y: 0.98
    z: -6.31
  # Eular Rotation offset relative to the room.
  rotation:
    x: 0
    y: 270
    z: 0
  # List of custom ragdolls to spawn around SCP-3114 at this location.
  custom_ragdolls:
  - role_type: FacilityGuard
    # Position offset for the ragdoll, relative to the selected room.
    position:
      x: 3.25
      y: 0.98
      z: -6.4
    # Rotation offset for the ragdoll (X, Y, Z).
    rotation:
      x: 0
      y: 87
      z: 0
  - role_type: Scientist
    # Position offset for the ragdoll, relative to the selected room.
    position:
      x: 4.8
      y: 0.98
      z: -4.44
    # Rotation offset for the ragdoll (X, Y, Z).
    rotation:
      x: 0
      y: 302
      z: 0
# It prevents SCP 3114 from being spectated by spectators.
make3114_un_spectatable: false
```

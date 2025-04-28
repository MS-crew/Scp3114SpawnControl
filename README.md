<h1 align="center">Scp 3114 Spawn Control</h1>
<div align="center">
<a href="https://github.com/MS-crew/Scp3114SpawnControl/releases"><img src="https://img.shields.io/github/downloads/MS-crew/Scp3114SpawnControl/total?style=for-the-badge&logo=githubactions&label=Downloads" href="https://github.com/MS-crew/Scp3114SpawnControl/releases" alt="GitHub Release Download"></a>
<a href="https://github.com/MS-crew/Scp3114SpawnControl/releases"><img src="https://img.shields.io/badge/Build-1.2.0-brightgreen?style=for-the-badge&logo=gitbook" href="https://github.com/MS-crew/Scp3114SpawnControl/releases" alt="GitHub Releases"></a>
<a href="https://github.com/MS-crew/Scp3114SpawnControl/blob/master/LICENSE"><img src="https://img.shields.io/badge/Licence-GNU_3.0-blue?style=for-the-badge&logo=gitbook" href="https://github.com/MS-crew/Scp3114SpawnControl/blob/master/LICENSE" alt="General Public License v3.0"></a>
<a href="https://github.com/ExMod-Team/EXILED"><img src="https://img.shields.io/badge/Exiled-9.6.0-red?style=for-the-badge&logo=gitbook" href="https://github.com/ExMod-Team/EXILED" alt="GitHub Exiled"></a>

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
# Chance for SCP-3114 to spawn (in percentage).
chance: 50
# Minimum number of humans required for SCP-3114 to spawn.
minimum_human: 5
# SCP-3114 will NOT spawn if any of the specified holiday types are currently active.
blocked_holiday_types:
- Halloween
```

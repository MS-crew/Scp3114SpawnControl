using System.Linq;
using UnityEngine;
using Exiled.API.Features;
using Exiled.API.Extensions;

namespace Scp3114SpawnControl
{
    public class EventHandlers
    {
        public readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;
        public void AllPlayersSpawned()
        {
            if (Player.List.Where(p => p.IsHuman).Count() < plugin.Config.MinimumHuman)
                return;

            if ((float)Random.value >= Mathf.Clamp(plugin.Config.Chance / 100f, 0f, 1f))
                return;

            Player.List.Where(p => p.IsScp).GetRandomValue().Role
                .Set(PlayerRoles.RoleTypeId.Scp3114, Exiled.API.Enums.SpawnReason.RoundStart , PlayerRoles.RoleSpawnFlags.All);
        }
    }
}
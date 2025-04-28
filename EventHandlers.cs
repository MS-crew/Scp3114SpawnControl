using System.Linq;
using UnityEngine;
using Exiled.API.Features;
using Exiled.API.Extensions;
using MapGeneration.Holidays;

namespace Scp3114SpawnControl
{
    public class EventHandlers(Plugin plugin)
    {
        public void AllPlayersSpawned()
        {
            if (Player.List.Count(p => p.IsHuman) < Mathf.Max(1, plugin.Config.MinimumHuman))
                return;

            if (Random.Range(0, 100) >= plugin.Config.Chance)
                return;

            if (plugin.Config.BlockedHolidayTypes.Contains(HolidayUtils.GetActiveHoliday()))
                return;

            Player.List.GetRandomValue(p => p.IsScp)?.Role.Set(PlayerRoles.RoleTypeId.Scp3114, Exiled.API.Enums.SpawnReason.RoundStart, PlayerRoles.RoleSpawnFlags.All);
        }
    }
}
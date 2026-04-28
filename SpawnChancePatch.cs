using Exiled.API.Features;

using HarmonyLib;

using MapGeneration.Holidays;
using PlayerRoles.PlayableScps.Scp3114;

namespace Scp3114SpawnControl
{
    [HarmonyPatch(typeof(Scp3114Role), nameof(Scp3114Role.GetSpawnChance))]
    public static class SpawnChancePatch
    {
        private static void Postfix(ref float __result)
        {
            Config config = Plugin.Instance.Config;

            if (config.BlockedHolidayTypes.Contains(HolidayUtils.GetActiveHoliday()))
                return;

            if (Server.PlayerCount < config.MinimumPlayer)
                return;

            __result = config.SpawnChance;
        }
    }
}

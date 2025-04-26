using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using Exiled.API.Features.Pools;
using HarmonyLib;

using PlayerRoles;
using PlayerRoles.PlayableScps;
using PlayerRoles.PlayableScps.Scp3114;

namespace Scp3114SpawnControl
{
    [HarmonyPatch(typeof(Scp3114Spawner), nameof(Scp3114Spawner.OnPlayersSpawned))]
    public static class Scp3114SpawnerPatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = ListPool<CodeInstruction>.Pool.Get(instructions);

            int index = codes.FindIndex(x => x.opcode == OpCodes.Ldc_R4 && (float)x.operand == Scp3114Spawner.SpawnChance);

            codes[index].operand = (float)Plugin.Instance.Config.Chance / 100f;

            index = codes.FindIndex(x => x.opcode == OpCodes.Bge_S) - 1;

            codes[index].opcode = OpCodes.Ldc_I4;
            codes[index].operand = Plugin.Instance.Config.MinimumHuman;

            if (Plugin.Instance.Config.SelectFromScps)
            {
                MethodInfo method = typeof(PlayerRolesUtils).GetMethod(nameof(PlayerRolesUtils.ForEachRole), new[] { typeof(Action<ReferenceHub>) });
                index = codes.FindIndex(x => x.opcode == OpCodes.Call && x.operand is MethodInfo m && m.IsGenericMethod && m.GetGenericMethodDefinition() == method);
                codes[index].operand = method.MakeGenericMethod(typeof(FpcStandardScp));
            }

            for (int i = 0; i < codes.Count; i++)
                yield return codes[i];

            ListPool<CodeInstruction>.Pool.Return(codes);
        }
    }
}

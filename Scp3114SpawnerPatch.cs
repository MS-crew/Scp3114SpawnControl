using System;
using System.Collections.Generic;
using System.Reflection.Emit;

using Exiled.API.Features.Pools;
using HarmonyLib;

using PlayerRoles;
using PlayerRoles.PlayableScps;
using PlayerRoles.PlayableScps.Scp3114;

using static HarmonyLib.AccessTools;

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
                index = codes.FindLastIndex(x => x.LoadsField(Field(typeof(Scp3114Spawner), nameof(Scp3114Spawner.SpawnCandidates))));
                
                codes.InsertRange(index , new List<CodeInstruction>
                {
                    new CodeInstruction(OpCodes.Ldsfld , Field(typeof(Scp3114Spawner), nameof(Scp3114Spawner.SpawnCandidates))),
                    new CodeInstruction(OpCodes.Callvirt , Method(typeof(List<ReferenceHub>), nameof(List<ReferenceHub>.Clear))),

                    new CodeInstruction(OpCodes.Ldsfld , Field(typeof(Scp3114Spawner), nameof(Scp3114Spawner.SpawnCandidates))),
                    new CodeInstruction(OpCodes.Dup),
                    new CodeInstruction(OpCodes.Ldvirtftn, Method(typeof(List<ReferenceHub>), nameof(List<ReferenceHub>.Add))),
                    new CodeInstruction(OpCodes.Newobj, GetDeclaredConstructors(typeof(Action<ReferenceHub>))[0]),
                    new CodeInstruction(OpCodes.Call , Method(typeof(PlayerRolesUtils), nameof(PlayerRolesUtils.ForEachRole) , new[] { typeof(Action<ReferenceHub>) }).MakeGenericMethod(typeof(FpcStandardScp))),            
                });
            }

            for (int i = 0; i < codes.Count; i++)
                yield return codes[i];

            ListPool<CodeInstruction>.Pool.Return(codes);
        }
    }
}

using System.Collections.Generic;
using System.Reflection.Emit;

using Exiled.API.Features;
using Exiled.API.Features.Pools;
using HarmonyLib;

using PlayerRoles.PlayableScps.Scp3114;

namespace Scp3114SpawnControl
{
    [HarmonyPatch(typeof(Scp3114Spawner), nameof(Scp3114Spawner.OnPlayersSpawned))]
    public static class Scp3114SpawnerPatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = ListPool<CodeInstruction>.Pool.Get(instructions);

            // Default spawn chance is 0.0f (0%).
            const double defaultspawnchance = Scp3114Spawner.SpawnChance;

            // Find spawn chance index.
            int changeindex = codes.FindIndex(x => x.opcode == OpCodes.Ldc_R4 && (float)x.operand == defaultspawnchance);

            // Fail safe if it cannot find the index.
            if (changeindex == -1)
            {
                Log.Error("Scp3114Spawner error: Failed to find spawn chance instruction.");
                foreach (CodeInstruction code in codes)
                    yield return code;

                ListPool<CodeInstruction>.Pool.Return(codes); // cleanup

                yield break;
            }

            // `UnityEngine.Random.value >= 0f` to `UnityEngine.Random.value >= Plugin.Instance.Config.Chance / 100f`
            codes[changeindex].operand = (float)Plugin.Instance.Config.Chance / 100f;

            // Default minimum human for spawn is 2.
            int minhumanindex = codes.FindIndex(x => x.opcode == OpCodes.Ldc_I4_2 );

            // Fail safe if it cannot find the index.
            if (minhumanindex == -1)
            {
                Log.Error("Scp3114Spawner error: Failed to find minimum human instruction.");
                foreach (CodeInstruction code in codes)
                    yield return code;

                ListPool<CodeInstruction>.Pool.Return(codes); // cleanup

                yield break;
            }

            // `Scp3114Spawner.SpawnCandidates.Count < 2` to `Scp3114Spawner.SpawnCandidates.Count < Plugin.Instance.Config.MinimumHuman`
            codes[minhumanindex].opcode = OpCodes.Ldc_I4;
            codes[minhumanindex].operand = Plugin.Instance.Config.MinimumHuman;
            
            foreach (CodeInstruction code in codes)
                yield return code;

            ListPool<CodeInstruction>.Pool.Return(codes);
        }
    }
}

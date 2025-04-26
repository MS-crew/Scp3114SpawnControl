using System;

using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;

namespace Scp3114SpawnControl
{
    public class Plugin : Plugin<Config>
    {
        private Harmony harmony;

        public override string Author => "ZurnaSever";

        public override string Name => "Scp3114SpawnControl";

        public override string Prefix => "Scp3114SpawnControl";

        public static Plugin Instance { get; private set; }

        public override Version Version { get; } = new Version(1, 1, 0);

        public override Version RequiredExiledVersion { get; } = new Version(9, 6, 0);

        public override PluginPriority Priority { get; } = PluginPriority.First;

        public override void OnEnabled()
        {
            Instance = this;

            harmony = new Harmony("Scp3114SpawnControl" + DateTime.Now.Ticks);
            harmony.PatchAll();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            harmony.UnpatchAll(harmonyID: "Scp3114SpawnControl");

            Instance = null;
            base.OnDisabled();
        }
    }
}

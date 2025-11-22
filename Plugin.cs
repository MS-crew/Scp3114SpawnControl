using System;
using Exiled.API.Features;

using HarmonyLib;

namespace Scp3114SpawnControl
{
    public class Plugin : Plugin<Config>
    {
        private Harmony harmony;

        public static Plugin Instance { get; private set; }

        public override string Author => "ZurnaSever";

        public override string Name => "Scp3114SpawnControl";

        public override string Prefix => "Scp3114SpawnControl";

        public override Version Version { get; } = new Version(1, 3, 0);

        public override Version RequiredExiledVersion { get; } = new Version(9, 10, 0);

        public override void OnEnabled()
        {
            Instance = this;

            harmony = new Harmony(Prefix + DateTime.Now.Ticks);
            harmony.PatchAll();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            harmony.UnpatchAll(harmony.Id);

            harmony = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}

using System;
using Exiled.API.Features;

using HarmonyLib;

namespace Scp3114SpawnControl
{
    public class Plugin : Plugin<Config>
    {
        private Harmony harmony;

        private EventHandler eventHandler;

        public static Plugin Instance { get; private set; }

        public override string Author => "Ms";

        public override string Name => "Scp3114SpawnControl";

        public override string Prefix => "Scp3114SpawnControl";

        public override Version Version { get; } = new Version(1, 4, 0);

        public override Version RequiredExiledVersion { get; } = new Version(9, 13, 0);

        public override void OnEnabled()
        {
            Instance = this;
            eventHandler = new EventHandler();

            eventHandler.Subscribe();

            harmony = new Harmony(Prefix + DateTime.Now.Ticks);
            harmony.PatchAll();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            eventHandler.Unsubscribe();

            harmony.UnpatchAll(harmony.Id);

            Instance = null;
            base.OnDisabled();
        }
    }
}

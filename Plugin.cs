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

        public override string Author => "ZurnaSever";

        public override string Name => "Scp3114SpawnControl";

        public override string Prefix => "Scp3114SpawnControl";

        public override Version Version { get; } = new Version(1, 3, 1);

        public override Version RequiredExiledVersion { get; } = new Version(9, 10, 0);

        public override void OnEnabled()
        {
            Instance = this;
            eventHandler = new EventHandler();

            Exiled.Events.Handlers.Player.Spawned += eventHandler.OnSpawned;

            harmony = new Harmony(Prefix + DateTime.Now.Ticks);
            harmony.PatchAll();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Spawned -= eventHandler.OnSpawned;

            harmony.UnpatchAll(harmony.Id);

            harmony = null;
            eventHandler = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}

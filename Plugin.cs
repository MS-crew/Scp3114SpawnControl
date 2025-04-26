using System;
using Exiled.API.Features;
using PlayerRoles.RoleAssign;

namespace Scp3114SpawnControl
{
    public class Plugin : Plugin<Config>
    {
        public static EventHandlers eventHandlers;

        public override string Author => "ZurnaSever";

        public override string Name => "Scp3114SpawnControl";

        public override string Prefix => "Scp3114SpawnControl";

        public override Version Version { get; } = new Version(1, 1, 0);

        public override Version RequiredExiledVersion { get; } = new Version(9, 6, 0);

        public override void OnEnabled()
        {
            eventHandlers = new EventHandlers(this);
            RoleAssigner.OnPlayersSpawned += eventHandlers.AllPlayersSpawned;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            RoleAssigner.OnPlayersSpawned -= eventHandlers.AllPlayersSpawned;
            eventHandlers = null;
            base.OnDisabled();
        }
    }
}

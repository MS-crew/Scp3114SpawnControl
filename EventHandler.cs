using System.Collections.Generic;

using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

using PlayerRoles;

namespace Scp3114SpawnControl
{
    public class EventHandler
    {
        private const RoleTypeId scp3114Role = RoleTypeId.Scp3114;

        private readonly Dictionary<Player, bool> spectatableCache = new(2);

        public void OnWaitingforPlayers()
        {
            spectatableCache.Clear();
        }

        public void OnSpawned(SpawnedEventArgs ev)
        {
            if (!Plugin.Instance.Config.Make3114UnSpectatable)
                return;

            if (ev.OldRole.Type == scp3114Role && ev.Player.Role.Type != scp3114Role && spectatableCache.TryGetValue(ev.Player, out bool previous))
            {
                ev.Player.IsSpectatable = previous;
                spectatableCache.Remove(ev.Player);
                return;
            }

            if (ev.Player.Role.Type == scp3114Role)
            {
                spectatableCache[ev.Player] = ev.Player.IsSpectatable;
                ev.Player.IsSpectatable = false;
            }
        }
    }
}

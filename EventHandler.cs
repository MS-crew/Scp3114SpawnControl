using System.Collections.Generic;
using System.Linq;

using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

using PlayerRoles;
using PlayerRoles.PlayableScps.Scp3114;

using Scp3114SpawnControl.Models;

using UnityEngine;

using Events = Exiled.Events.Handlers;
using Random = UnityEngine.Random;

namespace Scp3114SpawnControl
{
    public class EventHandler
    {
        private const RoleTypeId scp3114Role = RoleTypeId.Scp3114;

        private readonly Dictionary<Player, bool> spectatableCache = new(2);

        private readonly Dictionary<Player, Quaternion> pendingRotations = new(2);

        public void Subscribe()
        {
            Events.Player.Spawned += OnSpawned;
            Events.Player.Spawning += OnSpawning;
            Events.Server.WaitingForPlayers += OnWaitingforPlayers;

            PlayerRoleManager.OnServerRoleSet -= Scp3114InitialRagdollSpawner.OnServerRoleSet;
        }

        public void Unsubscribe()
        {
            Events.Player.Spawned -= OnSpawned;
            Events.Player.Spawning -= OnSpawning;
            Events.Server.WaitingForPlayers -= OnWaitingforPlayers;

            PlayerRoleManager.OnServerRoleSet += Scp3114InitialRagdollSpawner.OnServerRoleSet;
        }

        private void OnWaitingforPlayers() 
        { 
            spectatableCache.Clear();
            pendingRotations.Clear();
        }

        private void OnSpawning(SpawningEventArgs ev)
        {
            if (ev.NewRole.Type != scp3114Role)
                return;

            if (!ev.NewRole.SpawnFlags.HasFlag(RoleSpawnFlags.UseSpawnpoint))
                return;

            List<SpawnPoint> spawnPoints = Plugin.Instance.Config.SpawnPoints;
            if (spawnPoints.Count == 0)
                return;

            float totalChance = spawnPoints.Sum(x => x.Chance);
            float randomValue = Random.Range(0f, totalChance);
            float cumulative = 0f;

            SpawnPoint selectedSpawn = null;

            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                cumulative += spawnPoint.Chance;
                if (randomValue <= cumulative)
                {
                    selectedSpawn = spawnPoint;
                    break;
                }
            }

            if (selectedSpawn == null)
                return;

            Room room = Room.Get(selectedSpawn.Room);
            bool roomNull = room == null;

            ev.Position = roomNull ? selectedSpawn.Position : room.WorldPosition(selectedSpawn.Position);

            /// Not working idk why?
            //ev.HorizontalRotation = roomNull ? selectedSpawn.Rotation.y : (room.Rotation * Quaternion.Euler(selectedSpawn.Rotation)).eulerAngles.y;

            pendingRotations[ev.Player] = roomNull ? Quaternion.Euler(selectedSpawn.Rotation) : room.Rotation * Quaternion.Euler(selectedSpawn.Rotation);

            if (!Scp3114InitialRagdollSpawner._ragdollsSpawned && selectedSpawn.CustomRagdolls != null && selectedSpawn.CustomRagdolls.Count != 0)
            {
                foreach (CustomRagdolls ragdoll in selectedSpawn.CustomRagdolls)
                {
                    Vector3 pos = roomNull ? ragdoll.Position : room.WorldPosition(ragdoll.Position);
                    Quaternion rot = roomNull ? Quaternion.Euler(ragdoll.Rotation) : room.Rotation * Quaternion.Euler(ragdoll.Rotation);

                    Scp3114InitialRagdollSpawner.ServerSpawnRagdoll(ragdoll.RoleType, pos, rot, ev.Player.ReferenceHub);
                }

                Scp3114InitialRagdollSpawner._ragdollsSpawned = true;
            }
        }

        private void OnSpawned(SpawnedEventArgs ev)
        {
            if (ev.Player.Role.Type == scp3114Role)
            {
                Scp3114InitialRagdollSpawner.ServerSpawnRagdolls(ev.Player.ReferenceHub);

                if (pendingRotations.TryGetValue(ev.Player, out Quaternion rot))
                {
                    if (ev.SpawnFlags.HasFlag(RoleSpawnFlags.UseSpawnpoint))
                        ev.Player.Rotation = rot;

                    pendingRotations.Remove(ev.Player);
                }
            }
            
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

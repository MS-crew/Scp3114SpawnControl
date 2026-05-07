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

            SpawnPoint selectedSpawn = TryGetSpawnPoint();
            if (selectedSpawn == null)
                return;

            Room room = Room.Get(selectedSpawn.Room);
            bool isRoomNull = room == null;

            /// Not working idk why?
            // ev.HorizontalRotation = roomNull ? selectedSpawn.Rotation.y : (room.Rotation * Quaternion.Euler(selectedSpawn.Rotation)).eulerAngles.y;

            ev.Position = isRoomNull ? selectedSpawn.Position : room.WorldPosition(selectedSpawn.Position);
            pendingRotations[ev.Player] = isRoomNull ? Quaternion.Euler(selectedSpawn.Rotation) : room.Rotation * Quaternion.Euler(selectedSpawn.Rotation);

            TrySpawnCustomRagdolls(selectedSpawn, room, isRoomNull, ev.Player);
        }

        private void OnSpawned(SpawnedEventArgs ev)
        {
            bool isNewRoleIsScp3114 = ev.Player.Role.Type == scp3114Role;

            if (isNewRoleIsScp3114)
                Handle3114Spawn(ev.Player);

            if (!Plugin.Instance.Config.Make3114UnSpectatable)
                return;

            if (ev.OldRole.Type == scp3114Role && !isNewRoleIsScp3114 && spectatableCache.TryGetValue(ev.Player, out bool previous))
            {
                ev.Player.IsSpectatable = previous;
                spectatableCache.Remove(ev.Player);
                return;
            }

            if (isNewRoleIsScp3114)
            {
                spectatableCache[ev.Player] = ev.Player.IsSpectatable;
                ev.Player.IsSpectatable = false;
            }
        }

        private SpawnPoint TryGetSpawnPoint()
        {
            List<SpawnPoint> spawnPoints = Plugin.Instance.Config.SpawnPoints;
            if (spawnPoints.Count == 0)
                return null;

            float totalChance = spawnPoints.Sum(x => x.Chance);
            if (totalChance <= 0f)
                return null;

            float randomValue = Random.Range(0f, Mathf.Max(100f, totalChance));
            float cumulative = 0f;

            SpawnPoint selectedSpawn = null;

            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                cumulative += spawnPoint.Chance;
                if (randomValue <= cumulative)
                    return spawnPoint;
            }

            return selectedSpawn;
        }

        private void TrySpawnCustomRagdolls(SpawnPoint spawnPoint, Room room, bool isRoomNull, Player player)
        {
            if (Scp3114InitialRagdollSpawner._ragdollsSpawned)
                return;

            if (spawnPoint.CustomRagdolls == null || spawnPoint.CustomRagdolls.Count == 0)
                return;

            foreach (CustomRagdolls ragdoll in spawnPoint.CustomRagdolls)
            {
                Vector3 pos = isRoomNull ? ragdoll.Position : room.WorldPosition(ragdoll.Position);
                Quaternion rot = isRoomNull ? Quaternion.Euler(ragdoll.Rotation) : room.Rotation * Quaternion.Euler(ragdoll.Rotation);

                Scp3114InitialRagdollSpawner.ServerSpawnRagdoll(ragdoll.RoleType, pos, rot, player.ReferenceHub);
            }

            Scp3114InitialRagdollSpawner._ragdollsSpawned = true;
        }

        private void Handle3114Spawn(Player player)
        {
            Scp3114InitialRagdollSpawner.ServerSpawnRagdolls(player.ReferenceHub);

            if (pendingRotations.TryGetValue(player, out Quaternion rot))
            {
                player.Rotation = rot;
                pendingRotations.Remove(player);
            }
        }
    }
}

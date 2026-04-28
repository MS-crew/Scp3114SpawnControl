using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Exiled.API.Enums;
using Exiled.API.Interfaces;

using MapGeneration.Holidays;

using PlayerRoles;
using PlayerRoles.PlayableScps.Scp3114;

using Scp3114SpawnControl.Models;

using UnityEngine;

namespace Scp3114SpawnControl
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        [Description("Chance for SCP-3114 to spawn (1 is default Scp Spawn Chance)")]
        public float SpawnChance { get; set; } = 1;

        [Description("Minimum number of player required for SCP-3114 to spawn.")]
        public int MinimumPlayer { get; set; } = 5;

        [Description("SCP-3114 spawn chance not chancing by this plugin if any of the specified holiday types are currently active.")]
        public HolidayType[] BlockedHolidayTypes { get; set; } = Scp3114Role.SupportedHolidays;

        public List<SpawnPoint> SpawnPoints { get; set; } = 
        [
            new SpawnPoint()
            {
                Chance = 50,
                Name = "Servers Lower Cabinet",
                Room = RoomType.HczServerRoom,
                Position = new Vector3(6.08f, -3.54f, 4.29f),
                Rotation = new Vector3(0, 180, 0),
                CustomRagdolls = 
                [
                    new CustomRagdolls()
                    {
                        RoleType = RoleTypeId.FacilityGuard,
                        Position = new Vector3(6.29f, -3.54f, 2.94f),
                        Rotation = new Vector3(0, 127, 0)
                    },
                    new CustomRagdolls()
                    {
                        RoleType = RoleTypeId.Scientist,
                        Position = new Vector3(5, -3.54f, 6.08f),
                        Rotation = new Vector3(0, 82, 0)
                    }
                ]
            },
            new SpawnPoint()
            {
                Chance = 50,
                Name = "Servers Upper",
                Room = RoomType.HczServerRoom,
                Position = new Vector3(5.40f, 0.98f, -6.31f),
                Rotation = new Vector3(0, 270, 0),
                CustomRagdolls =
                [
                    new CustomRagdolls()
                    {
                        RoleType = RoleTypeId.FacilityGuard,
                        Position = new Vector3(3.25f, 0.98f, -6.40f),
                        Rotation = new Vector3(0, 87, 0)
                    },
                    new CustomRagdolls()
                    {
                        RoleType = RoleTypeId.Scientist,
                        Position = new Vector3(4.80f, 0.98f, -4.44f),
                        Rotation = new Vector3(0, 302, 0)
                    }
                ]
            }
        ];

        [Description("It prevents SCP 3114 from being spectated by spectators.")]
        public bool Make3114UnSpectatable { get; set; } = false;
    }
}

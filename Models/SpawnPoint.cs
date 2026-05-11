using System.Collections.Generic;
using System.ComponentModel;

using Exiled.API.Enums;

using UnityEngine;

namespace Scp3114SpawnControl.Models
{
    public class SpawnPoint
    {
        public string Name { get; set; }

        public float Chance { get; set; }

        [Description("The room type where SCP-3114 and the ragdolls will spawn.")]
        public RoomType Room { get; set; }

        [Description("Position offset relative to the room (or world coordinates if no room is found).")]
        public Vector3 Position { get; set; }

        [Description("Eular Rotation offset relative to the room.")]
        public Vector3 Rotation { get; set; }

        [Description("List of custom ragdolls to spawn around SCP-3114 at this location.")]
        public List<CustomRagdolls> CustomRagdolls { get; set; }
    }
}

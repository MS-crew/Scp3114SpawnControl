using System.ComponentModel;

using PlayerRoles;

using UnityEngine;

namespace Scp3114SpawnControl.Models
{
    public class CustomRagdolls
    {
        public RoleTypeId RoleType { get; set; }

        [Description("Position offset for the ragdoll, relative to the room.")]
        public Vector3 Position { get; set; }

        [Description("Eular Rotation offset relative to the room.")]
        public Vector3 Rotation { get; set; }
    }
}

using System.ComponentModel;

using PlayerRoles;

using UnityEngine;

namespace Scp3114SpawnControl.Models
{
    public class CustomRagdolls
    {
        [Description("The role type of the ragdoll (e.g., Scientist, ClassD, FacilityGuard).")]
        public RoleTypeId RoleType { get; set; }

        [Description("Position offset for the ragdoll, relative to the selected room.")]
        public Vector3 Position { get; set; }

        [Description("Rotation offset for the ragdoll (X, Y, Z).")]
        public Vector3 Rotation { get; set; }
    }
}

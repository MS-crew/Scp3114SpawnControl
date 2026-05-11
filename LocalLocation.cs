using System;

using CommandSystem;

using Exiled.API.Features;

using UnityEngine;

namespace Scp3114SpawnControl
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class LocalLocation : ICommand
    {
        public string Command { get; } = "mylocallocation";
        public string[] Aliases { get; } = ["myloc"];
        public string Description { get; } = "Shows your local position and rotation in your current room.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.TryGet(sender, out Player player))
            {
                response = "This command can only be used by players.";
                return false;
            }

            Room room = player.CurrentRoom;

            if (room == null)
            {
                response = "You are not in a room.";
                return false;
            }

            response = $"Room: {room.Type}, Local Position: {room.LocalPosition(player.Position)}, Local Rotation: {(Quaternion.Inverse(room.Rotation) * player.Rotation).eulerAngles}";
            return true;
        }
    }
}

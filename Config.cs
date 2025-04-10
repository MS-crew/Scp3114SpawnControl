using System.ComponentModel;

using Exiled.API.Interfaces;

namespace Scp3114SpawnControl
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        [Description("Chances of Scp-3114 spawning")]
        public int Chance { get; set; } = 100;

        [Description("Minimum number of player required for Scp-3114 to spawn")]
        public int MinimumHuman { get; set; } = 0;
    }
}

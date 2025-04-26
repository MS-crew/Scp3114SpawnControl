using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Scp3114SpawnControl
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        [Description("Chance for SCP-3114 to spawn (in percentage).")]
        public int Chance { get; set; } = 50;

        [Description("Minimum number of humans required for SCP-3114 to spawn.")]
        public int MinimumHuman { get; set; } = 5;

        [Description("Whether SCP-3114 will be selected from SCPs or humans. (True = SCPs / False = Humans)")]
        public bool SelectFromScps { get; set; } = true;
    }
}

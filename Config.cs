using System.ComponentModel;
using Exiled.API.Interfaces;
using MapGeneration.Holidays;
using System.Collections.Generic;

namespace Scp3114SpawnControl
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Chance for SCP-3114 to spawn (1 is default Scp Spawn Chance)")]
        public float SpawnChance { get; set; } = 1;

        [Description("Minimum number of humans required for SCP-3114 to spawn.")]
        public int MinimumHuman { get; set; } = 5;

        [Description("SCP-3114 spawn chance not chancing by this plugin if any of the specified holiday types are currently active.")]
        public List<HolidayType> BlockedHolidayTypes { get; set; } =
        [
            HolidayType.Halloween,
        ];
    }
}

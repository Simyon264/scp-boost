using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace scp_boost
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool AdrenalineBoost { get; set; } = true;
        public float AdrenalineBoostDuration { get; set; } = 10f;
        public byte AdrenalineBoostAmount { get; set; } = 10;
        public string AdrenalineBoostMessage { get; set; } = "You have %amount%% speed boost for %duration%.";
        public Dictionary<RoleType, byte> Boost { get; set; } = new Dictionary<RoleType, byte>()
        {
            { RoleType.Scp049, 5}
        };
    }
}

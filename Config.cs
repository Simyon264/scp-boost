using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace scp_boost
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public Dictionary<RoleType, byte> Boost { get; set; } = new Dictionary<RoleType, byte>()
        {
            { RoleType.Scp049, 5}
        };
    }
}

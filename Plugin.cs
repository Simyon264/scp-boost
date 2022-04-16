using Exiled.API.Enums;
using Features = Exiled.API.Features;
using ServerEvents = Exiled.Events.Handlers.Server;
using PlayerEvents = Exiled.Events.Handlers.Player;
using Exiled.Events.EventArgs;
using MEC;

namespace scp_boost
{
    public class Plugin : Features.Plugin<Config>
    {
        public override string Name { get; } = "SCP-Boost";
        public override string Prefix { get; } = "scpboost";
        public override string Author { get; } = "Simyon";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override PluginPriority Priority { get; } = PluginPriority.High;

        private static readonly Plugin InstanceValue = new Plugin();
        private Plugin()
        {

        }

        public static Plugin StaticInstance => InstanceValue;

        public override void OnEnabled()
        {
            if (!Config.IsEnabled) return;

            PlayerEvents.ChangingRole += Spawn;
            PlayerEvents.Died += Died;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            PlayerEvents.ChangingRole -= Spawn;
            PlayerEvents.Died -= Died;
            base.OnDisabled();
        }

        public void Spawn(ChangingRoleEventArgs ev)
        {
            Timing.CallDelayed(1f, () =>
             {
                 foreach (KeyValuePair<RoleType, byte> entry in Config.Boost)
                 {
                     if (entry.Key == ev.NewRole)
                     {
                         ev.Player.EnableEffect("MovementBoost");
                         ev.Player.ChangeEffectIntensity("MovementBoost", entry.Value);
                     }
                 }
             });
        }

        public void Died(DiedEventArgs ev)
        {
            ev.Target.DisableAllEffects();
        }
    }
}
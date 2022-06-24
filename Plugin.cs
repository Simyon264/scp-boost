using Exiled.API.Enums;
using Features = Exiled.API.Features;
using ServerEvents = Exiled.Events.Handlers.Server;
using PlayerEvents = Exiled.Events.Handlers.Player;
using Exiled.Events.EventArgs;
using MEC;
using System.Linq;

namespace scp_boost
{
    public class Plugin : Features.Plugin<Config>
    {
        public override string Name { get; } = "SCP-Boost";
        public override string Prefix { get; } = "scpboost";
        public override string Author { get; } = "Simyon";
        public override Version Version { get; } = new Version(1, 0, 1);
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
            PlayerEvents.UsedItem += UsedItem;
            PlayerEvents.Died += Died;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            PlayerEvents.ChangingRole -= Spawn;
            PlayerEvents.UsedItem -= UsedItem;
            PlayerEvents.Died -= Died;
            base.OnDisabled();
        }

        public void UsedItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type == ItemType.Adrenaline)
            { 
                Timing.CallDelayed(0.3f, () =>
                {
                    ev.Player.EnableEffect("MovementBoost", Config.AdrenalineBoostDuration, true);
                    ev.Player.ChangeEffectIntensity("MovementBoost", Config.AdrenalineBoostAmount);
                    ev.Player.ShowHint(Config.AdrenalineBoostMessage.Replace("%amount%", Config.AdrenalineBoostAmount.ToString()).Replace("%duration%", Config.AdrenalineBoostDuration.ToString()));
                });
            }
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
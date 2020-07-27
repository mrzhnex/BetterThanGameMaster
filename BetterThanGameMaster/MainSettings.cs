using Exiled.API.Features;

namespace BetterThanGameMaster
{
    public class MainSettings : Plugin<Config>
    {
        public override string Name => nameof(BetterThanGameMaster);
        public SetEvents SetEvents { get; set; }
        public override void OnEnabled()
        {
            SetEvents = new SetEvents();
            Exiled.Events.Handlers.Player.Escaping += SetEvents.OnEscaping;
            Exiled.Events.Handlers.Server.RoundStarted += SetEvents.OnRoundStarted;
            Exiled.Events.Handlers.Server.WaitingForPlayers += SetEvents.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.Hurting += SetEvents.OnHurting;
            Exiled.Events.Handlers.Map.AnnouncingScpTermination += SetEvents.OnAnnouncingScpTermination;
            Exiled.Events.Handlers.Player.ChangingRole += SetEvents.OnChangingRole;
            Exiled.Events.Handlers.Player.SpawningRagdoll += SetEvents.OnSpawningRagdoll;
            Exiled.Events.Handlers.Player.InteractingDoor += SetEvents.OnInteractingDoor;
            Exiled.Events.Handlers.Server.SendingConsoleCommand += SetEvents.OnSendingConsoleCommand;
            Log.Info(Name + " on");
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Escaping -= SetEvents.OnEscaping;
            Exiled.Events.Handlers.Server.RoundStarted -= SetEvents.OnRoundStarted;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= SetEvents.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.Hurting -= SetEvents.OnHurting;
            Exiled.Events.Handlers.Map.AnnouncingScpTermination -= SetEvents.OnAnnouncingScpTermination;
            Exiled.Events.Handlers.Player.ChangingRole -= SetEvents.OnChangingRole;
            Exiled.Events.Handlers.Player.SpawningRagdoll -= SetEvents.OnSpawningRagdoll;
            Exiled.Events.Handlers.Player.InteractingDoor -= SetEvents.OnInteractingDoor;
            Exiled.Events.Handlers.Server.SendingConsoleCommand -= SetEvents.OnSendingConsoleCommand;
            Log.Info(Name + " off");
        }
    }
}
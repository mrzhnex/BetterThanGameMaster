using EXILED;

namespace BetterThanGameMaster
{
    public class MainSettings : Plugin
    {
        public override string getName => nameof(BetterThanGameMaster);
        public SetEvents SetEvents { get; set; }
        public override void OnEnable()
        {
            SetEvents = new SetEvents();
            Events.RoundStartEvent += SetEvents.OnRoundStart;
            Events.WaitingForPlayersEvent += SetEvents.OnWaitingForPlayers;
            Events.PlayerHurtEvent += SetEvents.OnPlayerHurt;
            Events.CheckEscapeEvent += SetEvents.OnCheckEscape;
            Events.AnnounceScpTerminationEvent += SetEvents.OnAnnounceScpTermination;
            Events.PlayerSpawnEvent += SetEvents.OnPlayerSpawn;
            Events.ConsoleCommandEvent += SetEvents.OnConsoleCommand;
            Events.SpawnRagdollEvent += SetEvents.OnSpawnRagdoll;
            Events.DoorInteractEvent += SetEvents.OnDoorInteractEvent;
            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            Events.RoundStartEvent -= SetEvents.OnRoundStart;
            Events.WaitingForPlayersEvent -= SetEvents.OnWaitingForPlayers;
            Events.PlayerHurtEvent -= SetEvents.OnPlayerHurt;
            Events.CheckEscapeEvent -= SetEvents.OnCheckEscape;
            Events.AnnounceScpTerminationEvent -= SetEvents.OnAnnounceScpTermination;
            Events.PlayerSpawnEvent -= SetEvents.OnPlayerSpawn;
            Events.ConsoleCommandEvent -= SetEvents.OnConsoleCommand;
            Events.SpawnRagdollEvent -= SetEvents.OnSpawnRagdoll;
            Events.DoorInteractEvent -= SetEvents.OnDoorInteractEvent;
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}
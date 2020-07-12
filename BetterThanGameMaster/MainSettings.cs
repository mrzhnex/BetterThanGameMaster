using EXILED;

namespace BetterThanGameMaster
{
    public class MainSettings : Plugin
    {
        public override string getName => "BetterThanGameMaster";
        private SetEvents SetEvents;
        public override void OnEnable()
        {
            SetEvents = new SetEvents();
            Events.RoundStartEvent += SetEvents.OnRoundStart;
            Events.WaitingForPlayersEvent += SetEvents.OnWaitingForPlayers;
            Events.DoorInteractEvent += SetEvents.OnDoorInteract;
            Events.PlayerHurtEvent += SetEvents.OnPlayerHurt;
            Events.CheckEscapeEvent += SetEvents.OnCheckEscape;
            Events.AnnounceScpTerminationEvent += SetEvents.OnAnnounceScpTermination;
            Events.PlayerSpawnEvent += SetEvents.OnPlayerSpawn;
            Events.ConsoleCommandEvent += SetEvents.OnConsoleCommand;
            Events.SpawnRagdollEvent += SetEvents.OnSpawnRagdoll;
            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            Events.RoundStartEvent -= SetEvents.OnRoundStart;
            Events.WaitingForPlayersEvent -= SetEvents.OnWaitingForPlayers;
            Events.DoorInteractEvent -= SetEvents.OnDoorAccess;
            Events.PlayerHurtEvent -= SetEvents.OnPlayerHurt;
            Events.CheckEscapeEvent -= SetEvents.OnCheckEscape;
            Events.AnnounceScpTerminationEvent -= SetEvents.OnAnnounceScpTermination;
            Events.PlayerSpawnEvent -= SetEvents.OnPlayerSpawn;
            Events.ConsoleCommandEvent -= SetEvents.OnConsoleCommand;
            Events.SpawnRagdollEvent -= SetEvents.OnSpawnRagdoll; 
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}
using Exiled.API.Features;
using UnityEngine;

namespace BetterThanGameMaster
{
    public class OpenDoorsOnTime : MonoBehaviour
    {
        private float Timer = 0f;  

        public void Update()
        {
            Timer += Time.deltaTime; 
            if (Timer > Global.timeToOpenClassD && !Global.OpenClassD)
            {
                Global.OpenClassD = true;
                foreach (Door door in Global.classDDoors)
                {
                    door.NetworkisOpen = true;
                    door.CallRpcDoSound();
                }

            }
            if (Timer > Global.timeToOpenOtherSCP && !Global.OpenOtherSCP)
            {
                Global.OpenOtherSCP = true;
                Global.gate049.Networklocked = false;
                Global.gate049.NetworkisOpen = true;
                foreach (Door door in Global.scp939Doors)
                {
                    door.NetworkisOpen = true;
                    door.CallRpcDoSound();
                }
            }
            if (Timer > Global.timeToOpen173 && !Global.Open173)
            {
                Global.Open173 = true;
                Global.gate173.Networklocked = false;
                Global.gate173.NetworkisOpen = true;
                foreach (Player p in Player.List)
                {
                    if (p.Team == Team.SCP && p.Role != RoleType.Scp079)
                    {
                        p.ClearBroadcasts();
                        p.Broadcast(20, "SCP: прошло 3 минуты", Broadcast.BroadcastFlags.Normal);
                    }
                }
            }
            if (Timer > Global.timeTenMinuts && !Global.AnonceTenMinuts)
            {
                Global.AnonceTenMinuts = true;
                foreach (Player p in Player.List)
                {
                    if (p.Team == Team.SCP && p.Role != RoleType.Scp079)
                    {
                        p.ClearBroadcasts();
                        p.Broadcast(20, "SCP: прошло 10 минут", Broadcast.BroadcastFlags.Normal);
                    }
                }

            }
            if (Timer > Global.timeFifrteenMinuts && !Global.AnonceFifteenMinuts)
            {
                Global.AnonceFifteenMinuts = true;
                foreach (Player p in Player.List)
                {
                    if (p.Role == RoleType.Scp106)
                    {
                        p.ClearBroadcasts();
                        p.Broadcast(20, "SCP: прошло 15 минут", Broadcast.BroadcastFlags.Normal);
                    }
                }
            }
        }
    }
}
﻿using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using EXILED;
using EXILED.Extensions;

namespace BetterThanGameMaster
{
    internal class SetEvents
    {
        internal void OnConsoleCommand(ConsoleCommandEvent ev)
        {
            if (!Global.can_use_commands)
            {
                ev.ReturnMessage = "Дождитесь начала раунда!";
                return;
            }
            else if (ev.Command.ToLower().Contains("contain") && ev.Command.ToLower().Contains("96"))
            {
                ReferenceHub scp096 = Player.GetHubs().Where(x => x.GetRole() == RoleType.Scp096).FirstOrDefault();
                if (scp096 == default)
                {
                    ev.ReturnMessage = Global._outofscp096;
                    return;
                }
                if (ev.Player.GetTeam() == Team.SCP)
                {
                    ev.ReturnMessage = Global._outofscp096;
                    return;
                }
                foreach (GameObject gameplayer in PlayerManager.players)
                {
                    if (gameplayer.GetComponent<Contain096OwnerComponent>() != null)
                    {
                        ev.ReturnMessage = Global._alreadycontainproccess096;
                        return;
                    }
                }
                if (Vector3.Distance(ev.Player.GetPosition(), scp096.GetPosition()) < Global.distanceForContain096And173)
                {

                    ev.Player.gameObject.AddComponent<Contain096OwnerComponent>();
                    ev.Player.gameObject.GetComponent<Contain096OwnerComponent>().owner = ev.Player;
                    ev.Player.gameObject.GetComponent<Contain096OwnerComponent>().scp096 = scp096;
                    ev.ReturnMessage = Global._successstartcontain096 + Global.time_to_contain_096;
                    return;
                }
                else
                {
                    ev.ReturnMessage = Global._outofscp096;
                    return;
                }
            }
            else if (ev.Command.ToLower().Contains("contain") && ev.Command.ToLower().Contains("173"))
            {
                ReferenceHub scp173 = Player.GetHubs().Where(x => x.GetRole() == RoleType.Scp173).FirstOrDefault();
                if (scp173 == default)
                {
                    ev.ReturnMessage = Global._outofscp173;
                    return;
                }
                if (ev.Player.GetTeam() == Team.SCP)
                {
                    ev.ReturnMessage = Global._outofscp173;
                    return;
                }
                foreach (GameObject gameplayer in PlayerManager.players)
                {
                    if (gameplayer.GetComponent<Contain173OwnerComponent>() != null)
                    {
                        ev.ReturnMessage = Global._alreadycontainproccess173;
                        return;
                    }
                }
                if (Vector3.Distance(ev.Player.GetPosition(), scp173.GetPosition()) < Global.distanceForContain096And173)
                {

                    ev.Player.gameObject.AddComponent<Contain173OwnerComponent>();
                    ev.Player.gameObject.GetComponent<Contain173OwnerComponent>().owner = ev.Player;
                    ev.Player.gameObject.GetComponent<Contain173OwnerComponent>().scp173 = scp173;
                    ev.ReturnMessage = Global._successstartcontain173 + Global.time_to_contain_173;
                    return;
                }
                else
                {
                    ev.ReturnMessage = Global._outofscp173;
                    return;
                }
            }
        }

        internal void OnDoorInteract(ref DoorInteractionEvent ev)
        {
            throw new System.NotImplementedException();
        }

        internal void OnSpawnRagdoll(SpawnRagdollEvent ev)
        {
            if (ev.Killer.GetRole() == RoleType.Scp096)
            {
                ev.Allow = false;
            }
        }

        internal void OnRoundStart()
        {
            RoundSummary.RoundLock = true;
            Global.can_use_commands = true;
            if (GameObject.FindWithTag("FemurBreaker") != null)
            {
                GameObject.FindWithTag("FemurBreaker").AddComponent<OpenDoorsOnTime>();
                GameObject.FindWithTag("FemurBreaker").AddComponent<CheckCustomEscape>();
            }
        }

        internal void OnWaitingForPlayers()
        {
            Door temp173 = null;
            Door temp049 = null;
            foreach (Door door in Map.Doors)
            {
                if (door.DoorName == "173")
                {
                    temp173 = door;
                }
                if (door.DoorName.ToLower().Contains("049"))
                {
                    temp049 = door;
                }
            }

            foreach (Door door in Map.Doors)
            {
                if (Vector3.Distance(temp173.gameObject.transform.position, door.gameObject.transform.position) < 20f)
                {
                    if (door.DoorName.ToLower().Contains("armory") || door.DoorName.ToLower().Contains("173"))
                    {
                        continue;
                    }
                    Global.gate173 = door;
                    door.Networklocked = true;
                    door.NetworkisOpen = false;
                }
                if (Vector3.Distance(temp049.gameObject.transform.position, door.gameObject.transform.position) < 20f)
                {
                    if (door.DoorName.ToLower().Contains("armory"))
                    {
                        continue;
                    }
                    Global.gate049 = door;
                    door.Networklocked = true;
                    door.NetworkisOpen = false;
                }
            }
            Global.can_use_commands = false;

            Global.classDDoors = new List<Door>();
            Global.scp939Doors = new List<Door>();
            Global.OpenClassD = false;
            Global.OpenOtherSCP = false;
            Global.Open173 = false;
            Global.AnonceTenMinuts = false;
            Global.timeToOpenClassD = 20.0f;
            Global.timeToOpenOtherSCP = 40.0f;
            Global.timeToOpen173 = 180.0f;
        }

        internal void OnPlayerHurt(ref PlayerHurtEvent ev)
        {
            if (ev.Attacker.GetRole() == RoleType.Scp93953 || ev.Attacker.GetRole() == RoleType.Scp93989)
            {
                ev.Amount = 300.0f;
            }
            if ((ev.Player.GetRole() == RoleType.Scp173 || ev.Player.GetRole() == RoleType.Scp106 || ev.Player.GetRole() == RoleType.Scp096) && ev.DamageType == DamageTypes.Tesla)
            {
                ev.Amount = 0.0f;
            }
        }

        internal void OnPlayerSpawn(PlayerSpawnEvent ev)
        {
            if (ev.Player.GetRole() == RoleType.ClassD)
            {
                ev.Player.gameObject.AddComponent<SetRoleOnSpawn>();
                ev.Player.gameObject.GetComponent<SetRoleOnSpawn>().AddItems.Add(ItemType.Flashlight);
                if (!Global.OpenClassD)
                {
                    if (ev.Player.gameObject.GetComponent<DoorAddComponent>() == null)
                        ev.Player.gameObject.AddComponent<DoorAddComponent>();
                }
            }
            else if ((ev.Player.GetRole() == RoleType.Scp93953 || ev.Player.GetRole() == RoleType.Scp93989) && !Global.OpenOtherSCP)
            {
                if (ev.Player.gameObject.GetComponent<DoorAddComponent>() == null)
                    ev.Player.gameObject.AddComponent<DoorAddComponent>();
            }
        }

        internal void OnCheckEscape(ref CheckEscapeEvent ev)
        {
            if (ev.Player.GetRole() != RoleType.FacilityGuard && ev.Player.GetTeam() != Team.MTF)
            {
                ev.Player.ClearInventory();
                ev.Allow = false;
                ev.Player.SetRole(RoleType.Spectator);
            }
        }

        internal void OnAnnounceScpTermination(AnnounceScpTerminationEvent ev)
        {
            ev.Allow = false;
        }
    }
}
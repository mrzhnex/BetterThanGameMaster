using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Exiled.Events.EventArgs;
using Exiled.API.Features;

namespace BetterThanGameMaster
{
    public class SetEvents
    {
        internal void OnRoundStarted()
        {
            RoundSummary.RoundLock = true;
            Global.can_use_commands = true;
            if (GameObject.FindWithTag("FemurBreaker") != null)
            {
                GameObject.FindWithTag("FemurBreaker").AddComponent<OpenDoorsOnTime>();
            }
            Global.IntercomPosition = GameObject.Find("IntercomMonitor").transform.position;
            foreach (Door door in Map.Doors)
            {
                if (door.DoorName.ToLower().Contains("intercom"))
                {
                    Global.IntercomDoorPosition = door.gameObject.transform.position;
                    break;
                }
            }
        }

        internal void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker != null && (ev.Attacker.Role == RoleType.Scp93953 || ev.Attacker.Role == RoleType.Scp93989))
            {
                ev.Amount = 300.0f;
            }
            if ((ev.Target.Role == RoleType.Scp173 || ev.Target.Role == RoleType.Scp106 || ev.Target.Role == RoleType.Scp096) && ev.DamageType == DamageTypes.Tesla)
            {
                ev.Amount = 0.0f;
            }
        }

        internal void OnAnnouncingScpTermination(AnnouncingScpTerminationEventArgs ev)
        {
            ev.IsAllowed = false;
        }

        internal void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleType.ClassD && !Global.OpenClassD)
            {
                if (ev.Player.GameObject.GetComponent<DoorAddComponent>() == null)
                    ev.Player.GameObject.AddComponent<DoorAddComponent>();
            }
            else if ((ev.NewRole == RoleType.Scp93953 || ev.NewRole == RoleType.Scp93989) && !Global.OpenOtherSCP)
            {
                if (ev.Player.GameObject.GetComponent<DoorAddComponent>() == null)
                    ev.Player.GameObject.AddComponent<DoorAddComponent>();
            }
        }

        internal void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
        {
            if (ev.Killer != null && ev.Killer.Role == RoleType.Scp096)
            {
                ev.IsAllowed = false;
                for (int i = 1; i < 4; i++)
                {
                    ev.Owner.GameObject.GetComponent<CharacterClassManager>().RpcPlaceBlood(ev.Owner.Position, 2, i);
                }
            }
        }

        internal void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (Global.IsRemoteControl && (ev.Door.DoorName.ToLower().Contains("gate_b") || ev.Door.DoorName.ToLower().Contains("gate_a")))
            {
                ev.IsAllowed = false;
            }
        }

        internal void OnSendingConsoleCommand(SendingConsoleCommandEventArgs ev)
        {
            if (!Global.can_use_commands)
            {
                ev.ReturnMessage = "Дождитесь начала раунда!";
                return;
            }
            else if (ev.Name.ToLower().Contains("contain") && ev.Name.ToLower().Contains("96"))
            {
                Player scp096 = Player.List.Where(x => x.Role == RoleType.Scp096).FirstOrDefault();
                if (scp096 == default)
                {
                    ev.ReturnMessage = Global._outofscp096;
                    return;
                }
                if (ev.Player.Team == Team.SCP)
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
                if (Vector3.Distance(ev.Player.Position, scp096.Position) < Global.distanceForContain096And173)
                {

                    ev.Player.GameObject.AddComponent<Contain096OwnerComponent>();
                    ev.Player.GameObject.GetComponent<Contain096OwnerComponent>().owner = ev.Player;
                    ev.Player.GameObject.GetComponent<Contain096OwnerComponent>().scp096 = scp096;
                    ev.ReturnMessage = Global._successstartcontain096 + Global.time_to_contain_096;
                    return;
                }
                else
                {
                    ev.ReturnMessage = Global._outofscp096;
                    return;
                }
            }
            else if (ev.Name.ToLower().Contains("contain") && ev.Name.ToLower().Contains("173"))
            {
                Player scp173 = Player.List.Where(x => x.Role == RoleType.Scp173).FirstOrDefault();
                if (scp173 == default)
                {
                    ev.ReturnMessage = Global._outofscp173;
                    return;
                }
                if (ev.Player.Team == Team.SCP)
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
                if (Vector3.Distance(ev.Player.Position, scp173.Position) < Global.distanceForContain096And173)
                {

                    ev.Player.GameObject.AddComponent<Contain173OwnerComponent>();
                    ev.Player.GameObject.GetComponent<Contain173OwnerComponent>().owner = ev.Player;
                    ev.Player.GameObject.GetComponent<Contain173OwnerComponent>().scp173 = scp173;
                    ev.ReturnMessage = Global._successstartcontain173 + Global.time_to_contain_173;
                    return;
                }
                else
                {
                    ev.ReturnMessage = Global._outofscp173;
                    return;
                }
            }
            if (Vector3.Distance(ev.Player.Position, GameObject.Find("IntercomMonitor").transform.position) < 9.0f &&
                Vector3.Distance(ev.Player.Position, GameObject.Find("IntercomMonitor").transform.position) > 7.5f &&
                Vector3.Distance(ev.Player.Position, Global.IntercomDoorPosition) > 14.0f)
            {
                if (ev.Name.ToLower() == "ra_diagnosis")
                {
                    Global.RemoteControlStage = 1;
                    ev.ReturnMessage = ".....Сброс процессов системы...........Ошибки не обнаружены......";
                    ev.Color = "blue";
                    return;
                }
                else if (ev.Name.ToLower() == "ra_retreivelocal1")
                {
                    if (Global.RemoteControlStage != 1)
                    {
                        ev.ReturnMessage = "В доступе отказано. Запустите диагностику системы.";
                        ev.Color = "red";
                        return;
                    }
                    Global.RemoteControlStage = 2;
                    ev.ReturnMessage = "Введите код доступа:";
                    ev.Color = "yellow";
                    return;
                }
                else if (ev.Name.ToLower() == "ra_retreivecontol1")
                {
                    if (Global.RemoteControlStage != 1)
                    {
                        ev.ReturnMessage = "В доступе отказано. Запустите диагностику системы.";
                        ev.Color = "red";
                        return;
                    }
                    Global.RemoteControlStage = 3;
                    ev.ReturnMessage = "Введите код доступа:";
                    ev.Color = "yellow";
                    return;
                }
                else if (ev.Name.ToLower() == "mike16alpha02")
                {
                    if (Global.RemoteControlStage == 2)
                    {
                        Global.RemoteControlStage = 0;
                        if (Global.IsRemoteControl)
                        {
                            Global.IsRemoteControl = false;
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("Gate A and Gate B lockdown sequence disengaged", 0.0f, 0.0f);
                            ev.ReturnMessage = "Удаленный доступ был отключен";
                            ev.Color = "blue";
                            return;
                        }
                        else
                        {
                            ev.ReturnMessage = "Удаленный доступ уже выключен.";
                            ev.Color = "red";
                            return;
                        }
                    }
                    else if (Global.RemoteControlStage == 3)
                    {
                        Global.RemoteControlStage = 0;
                        if (Global.IsRemoteControl)
                        {
                            ev.ReturnMessage = "Удаленный доступ уже включен";
                            ev.Color = "red";
                            return;
                        }
                        else
                        {
                            Global.IsRemoteControl = true;
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("Gate A and Gate B lockdown sequence initiated", 0.0f, 0.0f);
                            ev.ReturnMessage = "Удаленный доступ был включен.";
                            ev.Color = "blue";
                            return;
                        }
                    }
                    else
                    {
                        ev.ReturnMessage = "....Обнаружен незарегистрированный набор символов........Команда не была выполнена.........";
                        ev.Color = "red";
                        return;
                    }
                }
            }
        }

        internal void OnEscaping(EscapingEventArgs ev)
        {
            if (ev.Player.Role == RoleType.FacilityGuard || ev.Player.Role == RoleType.ClassD || ev.Player.Role == RoleType.Scientist)
            {
                ev.IsAllowed = false;
                ev.Player.ClearInventory();
                ev.Player.SetRole(RoleType.Spectator);
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
    }
}
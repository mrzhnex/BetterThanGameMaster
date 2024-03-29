﻿using System.Collections.Generic;
using UnityEngine;

namespace BetterThanGameMaster
{
    public static class Global
    {
        public static float distanceForClassD = 5.2f;
        public static float distanceForSCP939 = 30.0f;
        public static float distanceForAmmo = 0.5f;
        public static float distanceForContain096And173 = 2.0f;

        public static float timeToOpenClassD = 20.0f;
        public static float timeToOpen173 = 180.0f;
        public static float timeToOpenOtherSCP = 40.0f;
        public static float timeTenMinuts = 600.0f;
        public static float timeFifrteenMinuts = 900.0f;

        public static bool OpenClassD = false;
        public static bool OpenOtherSCP = false;
        public static bool Open173 = false;
        public static bool AnonceTenMinuts = false;
        public static bool AnonceFifteenMinuts = false;
        
        public static List<Door> classDDoors = new List<Door>();
        public static List<Door> scp939Doors = new List<Door>();
        public static Door gate173;
        public static Door gate049;


        internal static bool can_use_commands;

        public static float time_to_contain_096 = 15.0f;
        public static string _outofscp096 = "SCP 096 нет рядом";
        public static string _successstartcontain096 = "Вы надеваете мешок на SCP 096. Ждите: ";
        public static string _noticescp096 = "На вас надевают мешок";
        public static string announcecontainscp096 = "SCP 0 9 6 ContainedSuccessfully . ";
        public static string _alreadycontainproccess096 = "Другой гуманоид уже надевает мешок";
        public static string _failedcontain096and173 = "Вы отошли слишком далеко. Процесс прерван";

        public static float time_to_contain_173 = 40.0f;
        public static string _outofscp173 = "SCP 173 нет рядом";
        public static string _successstartcontain173 = "Вы собираете клетку 173. Ждите: ";
        public static string _noticescp173 = "Вас помещают в клетку";
        public static string announcecontainscp173 = "SCP 1 7 3 ContainedSuccessfully . ";
        public static string _alreadycontainproccess173 = "Другой гуманоид уже собирает клетку";


        public static bool IsRemoteControl = false;

        public static int RemoteControlStage = 0;
        public static Vector3 IntercomPosition = Vector3.zero;
        public static Vector3 IntercomDoorPosition = Vector3.zero;
    }
}
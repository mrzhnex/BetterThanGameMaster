using UnityEngine;
using System.Linq;
using Exiled.API.Features;

namespace BetterThanGameMaster
{
    public class DoorAddComponent : MonoBehaviour
    {
        private Player Target;
        private float Timer = 0.0f;

        public void Start()
        {
            Target = Player.Get(gameObject);
        }

        public void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > 0.2f)
            {
                switch (Target.Role)
                {
                    case RoleType.ClassD:
                        if (!Global.OpenClassD)
                        {
                            foreach (Door door in Map.Doors.Where(x => x.DoorName == "").ToList())
                            {
                                if (Vector3.Distance(Target.Position, door.gameObject.transform.position) < Global.distanceForClassD)
                                {
                                    if (!Global.classDDoors.Contains(door))
                                        Global.classDDoors.Add(door);
                                }
                            }
                        }
                        break;

                    case RoleType.Scp93953:
                    case RoleType.Scp93989:
                        if (!Global.OpenOtherSCP)
                        {
                            foreach (Door door in Map.Doors.Where(x => x.DoorName == "").ToList())
                            {
                                if (Vector3.Distance(Target.Position, door.gameObject.transform.position) < Global.distanceForSCP939)
                                {
                                    if (!Global.scp939Doors.Contains(door))
                                        Global.scp939Doors.Add(door);
                                }
                            }
                        }
                        break;
                }

                Destroy(gameObject.GetComponent<DoorAddComponent>());
            }
        }
    }
}
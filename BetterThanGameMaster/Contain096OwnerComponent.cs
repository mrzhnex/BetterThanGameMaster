using EXILED.Extensions;
using UnityEngine;

namespace BetterThanGameMaster
{
    class Contain096OwnerComponent : MonoBehaviour
    {
        public ReferenceHub owner;
        public ReferenceHub scp096;
        private float timer = 0.0f;
        private readonly float time_is_up = 1.0f;
        private float progress = Global.time_to_contain_096;

        public void Update()
        {
            timer += Time.deltaTime;
            if (timer >= time_is_up)
            {
                timer = 0f;
                progress -= time_is_up;
                owner.ClearBroadcasts();
                owner.Broadcast(1, Global._successstartcontain096 + progress, true);
                scp096.ClearBroadcasts();
                scp096.Broadcast(1, Global._noticescp096, true);
                if (progress <= 0.0f)
                {
                    Contain();
                }
                if (Vector3.Distance(transform.position, scp096.GetPosition()) > Global.distanceForContain096And173)
                {
                    owner.ClearBroadcasts();
                    owner.Broadcast(10, Global._failedcontain096and173, true);
                    Destroy(gameObject.GetComponent<Contain096OwnerComponent>());
                }
            }
        }

        private void Contain()
        {
            scp096.SetRole(RoleType.Spectator);
            Destroy(gameObject.GetComponent<Contain096OwnerComponent>());
        }
    }
}
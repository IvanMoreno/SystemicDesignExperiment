using System.Collections.Generic;
using UnityEngine;

namespace Sistemic.Runtime
{
    public class AreaEmitter : MonoBehaviour
    {
        [SerializeField] string signal;
        [SerializeField] float radius = 1;

        void Update()
        {
            foreach (var potentialReceiver in NearbyElements())
            {
                EmitTo(potentialReceiver.gameObject);                
            }
        }

        IEnumerable<Collider2D> NearbyElements() => Physics2D.OverlapCircleAll(transform.position, radius);

        void EmitTo(GameObject potentialReceiver)
        {
            potentialReceiver.SendMessage
            (
                "Perceive",
                new Dictionary<string, object>
                {
                    { "signal", signal }
                },
                SendMessageOptions.DontRequireReceiver
            );
        }

        void OnDrawGizmos() => Gizmos.DrawWireSphere(transform.position, radius);
    }
}
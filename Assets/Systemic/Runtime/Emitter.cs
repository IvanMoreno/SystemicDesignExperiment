using System.Collections.Generic;
using UnityEngine;

namespace Sistemic.Runtime
{
    public class Emitter : MonoBehaviour
    {
        [SerializeField] string signal;

        void OnTriggerEnter(Collider other) => EmitTo(other.gameObject);

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
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Sistemic.Runtime
{
    public class DebugReceiver : MonoBehaviour
    {
        void Perceive(Dictionary<string, object> stimuli)
        {
            Debug.Log($"Perceived signal {stimuli["signal"]}");
        }
    }
}
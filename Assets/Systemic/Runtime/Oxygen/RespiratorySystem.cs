using System.Linq;
using UnityEngine;

namespace Sistemic.Runtime.Oxygen
{
    public class RespiratorySystem : MonoBehaviour
    {
        void Update()
        {
            ReflectOxygenInBody();
            if (!HasOxygenInBody())
                DieOfAsphyxiation();
        }

        void ReflectOxygenInBody()
        {
            if (!HasOxygenInBody()) return;
            
            GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.blue, OxygenInBody());
        }

        bool HasOxygenInBody() => OxygenInBody() > 0;
        float OxygenInBody() => GetComponentsInChildren<Lung>().Sum(lung => lung.AccumulatedOxygen);

        void DieOfAsphyxiation()
        {
            foreach (var lung in GetComponentsInChildren<Lung>())
            {
                lung.enabled = false;
            }

            GetComponentInChildren<SpriteRenderer>().color = Color.purple;
        }
    }
}
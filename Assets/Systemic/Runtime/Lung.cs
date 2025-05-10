using System.Collections.Generic;
using UnityEngine;

namespace Sistemic.Runtime.Oxygen
{
    public class Lung : MonoBehaviour
    {
        [SerializeField] float carbonDioxideEmissionRadius = 1;
        [SerializeField] float oxygenAccumulationIncrementMultiplier = 1;
        [SerializeField] float oxygenAccumulationDecrementMultiplier = 1;
        [SerializeField] float accumulatedOxygen = 1;

        public float AccumulatedOxygen
        {
            get => accumulatedOxygen;
            private set => accumulatedOxygen = Mathf.Clamp(value, 0, 1);
        }

        void Update() => Exhale();
        void Exhale()
        {
            if (AccumulatedOxygen <= 0) return;
            
            AccumulatedOxygen -= Time.deltaTime * oxygenAccumulationDecrementMultiplier;
            EmitToElementsInsideArea();
        }
        
        void EmitToElementsInsideArea()
        {
            foreach (var potentialReceiver in NearbyElements())
            {
                EmitTo(potentialReceiver.gameObject);
            }
        }

        IEnumerable<Collider2D> NearbyElements() => Physics2D.OverlapCircleAll(transform.position, carbonDioxideEmissionRadius);

        void EmitTo(GameObject potentialReceiver)
        {
            potentialReceiver.SendMessage
            (
                "Perceive",
                new Dictionary<string, object>
                {
                    { "signal", "CarbonDioxide" }
                },
                SendMessageOptions.DontRequireReceiver
            );
        }

        void Perceive(Dictionary<string, object> stimuli)
        {
            if (stimuli["signal"].Equals("Oxygen"))
            {
                Inhale();
            }
        }

        void Inhale() => AccumulatedOxygen += Time.deltaTime * oxygenAccumulationIncrementMultiplier;
    }
}
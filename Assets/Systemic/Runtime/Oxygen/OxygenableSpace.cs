using System.Collections.Generic;
using UnityEngine;

namespace Sistemic.Runtime.Oxygen
{
    public class OxygenableSpace : MonoBehaviour
    {
        [SerializeField] float spaceRadius = 1;
        [SerializeField] float oxygenAccumulationDecrementMultiplier = 1;
        [SerializeField] float accumulatedOxygen = 1;
        
        public float AccumulatedOxygen
        {
            get => accumulatedOxygen;
            private set => accumulatedOxygen = Mathf.Clamp(value, 0, 1);
        }

        void Update()
        {
            if (accumulatedOxygen > 0) 
                EmitToElementsInsideArea();

            GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.blue, AccumulatedOxygen);
        }

        void EmitToElementsInsideArea()
        {
            foreach (var potentialReceiver in NearbyElements())
            {
                EmitTo(potentialReceiver.gameObject);
            }
        }

        IEnumerable<Collider2D> NearbyElements() => Physics2D.OverlapCircleAll(transform.position, spaceRadius);

        void EmitTo(GameObject potentialReceiver)
        {
            potentialReceiver.SendMessage
            (
                "Perceive",
                new Dictionary<string, object>
                {
                    { "signal", "Oxygen" }
                },
                SendMessageOptions.DontRequireReceiver
            );
        }

        void Perceive(Dictionary<string, object> stimuli)
        {
            if (stimuli["signal"].Equals("CarbonDioxide"))
            {
                DecreaseOxygen();
            }
        }

        void DecreaseOxygen() => AccumulatedOxygen -= Time.deltaTime * oxygenAccumulationDecrementMultiplier;
        void OnDrawGizmos() => Gizmos.DrawWireSphere(transform.position, spaceRadius);
    }
}
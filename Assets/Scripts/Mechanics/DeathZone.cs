using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// DeathZone components mark a collider which will schedule a
    /// PlayerEnteredDeathZone event when the player enters the trigger.
    /// </summary>
    public class DeathZone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            IHealthHandler healthHandler = collider.gameObject.GetComponent<IHealthHandler>();
            if (healthHandler == null)
            {
                return;
            }
            
            healthHandler.Death();
        }
    }
}
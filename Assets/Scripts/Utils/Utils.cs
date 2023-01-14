using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Physic
    {
        public class UnityUtils : MonoBehaviour
        {
            public bool IsColliderTouchingGround (Collider2D collider, LayerMask lm, float extraRaycastRange = 0.1f)
            {
                return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, extraRaycastRange, lm).collider != null;
            }
        }

        public static UnityUtils Unity = new UnityUtils();
    }
    
}

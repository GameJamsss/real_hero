using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Entities.PlayerEntity.ModifierImpl
{
    public class SlideModifier : StateModifier<Player>
    {
        public override void EnterModify(Player entity)
        {
           
        }

        public override void ExitModify(Player entity)
        {
            
        }

        public override void UpdateModify(Player entity)
        {
            //float speedDiff = 0 - entity.Rigidbody.velocity.x;
            //float movement = Mathf.Abs(speedDiff) * entity.Decceleration * Mathf.Sign(speedDiff);
            //entity.Rigidbody.AddForce(movement * Vector2.right);\
            entity.Rigidbody.velocity = Vector2.zero;
        }
    }
}

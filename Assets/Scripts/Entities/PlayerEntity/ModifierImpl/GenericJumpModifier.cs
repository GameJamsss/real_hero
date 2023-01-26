using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Entities.PlayerEntity
{
    public class GenericJumpModifier : StateModifier<Player>
    {
        public override void EnterModify(Player entity)
        {
            entity.Rigidbody.velocity = Vector2.up * entity.JumpHeight;
            //entity.Rigidbody.AddForce(Vector2.up * entity.Rigidbody.velocity.y * (1 - entity.JumpHeight), ForceMode2D.Impulse);
        }

        public override void ExitModify(Player entity)
        {

        }

        public override void UpdateModify(Player entity)
        {
            
        }
    }
}

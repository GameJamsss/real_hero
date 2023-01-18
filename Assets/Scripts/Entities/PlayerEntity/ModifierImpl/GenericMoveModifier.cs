using Assets.Scripts.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    public class GenericMoveModifier : StateModifier<Player>
    {
        public override void EnterModify(Player entity)
        {

        }

        public override void ExitModify(Player entity)
        {

        }

        public override void UpdateModify(Player entity)
        {
            float targetSpeed = Input.GetAxis("Horizontal") * entity.Velocity;

            float speedDiff = targetSpeed - entity.Rigidbody.velocity.x;

            float acceleration = Mathf.Abs(targetSpeed) > 0.01f ? entity.Acceleration : entity.Decceleration;

            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * acceleration, entity.VelocityPower) * Mathf.Sign(speedDiff);

            entity.Rigidbody.AddForce(movement * Vector2.right);
        }
    }
}

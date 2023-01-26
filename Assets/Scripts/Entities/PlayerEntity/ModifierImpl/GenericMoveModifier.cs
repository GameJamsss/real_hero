using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Entities.PlayerEntity
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
            float input = Input.GetAxis("Horizontal");

            float targetSpeed = input * entity.Velocity;

            float speedDiff = targetSpeed - entity.Rigidbody.velocity.x;

            float acceleration = Mathf.Abs(targetSpeed) > 0.01f ? entity.Acceleration : entity.Decceleration;

            float movement = Mathf.Abs(speedDiff) * acceleration * Mathf.Sign(speedDiff);

            Vector3 characterScale = entity.transform.localScale;

            characterScale.x = input < 0 ? -1 : input > 0 ? 1 : characterScale.x;

            entity.transform.localScale = characterScale;

            entity.Rigidbody.AddForce(movement * Vector2.right);
        }
    }
}

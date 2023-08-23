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

            entity.Rigidbody.velocity = new Vector2(targetSpeed, entity.Rigidbody.velocity.y);
        }
    }
}

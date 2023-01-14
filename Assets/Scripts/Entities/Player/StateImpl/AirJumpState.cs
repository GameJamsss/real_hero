using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.StateImpl
{
    public class AirJumpState : AbstractState<TestPlayer>
    {
        public AirJumpState()
        {
            Name = "AirJump";
            Priority = 3;
        }

        protected override void OnEnterLogic(TestPlayer entity)
        {
            entity.Rigidbody.AddForce(Vector2.up * entity.JumpHeight, ForceMode2D.Impulse);
        }

        protected override void OnExitLogic(TestPlayer entity)
        {

        }

        protected override void OnUpdateLogic(TestPlayer entity)
        {

        }

        public override bool EnterCondition(TestPlayer entity)
        {
            return Utils.Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask)
                && Input.GetButton("Jump");
        }
    }
}

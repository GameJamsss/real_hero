using Assets.Scripts.Entities.Player.ModifierImpl;
using Assets.Scripts.StateMachine;
using System;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.StateImpl
{
    public class RunState : AbstractState<TestPlayer>
    {
        public RunState()
        {
            Name = "Run";
            Priority = 2;
            AddModifier(new GenericMoveModifier());
        }

        protected override void OnEnterLogic(TestPlayer entity)
        {

        }

        protected override void OnExitLogic(TestPlayer entity)
        {

        }

        protected override void OnUpdateLogic(TestPlayer entity)
        {

        }

        public override bool EnterCondition(TestPlayer entity)
        {
            return Input.GetButton("Horizontal")
                && Utils.Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask);
        }
    }
}

﻿using Assets.Scripts.Entities.Player.ModifierImpl;
using Assets.Scripts.StateMachine;
using System;

namespace Assets.Scripts.Entities.Player.StateImpl
{
    public class MovingUpState : AbstractState<TestPlayer>
    {
        public MovingUpState()
        {
            Name = "MovingUp";
            Priority = 5;
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
            return entity.Rigidbody.velocity.y > 0f 
                && !Utils.Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask);
        }
    }
}
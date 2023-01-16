using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.ModifierImpl
{
    internal class GenericJumpModifier : StateModifier<TestPlayer>
    {
        public override void EnterModify(TestPlayer entity)
        {
            entity.Rigidbody.AddForce(Vector2.up * entity.JumpHeight, ForceMode2D.Impulse);
        }

        public override void ExitModify(TestPlayer entity)
        {

        }

        public override void UpdateModify(TestPlayer entity)
        {
            
        }
    }
}

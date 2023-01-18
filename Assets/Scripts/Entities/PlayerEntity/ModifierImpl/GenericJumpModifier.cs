using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    internal class GenericJumpModifier : StateModifier<Player>
    {
        public override void EnterModify(Player entity)
        {
            entity.Rigidbody.velocity.Set(entity.Rigidbody.velocity.x, 0f);
            entity.Rigidbody.AddForce(Vector2.down * entity.Rigidbody.velocity.y * (1 - entity.JumpHeight), ForceMode2D.Impulse);
        }

        public override void ExitModify(Player entity)
        {

        }

        public override void UpdateModify(Player entity)
        {
            
        }
    }
}

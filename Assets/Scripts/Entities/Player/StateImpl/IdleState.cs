using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Player.StateImpl
{
    public class IdleState : AbstractState<TestPlayer>
    {
        public IdleState()
        {
            Name = "Idle";
            _priority = 1;
        }

        public override void OnEnter(TestPlayer entity)
        {
           
        }

        public override void OnExit(TestPlayer entity)
        {
            
        }

        public override void OnUpdate(TestPlayer entity)
        {
            
        }
    }
}

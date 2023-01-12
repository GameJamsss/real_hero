using Assets.Scripts.StateMachine;

namespace Assets.Scripts.Entities.Player.StateImpl
{
    public class IdleState : AbstractState<TestPlayer>
    {
        public IdleState()
        {
            Name = "Idle";
            Priority = 1;
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
    }
}

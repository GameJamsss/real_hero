using Assets.Scripts.StateMachine;

namespace Assets.Scripts.Entities.PlayerEntity.ModifierImpl
{
    public class GravityFallModifier : StateModifier<Player>
    {
        public override void EnterModify(Player entity)
        {
            entity.Rigidbody.gravityScale = entity.FallGravityScale;
        }

        public override void ExitModify(Player entity)
        {
            entity.Rigidbody.gravityScale = 1f;
        }

        public override void UpdateModify(Player entity)
        {
            
        }
    }
}

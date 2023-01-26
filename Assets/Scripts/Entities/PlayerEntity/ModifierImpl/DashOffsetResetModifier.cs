using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Entities.PlayerEntity.ModifierImpl
{
    public class DashOffsetResetModifier : StateModifier<Player>
    {
        public override void EnterModify(Player entity)
        {
           
        }

        public override void ExitModify(Player entity)
        {
            
        }

        public override void UpdateModify(Player entity)
        {
            if (entity.CurrentDashOffsetSec < entity.DashOffsetSec) entity.CurrentDashOffsetSec += Time.deltaTime;
        }
    }
}

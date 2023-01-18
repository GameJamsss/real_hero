using Assets.Scripts.StateMachine;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    internal static class StateMap
    {
        private static readonly GenericMoveModifier gmm = new GenericMoveModifier();
        private static readonly GenericJumpModifier gjm = new GenericJumpModifier();

        public static State<Player> Idle = new State<Player>("Idle", (ulong) StatePriority.Idle)
            .SetOnStateEnter(e => e.AirJumpsCounter = 0)
            .AddModifier(gmm);

        public static State<Player> Move = new State<Player>("Move", (ulong) StatePriority.Move)
            .SetEnterCondition(entity => Input.GetButton("Horizontal"))
            .AddModifier(gmm);

        public static State<Player> Jump = new State<Player>("Jump", (ulong) StatePriority.Jump)
            .SetEnterCondition(entity => Input.GetButton("Jump") && entity.AirJumpsCounter < entity.MaxAirJumps)
            .SetOnStateEnter(entity =>
                entity.AirJumpsCounter += Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask) ? 0 : 1
            )
            .AddModifier(gmm)
            .AddModifier(gjm);

        public static State<Player> Fall = new State<Player>("Fall", (ulong) StatePriority.Fall)
            .SetEnterCondition(entity => 
                !Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask)
            )
            .AddModifier(gmm)
            .ToBlack(Move);

        public static State<Player> MoveUp = new State<Player>("MoveUp", (ulong) StatePriority.MoveUp)
            .SetEnterCondition(entity => 
                entity.Rigidbody.velocity.y > 0f 
                && !Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask)
            )
            .AddModifier(gmm)
            .ToBlack(Move);
    }
}

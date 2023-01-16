using Assets.Scripts.Entities.Player.ModifierImpl;
using Assets.Scripts.StateMachine;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.StateImpl
{
    internal static class StateMap
    {
        private static readonly GenericMoveModifier gmm = new GenericMoveModifier();
        private static readonly GenericJumpModifier gjm = new GenericJumpModifier();

        public static State<TestPlayer> Idle = new State<TestPlayer>("Idle", (ulong) StatePriority.Idle)
            .SetOnStateEnter(e => e.AirJumpsCounter = 0);

        public static State<TestPlayer> Move = new State<TestPlayer>("Move", (ulong) StatePriority.Move)
            .SetEnterCondition(entity => 
                Input.GetButton("Horizontal")
                && Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask))
            .AddModifier(gmm);

        public static State<TestPlayer> Jump = new State<TestPlayer>("Jump", (ulong) StatePriority.Jump)
            .SetEnterCondition(entity => Input.GetButton("Jump") && entity.AirJumpsCounter < entity.MaxAirJumps)
            .SetOnStateEnter(entity =>
                entity.AirJumpsCounter += Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask) ? 0 : 1
            )
            .AddModifier(gmm)
            .AddModifier(gjm);

        public static State<TestPlayer> Fall = new State<TestPlayer>("Fall", (ulong) StatePriority.Fall)
            .SetEnterCondition(entity => 
                !Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask)
            )
            .AddModifier(gmm)
            .ToBlack(Move);

        public static State<TestPlayer> MoveUp = new State<TestPlayer>("MoveUp", (ulong) StatePriority.MoveUp)
            .SetEnterCondition(entity => 
                entity.Rigidbody.velocity.y > 0f 
                && !Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask)
            )
            .AddModifier(gmm)
            .ToBlack(Move);
    }
}

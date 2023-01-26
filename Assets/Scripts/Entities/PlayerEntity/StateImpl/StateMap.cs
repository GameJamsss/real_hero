using Assets.Scripts.Entities.PlayerEntity.ModifierImpl;
using Assets.Scripts.StateMachine;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Entities.PlayerEntity
{
    public static class StateMap
    {
        private static readonly GenericMoveModifier gmm = new GenericMoveModifier();
        private static readonly SlideModifier sm = new SlideModifier();
        private static readonly GenericJumpModifier gjm = new GenericJumpModifier();
        private static readonly GravityFallModifier gfm = new GravityFallModifier();
        private static readonly GenericDamageModifier gam = new GenericDamageModifier(1);
        private static readonly DashOffsetResetModifier dorm = new DashOffsetResetModifier();

        public static State<Player> Idle = new State<Player>("Idle", (ulong) StatePriority.Idle)
            .SetOnStateEnter(e => { e.AirJumpsCounter = 0; })
            .AddModifier(dorm)
            .AddModifier(sm);

        public static State<Player> Move = new State<Player>("Move", (ulong) StatePriority.Move)
            .SetEnterCondition(entity => Input.GetButton("Horizontal"))
            .AddModifier(dorm)
            .AddModifier(gmm);

        public static State<Player> Jump = new State<Player>("Jump", (ulong) StatePriority.Jump)
            .SetEnterCondition(entity => Input.GetButton("Jump") && entity.AirJumpsCounter < entity.MaxAirJumps)
            .SetOnStateEnter(entity =>
                entity.AirJumpsCounter += Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask) ? 0 : 1
            )
            .AddModifier(gmm)
            .AddModifier(dorm)
            .AddModifier(gjm);

        public static State<Player> Fall = new State<Player>("Fall", (ulong) StatePriority.Fall)
            .SetEnterCondition(entity => 
                !Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask)
            )
            .AddModifier(gmm)
            .AddModifier(dorm)
            .AddModifier(gfm)
            .ToBlack(Move);

        public static State<Player> MoveUp = new State<Player>("MoveUp", (ulong) StatePriority.MoveUp)
            .SetEnterCondition(entity => 
                entity.Rigidbody.velocity.y > 0f 
                && !Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask)
            )
            .AddModifier(gmm)
            .AddModifier(dorm)
            .ToBlack(Move);

        public static State<Player> Dash = new State<Player>("Dash", (ulong) StatePriority.Dash)
            .SetEnterCondition(entity => Input.GetButton("Dash") && entity.CurrentDashOffsetSec > entity.DashOffsetSec)
            .SetOnStateEnter(entity => 
                {
                    Dash.Lock = true;
                    entity.CurrentDashOffsetSec = 0f;
                    entity.CurrentDashTimeSec = 0f;
                    entity.Rigidbody.velocity = Vector2.zero;
                    entity.LastDashVector = entity.transform.localScale.x;
                    
                }
            )
            .SetStateLogic(entity =>
                {
                    entity.CurrentDashTimeSec += Time.deltaTime;
                    float vector = 
                        Mathf.Sign(entity.CurrentDashTimeSec > entity.UncontrollableDashTimeSec
                            ? Input.GetAxis("Horizontal")
                            : entity.LastDashVector);
                    
                    entity.Rigidbody.velocity = Vector2.right * entity.DashPower * vector;

                    Dash.Lock = entity.CurrentDashTimeSec < entity.DashTimeMilliAll;
                }
            );

        //public static State<Player> Attack1 = new State<Player>("Attack1", (ulong) StatePriority.Attack1)
        //    .SetOnStateEnter(entity => Attack1.Lock = true)
        //    .SetStateLogic(entity => 
        //    {
        //        float firstAttackDelayTest = 2.0f;
        //    })
        //    .SetEnterCondition(entity => true)
        //    .AddModifier(gam)
        //    .AddModifier(sm);

        //public static State<Player> Attack2 = new State<Player>("Attack2", (ulong) StatePriority.Attack2)
        //    .SetOnStateEnter(entity => 
        //        { 
                    
        //            Attack2.Lock = true; 
        //        } 
        //    )
        //    .SetStateLogic(entity => 
        //        {
            
        //        }
        //    )
        //    .SetEnterCondition(entity => true)
        //    .AddModifier(gam)
        //    .AddModifier(sm);
        
        //public static State<Player> Attack3 = new State<Player>("Attack3", (ulong) StatePriority.Attack3)
        //    .SetOnStateEnter(entity => Attack3.Lock = true)
        //    .SetStateLogic(entity => 
        //    {
            
        //    })
        //    .SetEnterCondition(entity => true)
        //    .AddModifier(gam)
        //    .AddModifier(sm);
    }
}

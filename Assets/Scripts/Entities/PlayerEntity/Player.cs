using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Entities.PlayerEntity
{
    public class Player : MonoBehaviour
    {
        [Header("Velocity")]
        [SerializeField] public float Velocity = 5.0f;

        [Header("Acceleration")]
        [SerializeField] public float Acceleration = 2.0f;

        [Header("Decceleration")]
        [SerializeField] public float Decceleration = 2.0f;

        [Header("Jump height")]
        [SerializeField] public float JumpHeight = 5.0f;

        [Header("Max air jumps")]
        [SerializeField] public int MaxAirJumps = 1;

        [Header("Fall gravity scale")]
        [SerializeField] public float FallGravityScale = 2f;

        [Header("The time need to be passed before new dash in milliseconds")]
        [SerializeField] public float DashOffsetSec = 0.5f;

        [Header("How long player will be dashing without control in milliseconds")]
        [SerializeField] public float UncontrollableDashTimeSec = 0.2f;

        [Header("How long player will be dashing in milliseconds overall")]
        [SerializeField] public float DashTimeMilliAll = 0.4f;

        [Header("Dash power")]
        [SerializeField] public float DashPower = 8f;

        [Header("Stun duration in seconds")]
        [SerializeField] public float StunDuration = 2f;

        [Header("Knock back power")]
        [SerializeField] public float KnockBackPower = 25f;

        [Header("Ground Layer Mask")]
        [SerializeField] public LayerMask GroundMask;

        [HideInInspector] public Collider2D Collider;

        [HideInInspector] public Animation Animation;

        [HideInInspector] public Rigidbody2D Rigidbody;

        [HideInInspector] public GameObject AttackObjectTest;

        [HideInInspector] public int AirJumpsCounter = 0;

        [HideInInspector] public float CurrentDashTimeSec = 0f;

        [HideInInspector] public float CurrentDashOffsetSec = 0f;

        [HideInInspector] public float LastDashVector = 0f;

        [HideInInspector] public float CurrentStunDuration = 0f;

        StateMachine<Player> fsm;

        void Start()
        {
            
            Animation animation = gameObject.GetComponent<Animation>();
            if (animation) Animation = animation; else Debug.LogError(gameObject.name + " need Animation");

            Collider2D col = gameObject.GetComponent<Collider2D>();
            if (col) Collider = col; else Debug.LogError(gameObject.name + " need Collider2d");

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            if (rb) Rigidbody = rb; else Debug.LogError(gameObject.name + " need Rigidbody2d");

            GameObject attackArea = GameObject.Find("AttackArea");
            if (attackArea) AttackObjectTest = attackArea; else Debug.LogError("Need attack area object on: " + gameObject.name);

            LastDashVector = transform.localScale.x;

            fsm = new StateMachine<Player>(
                new StateManager<Player>(this)
                .AddState(StateMap.Idle)
                .AddState(StateMap.Move)
                .AddState(StateMap.Jump)
                .AddState(StateMap.Fall)
                .AddState(StateMap.MoveUp)
                .AddState(StateMap.Damage)
                .AddState(StateMap.Dash)
            );
        }

        void Update()
        {
            fsm.Run();
            //Debug.Log(fsm.CurrentState.Name);
        }
    }
}

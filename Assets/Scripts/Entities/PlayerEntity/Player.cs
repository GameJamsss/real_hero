using Assets.Scripts.StateMachine;
using Assets.Scripts.Utils;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Assets.Scripts.Entities.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Velocity")]
        [SerializeField] public float Velocity = 5.0f;

        [Header("Acceleration")]
        [SerializeField] public float Acceleration = 2.0f;

        [Header("Decceleration")]
        [SerializeField] public float Decceleration = 4.0f;

        [Header("Velocity power")]
        [SerializeField] public float VelocityPower = 4.0f;

        [Header("Jump height")]
        [SerializeField] public float JumpHeight = 5.0f;

        [Header("Max air jumps")]
        [SerializeField] public int MaxAirJumps = 1;

        [Header("Ground Layer Mask")]
        [SerializeField] public LayerMask GroundMask;

        [HideInInspector] public Collider2D Collider;

        [HideInInspector] public Rigidbody2D Rigidbody;

        [HideInInspector] public int AirJumpsCounter = 0;

        StateMachine<Player> fsm;

        void Start()
        {
            Collider2D col = gameObject.GetComponent<Collider2D>();
            if (col) Collider = col; else Debug.LogError("Player need Collider2d");

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            if (rb) Rigidbody = rb; else Debug.LogError("Player need Rigidbody2d");

            fsm = new StateMachine<Player>(
                new StateManager<Player>(this)
                .AddState(StateMap.Idle)
                .AddState(StateMap.Move)
                .AddState(StateMap.Jump)
                .AddState(StateMap.Fall)
                .AddState(StateMap.MoveUp)
            );
        }

        void Update()
        {
            fsm.Run();
            Debug.Log(fsm.CurrentState.Name);
        }
    }
}

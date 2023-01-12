using Assets.Scripts.StateMachine;
using Assets.Scripts.Entities.Player.StateImpl;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class TestPlayer : MonoBehaviour
    {
        [Header("Velocity")]
        [SerializeField] public float Velocity = 5.0f;

        [Header("Jump height")]
        [SerializeField] public float JumpHeight = 5.0f;

        [Header("Ground Layer Mask")]
        [SerializeField] public LayerMask GroundMask;

        [HideInInspector] public Collider2D Collider;

        [HideInInspector] public Rigidbody2D Rigidbody;

        StateMachine<TestPlayer> fsm;

        void Start()
        {
            Collider2D col = gameObject.GetComponent<Collider2D>();
            if (col) Collider = col; else Debug.LogError("Player need Collider2d");

            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            if (rb) Rigidbody = rb; else Debug.LogError("Player need Rigidbody2d");

            fsm = new StateMachine<TestPlayer>(
                new StateManager<TestPlayer>(this)
                .AddState(new IdleState())
                .AddState(new RunState())
                .AddState(new JumpState())
                .AddState(new FallingDownState())
                .AddState(new MovingUpState())
                );
        }

        void Update()
        {
            fsm.Run();
            Debug.Log(Rigidbody.velocity.y);
            // Debug.LogError("fsm not set in main character: " + gameObject.name);
            Debug.Log(fsm.currentState.Name);
        }
    }
}

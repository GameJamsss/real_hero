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

        StateMachine<TestPlayer> fsm;

        void Start()
        {
            fsm = new StateMachine<TestPlayer>(
                new StateManager<TestPlayer>(this)
                .AddState(new IdleState())
                .AddState(new RunState())
                );
        }

        void Update()
        {
            if (fsm == null) fsm.Run();
        }
    }
}

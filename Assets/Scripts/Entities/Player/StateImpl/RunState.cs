using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.StateImpl
{
    internal class RunState : AbstractState<TestPlayer>
    {
        public RunState()
        {
            Name = "Run";
            _priority = 2;
        }

        public override void OnEnter(TestPlayer entity)
        {

        }

        public override void OnExit(TestPlayer entity)
        {

        }

        public override void OnUpdate(TestPlayer entity)
        {
            Vector3 direction = entity.transform.right * Input.GetAxis("Horizontal");
            entity.transform.position = Vector3.MoveTowards(entity.transform.position, entity.transform.position + direction, entity.Velocity * Time.deltaTime);
            Vector3 characterScale = entity.transform.localScale;
            if (Input.GetAxis("Horizontal") < 0)
            {
                characterScale.x = -1;

            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                characterScale.x = 1;
            }
            entity.transform.localScale = characterScale;
        }

        public override bool EnterCondition(TestPlayer entity)
        {
            Debug.Log(Input.GetButton("Horizontal"));
            return Input.GetButton("Horizontal");
        }
    }
}

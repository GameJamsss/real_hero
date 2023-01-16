using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.ModifierImpl
{
    public class GenericMoveModifier : StateModifier<TestPlayer>
    {
        public override void EnterModify(TestPlayer entity)
        {

        }

        public override void ExitModify(TestPlayer entity)
        {

        }

        public override void UpdateModify(TestPlayer entity)
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
    }
}

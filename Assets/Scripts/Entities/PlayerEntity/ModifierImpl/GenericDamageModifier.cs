using Assets.Scripts.Domain;
using Assets.Scripts.StateMachine;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities.PlayerEntity.ModifierImpl
{
    public class GenericDamageModifier : StateModifier<Player>
    {
        private int _damage = 1;

        public GenericDamageModifier(int damage)
        {
            _damage = damage;
        }

        public override void EnterModify(Player entity)
        {
            //List<Collider2D> list = new List<Collider2D>();
            //entity.AttackCollider.OverlapCollider(new ContactFilter2D().NoFilter(), list);
            //list.ForEach(col => {
            //    Attackable attackable = col.gameObject.GetComponent<Attackable>();
            //    if (attackable != null)
            //    {
            //        attackable.Hit(_damage);
            //    }
            //});
        }

        public override void ExitModify(Player entity)
        {
            
        }

        public override void UpdateModify(Player entity)
        {
            
        }
    }
}

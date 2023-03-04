using Assets.Scripts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    internal class DamageZone : MonoBehaviour, Damagable
    {
        [Header("Damage power")]
        [SerializeField] private int damagePower = 1;

        public void Disable()
        {
            gameObject.SetActive(false);    
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public int GetDamage()
        {
            return damagePower;
        }
    }
}

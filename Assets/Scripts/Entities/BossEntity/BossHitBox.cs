using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain;
using UnityEngine;

public class BossHitBox : MonoBehaviour{
    public Collider2D collider;
    public Action<int> damaged;

    private void Update(){
        List<Collider2D> colliders = new List<Collider2D>();
        collider.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
        GameObject goDamageZone = colliders
            .Select(col => col.gameObject)
            .ToList()
            .Find(dmg => dmg.name== "AttackArea");
        if (goDamageZone != null&&Time.timeScale!=0)
        {
            damaged?.Invoke(1);
        }
    }
}

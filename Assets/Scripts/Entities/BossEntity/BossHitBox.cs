using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain;
using UnityEngine;

public class BossHitBox : MonoBehaviour{
    public Collider2D collider;
    public Action<int> damaged;
    public SpriteRenderer blink;
    public AudioSource hit;

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
            StartCoroutine(Blink());
            hit.Play();
        }
    }
    private IEnumerator Blink()
    {
        blink.color=Color.red;
        Debug.Log("RED");
        // Ждем заданную длительность атаки
        yield return new WaitForSeconds(0.1f);
        // Уничтожаем объект атаки
        Debug.Log("WHITE");

        blink.color=Color.white;
    }

    private void OnDisable(){
        blink.color=Color.white;
    }
}

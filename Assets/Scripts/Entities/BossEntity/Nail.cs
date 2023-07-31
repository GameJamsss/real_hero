using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Domain;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nail : MonoBehaviour{
    public float additionalRotation = 90f; // Дополнительный угол поворота (90 градусов)
    public float speed;
    public Vector3 target;
    public bool isMove;
    public GameObject[] shoots;

    // private void OnCollisionEnter2D(Collision2D other){
    //     if (other.gameObject.layer == 3){
    //         int rand=Random.Range(0, shoots.Length);
    //         Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //         var obj = Instantiate(shoots[rand],position,Quaternion.identity);
    //     }
    //     Destroy(this.gameObject);
    // }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == 3){
            int rand=Random.Range(0, shoots.Length);
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            var obj = Instantiate(shoots[rand],position,Quaternion.identity);
        }
        StartCoroutine(DestroyNextFrameCoroutine());
        
    }

    private void Update()
    {
        if (isMove){
            // Поворот объекта в сторону игрока
            Vector3 directionToPlayer = target - transform.position;
            float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angleToPlayer+additionalRotation, Vector3.forward);

            // Движение объекта к игроку
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
    private IEnumerator DestroyNextFrameCoroutine()
    {
        yield return null; // Wait for the end of the current frame
        Destroy(gameObject);
    }
}

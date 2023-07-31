using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour{
    public SpriteRenderer sprite;
    public GameObject next;
    public int count;
    public bool right;

    public void Init(int count,bool isRight){
        this.count = count;
        this.right = isRight;
        sprite.flipX = isRight;
    }

    public void CreateEarth(){
        if (count > 0){
            count--;
            var offset = right ? 1.5f : -1.5f;
            Vector3 position = new Vector3(transform.position.x+offset, transform.position.y, transform.position.z);
            var obj = Instantiate(next,position,Quaternion.identity);
            obj.GetComponent<Earth>().Init(count,right);
        }
        Destroy(this.gameObject);
        }
    }

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Domain;
using UnityEngine;

public class Car : MonoBehaviour
{
    public void DestroyThis(){
        GameObject.Destroy(this.gameObject);
    }
}

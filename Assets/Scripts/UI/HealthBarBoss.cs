using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : MonoBehaviour{
    public Slider first;
    public event Action death;


    public void ChangeValue(int value){
        if (value <= 0){
            death?.Invoke();
        }
        first.value = (float )value / 100;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : MonoBehaviour{
    public Slider first;
    public Slider second;
    public event Action death;


    public void ChangeValue(int value){
        if (value <= 0){
            death?.Invoke();
        }
        if (value > 50){
            first.value = (float )(value - 50) / 100;
            second.value = 1;
        }
        else{
            first.value =0;
            second.value = (float )value / 100;
        }
    }
}

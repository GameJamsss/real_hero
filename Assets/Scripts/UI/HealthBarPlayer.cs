using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour{
    public Sprite[] sprites;
    public Image icon;
    public int currentImageIndex = 0;

    public event Action death;
    public void NextImage(int value){
        currentImageIndex=currentImageIndex+value;
        if (currentImageIndex >= sprites.Length)
        {
            death?.Invoke();
        }
        if (currentImageIndex >= 0 && currentImageIndex < sprites.Length)
        {
            icon.sprite = sprites[currentImageIndex];
        }
    }
}

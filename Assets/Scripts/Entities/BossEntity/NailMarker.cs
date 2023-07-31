using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailMarker : MonoBehaviour{
    public GameObject[] markers;

    public GameObject[] leftMarkers;
    public GameObject[] rightMarkers;


    public void AddBoth(){
        foreach (var leftMarker in leftMarkers){
            leftMarker.SetActive(true);
        }
        foreach (var rightMarker in rightMarkers){
            rightMarker.SetActive(true);
        }

    }
}

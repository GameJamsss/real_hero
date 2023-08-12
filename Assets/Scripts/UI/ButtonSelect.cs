using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void OnEnable()
    {
        button = GetComponent<Button>();
            button.Select();
    }
}

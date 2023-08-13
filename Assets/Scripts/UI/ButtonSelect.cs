using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
	private Button button;
	private ButtonTextColorChanger buttonSelectScript;

	// Start is called before the first frame update
	private void Awake()
	{
		button = GetComponent<Button>();
		buttonSelectScript = GetComponent<ButtonTextColorChanger>();
	}

	void OnEnable()
	{
		button.Select();
		// if (buttonSelectScript != null)
		// 	buttonSelectScript.OnSelect();
	}

	void OnDisable()
	{
		// if (buttonSelectScript != null)
		// 	buttonSelectScript.OnDeSelect();
	}

}

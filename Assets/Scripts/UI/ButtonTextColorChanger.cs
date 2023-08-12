using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class ButtonTextColorChanger : MonoBehaviour
{
	private Button button;
	private TextMeshProUGUI buttonText;

	[SerializeField]
	private Color normalColor = Color.white;
	[SerializeField]
	private Color hoverColor = new Color(1.0f, 0.298f, 0.298f); //#FF4C4C

	private string ButtonString;
	private float SizeText;

	private void Awake()
	{
		button = GetComponent<Button>();
		buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
		ButtonString = buttonText.text;
		SizeText = buttonText.fontSize;
	}

	public void OnSelect(){
		buttonText.text = $"<{ButtonString}>";
		buttonText.fontSize = SizeText * 1.2f;

	}
	public void OnDeSelect(){
		buttonText.text = $"{ButtonString}";
		buttonText.fontSize = SizeText;
	}

}

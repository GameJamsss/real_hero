using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Button button;
	private TextMeshProUGUI buttonText;

	[SerializeField]
	private Color normalColor = Color.white;
	[SerializeField]
	private Color hoverColor = new Color(1.0f, 0.298f, 0.298f); //#FF4C4C

	private void Awake()
	{
		button = GetComponent<Button>();
		buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		buttonText.color = hoverColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		buttonText.color = normalColor;
	}
}

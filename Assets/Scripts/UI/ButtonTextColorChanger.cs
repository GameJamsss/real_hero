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

	private Vector3 defaultScale;

	[SerializeField]
	private float scaleMultiplier = 1.2f;
	[SerializeField]
	private float animationDuration = 0.8f;

	private bool isAnimating = false;

	[SerializeField]
	private Color normalColor = Color.white;
	[SerializeField]
	private Color hoverColor = new Color(1.0f, 0.298f, 0.298f); //#FF4C4C

	private string ButtonString;
	private float SizeText;

	private Coroutine animationCoroutine;
	private bool shouldAnimate = false;

	public bool isSelected = false;

	private void Awake()
	{
		button = GetComponent<Button>();
		buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
		ButtonString = buttonText.text;
		SizeText = buttonText.fontSize;

		defaultScale = transform.localScale;
		button.onClick.AddListener(OnSelect);
	}

	private void OnEnable()
	{
		if (isSelected)
		{
			OnSelect();
		}
		if (shouldAnimate)
		{
			if (animationCoroutine != null)
			{
				StopCoroutine(animationCoroutine);
			}

			animationCoroutine = StartCoroutine(AnimateButton());
		}
	}

	public void OnSelect()
	{
		Debug.Log("BUTTON SELECTED NOW  - " + ButtonString);
		if (buttonText.text != null)
		{
			buttonText.text = $"< {ButtonString} >";
			buttonText.fontSize = SizeText * 1.2f;
		}


		shouldAnimate = true;

		if (animationCoroutine != null)
		{
			StopCoroutine(animationCoroutine);
		}
		if (gameObject.activeSelf && gameObject.activeInHierarchy)
		{
			animationCoroutine = StartCoroutine(AnimateButton());
		}


	}
	public void OnDeSelect()
	{
		if (buttonText.text != null)
		{
			buttonText.text = $"{ButtonString}";
			buttonText.fontSize = SizeText;
		}


		shouldAnimate = false;

		if (animationCoroutine != null)
		{
			StopCoroutine(animationCoroutine);
			animationCoroutine = null;

			transform.localScale = defaultScale; // Возвращаем размер кнопки к исходному
		}
	}

	private void OnDisable()
	{
		OnDeSelect();
	}

	private IEnumerator AnimateButton()
	{
		while (true)
		{
			Vector3 targetScale = defaultScale * scaleMultiplier;

			// Увеличиваем размер кнопки
			float elapsedTime = 0f;
			while (elapsedTime < animationDuration)
			{
				transform.localScale = Vector3.Lerp(defaultScale, targetScale, elapsedTime / animationDuration);
				elapsedTime += Time.deltaTime;
				yield return null;
			}

			// Уменьшаем размер кнопки
			elapsedTime = 0f;
			while (elapsedTime < animationDuration)
			{
				transform.localScale = Vector3.Lerp(targetScale, defaultScale, elapsedTime / animationDuration);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}
	}

}

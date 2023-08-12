using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopersPanelManager : MonoBehaviour
{
	[SerializeField] private GameObject _backBtn;
	public delegate void DisableDelegate();
	public event DisableDelegate DisabledEvent;



	private void Start()
	{
		if (_backBtn != null)
		{
			_backBtn.GetComponent<ButtonTextColorChanger>().OnSelect();
		}
	}
	private void OnDisable()
	{
		DisabledEvent?.Invoke();
	}
}

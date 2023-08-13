using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
	[Header("Panels")]
	[SerializeField] private GameObject _mainPanel;
	[SerializeField] private GameObject _leaderboardPanel;
	[SerializeField] private GameObject _enterNamePanel;
	[SerializeField] private GameObject _developersPanel;


	[Header("MainMenu")]
	[SerializeField] private GameObject _startBtn;
	[SerializeField] private GameObject _developersButton;
	[SerializeField] private GameObject _leaderboardButton;

	[SerializeField] private GameObject _enterNameFirstButton;

	private bool _mainMenuStarted = false;

	private Dictionary<GameObject, GameObject> buttonsAndPanelMap = new Dictionary<GameObject, GameObject>();



	void Start()
	{
		_mainPanel.SetActive(true);
		_leaderboardPanel.SetActive(false);
		_enterNamePanel.SetActive(false);
		if (StaticData.isOpenEnterNameScreen)
		{
			_mainPanel.SetActive(false);
			_enterNamePanel.SetActive(true);
		}

		buttonsAndPanelMap[_startBtn] = _mainPanel;
		buttonsAndPanelMap[_developersButton] = _developersPanel;
		buttonsAndPanelMap[_leaderboardButton] = _leaderboardPanel;


		LeaderboardManager leaderBoard = _leaderboardPanel.GetComponent<LeaderboardManager>();
		leaderBoard.DisabledEvent += () => ResetMainMenuToCurrentBtn(_leaderboardPanel);

		DevelopersPanelManager developersPanel = _developersPanel.GetComponent<DevelopersPanelManager>();
		developersPanel.DisabledEvent += () => ResetMainMenuToCurrentBtn(_developersPanel);

	}


	private void ResetMainMenuToCurrentBtn(GameObject currentPanel)
	{
		foreach (var buttonPanelPair in buttonsAndPanelMap)
		{
			GameObject button = buttonPanelPair.Key;
			GameObject panel = buttonPanelPair.Value;

			if (panel != currentPanel)
			{
				button.GetComponent<ButtonTextColorChanger>().OnDeSelect();
			}
			else
			{
				button.GetComponent<ButtonTextColorChanger>().OnSelect();
				EventSystem.current.SetSelectedGameObject(button);
			}
		}
	}

	public void OpenEnterName()
	{
		_mainPanel.SetActive(false);
		_enterNamePanel.SetActive(true);
		EventSystem.current.SetSelectedGameObject(null);
		//�� ����� ���� �������� ����������
		EventSystem.current.SetSelectedGameObject(_enterNameFirstButton);
	}

	public void StartGame()
	{
		//���� ��� ������ ������, �� ������ ������
		SceneManager.LoadScene(1);
	}

	public void OpenLeaderboard()
	{
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(_leaderboardButton);

		_mainPanel.SetActive(false);
		_leaderboardPanel.SetActive(true);
	}

	//MainPanel
	public void OpenMainMenu()
	{
		Debug.Log("OpenMainMenu");
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(_startBtn);


		_mainPanel.SetActive(true);
		_leaderboardPanel.SetActive(false);
		_enterNamePanel.SetActive(false);
	}
}

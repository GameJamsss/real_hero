using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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


		LeaderboardManager leaderBoard = _leaderboardPanel.GetComponent<LeaderboardManager>();
		leaderBoard.DisabledEvent += ResetMainMenuToCurrentBtn;

		DevelopersPanelManager developersPanel = _developersPanel.GetComponent<DevelopersPanelManager>();
		developersPanel.DisabledEvent += ResetMainMenuToCurrentBtn;

	}


	private void ResetMainMenuToCurrentBtn()
	{
		_startBtn.GetComponent<ButtonTextColorChanger>().OnDeSelect();
		_developersButton.GetComponent<ButtonTextColorChanger>().OnDeSelect();
		_leaderboardButton.GetComponent<ButtonTextColorChanger>().OnDeSelect();
		_startBtn.GetComponent<ButtonTextColorChanger>().OnSelect();
		EventSystem.current.SetSelectedGameObject(_startBtn);
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

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

    [Header("Navigation")]
    [SerializeField] private GameObject _mainMenuFirstButton;
    [SerializeField] private GameObject _leaderboardFirstButton;
    [SerializeField] private GameObject _enterNameFirstButton;

    void Start()
    {
        _mainPanel.SetActive(true);
        _leaderboardPanel.SetActive(false);
        _enterNamePanel.SetActive(false);
        if (StaticData.isOpenEnterNameScreen){
            _enterNamePanel.SetActive(true);
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
        EventSystem.current.SetSelectedGameObject(_leaderboardFirstButton);

        _mainPanel.SetActive(false);
        _leaderboardPanel.SetActive(true);
    }

    //MainPanel
    public void OpenMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_mainMenuFirstButton);

        _mainPanel.SetActive(true);
        _leaderboardPanel.SetActive(false);
        _enterNamePanel.SetActive(false);
    }
}

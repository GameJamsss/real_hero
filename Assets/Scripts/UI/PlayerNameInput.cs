using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameText;
    [SerializeField] private GameObject _errorPanel;
    [SerializeField] private TextMeshProUGUI _errorText;
    [SerializeField] private GameObject _errorFirstButton;
    [SerializeField] private MainMenu _mainMenu;

    private HashSet<string> _forbiddenWords = new HashSet<string>() { "сука", "блять", "хуй", "пизда", "ебать" }; //i'm sorry :c

    private void OnEnable()
    {
        _errorPanel.SetActive(false);
        _nameText.text = "";
    }

    public void SentKeySymbol(string symbol)
    {
        if (_nameText.text.Length <= 16)
            _nameText.text += symbol;
    }

    public void DeleteLaastSymbol()
    {
        if (_nameText.text.Length > 0)
            _nameText.text = _nameText.text.Remove(_nameText.text.Length - 1);
    }

    public void ConfirmPlayerName()
    {
        if (_nameText.text.Length == 0)
        {
            SetErrorPanelActive("Имя не может быть пустым");
        }
        else if (_forbiddenWords.Contains(_nameText.text.ToLowerInvariant()))
        {
            SetErrorPanelActive("Нельзя использовать ненормативную лексику");
        }
        else
        {
            _mainMenu.StartGame();
        }
    }

    private void SetErrorPanelActive(string msg)
    {
        _errorText.text = msg;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_errorFirstButton);
        _errorPanel.SetActive(true);
    }
}

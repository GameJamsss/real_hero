using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour
{
    [SerializeField] private PlayerNameInput _playerInput;
    private string _symbol;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        //_text.text = _symbol;
        _symbol = _text.text;
    }

    private void Start()
    {
        if (_playerInput == null)
            _playerInput = FindObjectOfType<PlayerNameInput>();
    }

    public void SentKey() => _playerInput.SentKeySymbol(_symbol);
}

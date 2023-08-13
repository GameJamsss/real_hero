using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class SkillCheck : MonoBehaviour
{
    [SerializeField] private GameObject _sliderCheck;
    [SerializeField] private GameObject _sliderPlayer;

    [SerializeField] private float extraError = 0.2f; //0-1f

    [SerializeField] private float minCheckValue = 0.4f;
    [SerializeField] private float maxCheckValue = 1f;

    [SerializeField] private Color _defaultBgColor;
    [SerializeField] private Color _successBgColor;
    [SerializeField] private Color _failBgColor;

    [SerializeField] private Image _bgImage;

    public UnityEvent<bool> OnSkillCheckResultSetted;

    private Slider _checkSlider;
    private Slider _playerSlider;

    private Animator _animator;

    private bool _isActive;

    void Awake()
    {
        _checkSlider = _sliderCheck.GetComponent<Slider>();
        _playerSlider= _sliderPlayer.GetComponent<Slider>();

        _checkSlider.value = 0f;
        _playerSlider.value = 0f;

        _bgImage.color = _defaultBgColor;

        HideSkillCheck();

        _animator = GetComponent<Animator>();
    }

    //Use for checking
/*    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckPlayerSkill();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            _isActive = false;
            StartSkillCheck();
        }
    }*/

    //Start skill check
    public void StartSkillCheck()
    {
        if (_isActive) //already active
            return;

        _isActive = true;
        _bgImage.color = _defaultBgColor;
        ActivateSliders();
        SetCheckSliderRandomValue();
        StartPlayerSliderAnim();
    }

    //When player press a button while skill check is active
    //true - success, false - failed
    public bool CheckPlayerSkill()
    {
        if (!_isActive)
            return false;

        var playerValue = _playerSlider.value;
        var checkValue = _checkSlider.value;

        var leftBorder = playerValue - extraError;
        var rightBorder = playerValue + extraError;

        var result = leftBorder <= checkValue && rightBorder >= checkValue; ;
        OnSkillCheckResultSetted?.Invoke(result);

        _bgImage.color = result ? _successBgColor : _failBgColor;

        _animator.SetTrigger("Idle");

        return result;
    }

    public void HideSkillCheck()
    {
        _sliderCheck.SetActive(false);
        _sliderPlayer.SetActive(false);
        _isActive = false;
    }

    //Call from anim
    //The indicator reached the end and the player didn't press a key - fail
    public void FinishSkillCheckWithFail()
    {
        OnSkillCheckResultSetted?.Invoke(false);
        _bgImage.color = _failBgColor;
        _animator.SetTrigger("Idle");
        Debug.Log("Faudss");
    }

    public bool IsActive()
    {
        return _isActive;
    }

    private void ActivateSliders()
    {
        _sliderCheck.SetActive(true);
        _sliderPlayer.SetActive(true);
    }

    private void StartPlayerSliderAnim()
    {
        _animator.SetTrigger("Start");
    }

    private void SetCheckSliderRandomValue()
    {
        _checkSlider.value = Random.Range(minCheckValue, maxCheckValue);
    }
}
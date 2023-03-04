using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameMenuManager : MonoBehaviour
{
    [HideInInspector] public static GameMenuManager Instance { get; private set; }

    [Header("Panels")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _newRecordPanel;
    [SerializeField] private GameObject _defeatPanel;

    [Space(20)]
    [Header("Points and Timer")]
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _hits;
    [SerializeField] private float _hitsCooldownValue = 3f;
    [SerializeField] private TextMeshProUGUI _points;

    private AudioSource _audioSource;

    private float _curTimer;
    private int _curHitsCount = 1;
    private bool _wasHit;
    private int _curPoints;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        HideAllPanels();

        _points.text = _curPoints.ToString();
        _hits.text = "x1";
        _timer.text = "0:0";

        InvokeRepeating("SubtractHits", 0f, _hitsCooldownValue);
    }

    //delete
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AddPointsAndHits(1, 1);

        _timer.text = _curTimer.ToString(); //запихнуть в корутин с таймером
    }

    private GameMenuManager() { }

    public void AddPointsAndHits(int points, int hits = 1)
    {
  
        _curHitsCount += hits;
        _curPoints += (points * _curHitsCount);
        HitsCooldown();

        _points.text = _curPoints.ToString();
        _hits.text = "x" + _curHitsCount.ToString();
    }

    public void ShowWinPanel()
    {
        HideAllPanels();
        _winPanel.SetActive(true);
    }

    public void ShowPausePanel()
    {
        HideAllPanels();
        _pausePanel.SetActive(true);
    }

    public void ShowNewRecordPanel()
    {
        HideAllPanels();
        _newRecordPanel.SetActive(true);
    }

    public void ShowDefeatPanel()
    {
        HideAllPanels();
        _defeatPanel.SetActive(true);
    }

    private void HideAllPanels()
    {
        _winPanel.SetActive(false);
        _pausePanel.SetActive(false);
        _newRecordPanel.SetActive(false);
        _defeatPanel.SetActive(false);
    }

    private void HitsCooldown()
    {
        StartCoroutine(StartCoolDownHits());
    }

    private IEnumerator StartCoolDownHits()
    {
        _wasHit = true;
        yield return new WaitForSeconds(_hitsCooldownValue);
        _wasHit = false;
    }

    private void SubtractHits()
    {
        if (!_wasHit)
        {
            _curHitsCount--;
            if (_curHitsCount < 1)
                _curHitsCount = 1;

            _hits.text = "x" + _curHitsCount.ToString();
        }
    }
}

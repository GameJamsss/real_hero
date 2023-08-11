using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities.BossEntity;
using Assets.Scripts.Entities.PlayerEntity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    public Player player;
    public Boss boss;
    public float time=0;
    public bool pause;
    public GameObject pauseText;

    public GameMenuManager uiManager;

    private void Start(){
        Time.timeScale = 1;

        Sub();
    }

    public void Sub(){
        player.damaged += uiManager.playerHealthBar.NextImage;
        uiManager.playerHealthBar.death += player.Death;
        uiManager.playerHealthBar.death += uiManager.ShowDefeatPanel;

        boss.damaged += uiManager.bossHealthBar.ChangeValue;
        uiManager.bossHealthBar.death += boss.Death;
        uiManager.bossHealthBar.death += uiManager.ShowWinPanel;

    }

    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void AfterWin(){
        Time.timeScale = 1;
        StaticData.isOpenEnterNameScreen = true;
        StaticData.isCurrentTime=FormatTime(time);
        SceneManager.LoadScene(0);
    }

    
    private void Update(){
            time = time + Time.deltaTime;
            uiManager._timer.text = FormatTime(time);
            uiManager._resultTimer.text = FormatTime(time);
            if (Input.GetKeyDown(KeyCode.P)){
                pause = !pause;
                if (pause){
                    Time.timeScale = 0;
                    pauseText.SetActive(true);
                }
                else{
                    Time.timeScale = 1;
                    pauseText.SetActive(false);
                }
            }

    }
    
    public static string FormatTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        string formattedTime = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        return formattedTime;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities.BossEntity;
using Assets.Scripts.Entities.PlayerEntity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Player player;
	public Boss boss;
	public float time;

	public GameMenuManager uiManager;

	private void Start()
	{
		Time.timeScale = 1;

		Sub();
	}

	public void Sub()
	{
		player.damaged += uiManager.playerHealthBar.NextImage;
		uiManager.playerHealthBar.death += uiManager.ShowDefeatPanel;
		boss.damaged += uiManager.bossHealthBar.ChangeValue;
		uiManager.bossHealthBar.death += uiManager.ShowWinPanel;

	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1;
	}

	private void Update()
	{
		time = time + Time.deltaTime;
		Debug.Log("TIME " + FormatTime(time));
		uiManager._timer.text = FormatTime(time);
		uiManager._resultTimer.text = FormatTime(time);
		// uiManager.ShowDefeatPanel();
	}

	public static string FormatTime(float timeInSeconds)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
		string formattedTime = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
		return formattedTime;
	}
}

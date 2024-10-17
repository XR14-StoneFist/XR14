using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	public Canvas MainCanvas;
	public GameObject DashArrowPrefab;

	[Header("StartUI")]
	public GameObject StartUI;
	public Button StartUI_GameStartButton;
	public Button StartUI_SettingsButton;
	public Button StartUI_ExitButton;

	[Header("SettingsUI")]
	public GameObject SettingsUI;

	[Header("PlayUI")]
	public GameObject PlayUI;
	
	[Header("GameOverUI")]
	public GameObject GameOverUI;
	public Button GameOverUI_RestartButton;
	public Button GameOverUI_ExitButton;
	
	private void Start()
	{
		StartUI_GameStartButton.onClick.AddListener(OnClickGameStartButton);
		StartUI_SettingsButton.onClick.AddListener(OnClickSettingsButton);
		StartUI_ExitButton.onClick.AddListener(OnClickExitButton);
		
		GameOverUI_RestartButton.onClick.AddListener(OnClickRestartButton);
		GameOverUI_ExitButton.onClick.AddListener(OnClickExitButton);

		Time.timeScale = 0;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (SettingsUI.activeSelf)
			{
				SettingsUI.SetActive(false);
				StartUI.SetActive(true);
			}
		}

		if (GameManager.Instance.Health <= 0)
		{
			Time.timeScale = 0;
			PlayUI.SetActive(false);
			GameOverUI.SetActive(true);
		}
	}

	public GameObject CreateDashArrow(Vector3 objectPosition)
	{
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(objectPosition);
		return Instantiate(DashArrowPrefab, screenPosition, Quaternion.identity, MainCanvas.transform);
	}

	public void MoveDashArrow(GameObject dashArrowObject, Vector3 objectPosition)
	{
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(objectPosition);
		dashArrowObject.transform.position = screenPosition;
	}

	private void OnClickGameStartButton()
	{
		Time.timeScale = 1;
		PlayUI.SetActive(true);
		StartUI.SetActive(false);
	}

	private void OnClickSettingsButton()
	{
		SettingsUI.SetActive(true);
		StartUI.SetActive(false);
	}

	private void OnClickExitButton()
	{
		Application.Quit();
	}

	private void OnClickRestartButton()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}

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
	public Button RestartButton;
	public TMP_Text PlayerStateText;
	public PlayerStateMachine PlayerStateMachine;

	private void Start()
	{
		RestartButton.onClick.AddListener(OnClickRestartButton);
	}

	private void Update()
	{
		PrintPlayerState();
	}

	private void OnClickRestartButton()
	{
		Debug.Log("OnClickRestartButton");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public GameObject CreateDashArrow(Vector3 objectPosition)
	{
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(objectPosition);
		return Instantiate(DashArrowPrefab, screenPosition, Quaternion.identity, MainCanvas.transform);
	}

	public void PrintPlayerState()
	{
		PlayerStateText.text = "Current State: " + PlayerStateMachine.CurrentState.GetType().Name;
	}
}

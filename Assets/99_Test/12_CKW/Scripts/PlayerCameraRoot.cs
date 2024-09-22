using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRoot : MonoBehaviour
{
	[SerializeField] private GameObject player;

	private Vector3 offset;
	
	private void Awake()
	{
		offset = transform.position - player.transform.position;
	}

	private void Update()
	{
		transform.position = player.transform.position + offset;
	}
}

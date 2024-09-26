using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDetector : MonoBehaviour
{
	[SerializeField] private JTH_PlayerMove _player;
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Flame"))
		{
			if (_player.DashFlame == null)
				_player.DashFlame = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Flame"))
		{
			if (_player.DashFlame == other.gameObject)
				_player.DashFlame = null;
		}
	}
}

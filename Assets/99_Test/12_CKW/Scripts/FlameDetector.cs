using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDetector : MonoBehaviour
{
	[SerializeField] private CustomThirdPersonController _controller;
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Flame"))
		{
			if (_controller.DashFlame == null)
				_controller.DashFlame = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Flame"))
		{
			if (_controller.DashFlame == other.gameObject)
				_controller.DashFlame = null;
		}
	}
}

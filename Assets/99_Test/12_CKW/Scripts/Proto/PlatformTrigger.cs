using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
	[SerializeField] private GameObject platform;
	[SerializeField] private Transform movePosition;
	[SerializeField] private float speed;

	private Vector3 _origin;
	private Vector3 _destination;
	private float _distance;
	private float _lerpPosition;
	private bool _isMoving;
	
	private void Start()
	{
		_origin = platform.transform.position;
		_destination = movePosition.position;
		_lerpPosition = 0;
	}

	private void Update()
	{
		if (_isMoving)
		{
			_distance = Vector3.Distance(_origin, _destination);
			if (_lerpPosition >= 1)
			{
				_isMoving = false;
			}
			else
			{
				_lerpPosition += speed * Time.deltaTime / _distance;
				platform.transform.position = Vector3.Lerp(_origin, _destination, EaseOutQuint(_lerpPosition));
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			_isMoving = true;
		}
	}

	private float EaseOutQuint(float value)
	{
		return 1 - Mathf.Pow(1 - value, 5);
	}
}

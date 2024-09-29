using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("점프")]
	public SubCollider JumpBox;
	public float JumpPower;
	
	public Rigidbody RigidbodyComponent { get; private set; }
	
	public bool IsGrounded { get; set; }

	private void Awake()
	{
		RigidbodyComponent = GetComponent<Rigidbody>();
		JumpBox.OnTriggerEnterAction += tagName =>
		{
			if (tagName == "Ground")
			{
				IsGrounded = true;
			}
		};
	}
	
}

using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GolemController : MonoBehaviour
{
	[Header("Player")]
	public float MoveSpeed;
	[Range(0.0f, 0.3f)]
	public float RotationSmoothTime = 0.12f;
	public float JumpHeight = 1.2f;
	public float Gravity = -15.0f;

	[Header("Animation")]
	public AudioClip LandingAudioClip;
	public AudioClip[] FootstepAudioClips;
	[Range(0, 1)] public float FootstepAudioVolume = 0.5f;
	
	private bool _shouldMove;
	private Vector3 _targetPosition;
	[SerializeField] private bool _isGrounded;
	private float _groundedOffset = -0.14f;
	private float _groundedRadius = 0.28f;
	private LayerMask _groundLayers;
	private float _jumpTimeoutDelta;
	private float _fallTimeoutDelta;
	private float _jumpTimeout = 0.5f;
	private float _fallTimeout = 0.15f;
	private float _verticalVelocity;
	private float _terminalVelocity = 53.0f;



	private Animator _animator;
	private CharacterController _controller;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController>();
	}

	private void Start()
	{
		_shouldMove = false;
		_targetPosition = transform.position;
		_animator.SetFloat("MotionSpeed", 1);
		_groundLayers = LayerMask.NameToLayer("TransparentFX");
		_jumpTimeoutDelta = _jumpTimeout;
		_fallTimeoutDelta = _fallTimeout;
	}

	private void Update()
	{
		JumpAndGravity();
		GroundedCheck();
		if (Input.GetMouseButtonDown(1))
		{
			Vector3 mousePosition = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay(mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit))
			{
				_shouldMove = true;
				_targetPosition = new Vector3(hit.point.x, hit.point.y, transform.position.z);
				
				_animator.SetFloat("Speed", 2);
			}
		}

		if (_verticalVelocity != 0.0f)
		{
			_controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}
		
		if (_shouldMove)
		{
			// 골렘 회전
			float rotationVelocity = 0f;
			float rotateDirection = Mathf.Sign(_targetPosition.x - transform.position.x);
			float targetRotation = Mathf.Atan2(rotateDirection, 0) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
			float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, RotationSmoothTime);
			transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			
			// 골렘 이동
			Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
			_controller.Move(targetDirection.normalized * (MoveSpeed * Time.deltaTime));
			if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
			{
				_shouldMove = false;
				_animator.SetFloat("Speed", 0);
			}
		}
	}

	private void GroundedCheck()
	{
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z);
		_isGrounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);
		_animator.SetBool("Grounded", _isGrounded);
	}

	private void JumpAndGravity()
	{
		if (_isGrounded)
        {
			_fallTimeoutDelta = _fallTimeout;

			_animator.SetBool("Jump", false);
			_animator.SetBool("FreeFall", false);

			if (_verticalVelocity < 0.0f)
				_verticalVelocity = -2f;

			if (Input.GetMouseButtonDown(0) && _jumpTimeoutDelta <= 0.0f)
			{
				_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				_animator.SetBool("Jump", true);
			}

			if (_jumpTimeoutDelta >= 0.0f)
			{
				_jumpTimeoutDelta -= Time.deltaTime;
			}
		}
		else
		{
			_jumpTimeoutDelta = _jumpTimeout;
			
			if (_fallTimeoutDelta >= 0.0f)
			{
				_fallTimeoutDelta -= Time.deltaTime;
			}
			else
			{
				_animator.SetBool("FreeFall", true);
			}
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}
	}

		private void OnFootstep(AnimationEvent animationEvent)
	{
		if (animationEvent.animatorClipInfo.weight > 0.5f)
		{
			if (FootstepAudioClips.Length > 0)
			{
				var index = Random.Range(0, FootstepAudioClips.Length);
				AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
			}
		}
	}

	private void OnLand(AnimationEvent animationEvent)
	{
		if (animationEvent.animatorClipInfo.weight > 0.5f)
		{
			AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
		}
	}
}
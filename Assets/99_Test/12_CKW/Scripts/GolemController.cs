using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GolemController : MonoBehaviour
{
	[Header("Player")]
	public float MoveSpeed;
	public float GrabSpeed;
	[Range(0.0f, 0.3f)]
	public float RotationSmoothTime = 0.12f;
	public float JumpHeight = 1.2f;
	public float Gravity = -15.0f;
	public float SpeedChangeRate = 10.0f;
	public float GrabRange = 0.5f;
	public LayerMask[] GroundLayers;
	
	[Header("Animation")]
	public AudioClip LandingAudioClip;
	public AudioClip[] FootstepAudioClips;
	[Range(0, 1)] public float FootstepAudioVolume = 0.5f;
	
	private bool _shouldMove;
	private Vector3 _targetPosition;
	[SerializeField] private bool _isGrounded;
	private float _groundedOffset = -0.14f;
	private float _groundedRadius = 0.28f;
	private float _jumpTimeoutDelta;
	private float _fallTimeoutDelta;
	private float _jumpTimeout = 0.5f;
	private float _fallTimeout = 0.15f;
	private float _verticalVelocity;
	private float _terminalVelocity = 53.0f;
	private float _animationBlend;
	private GameObject _clickedObject;
	private GameObject _grabbedObject;
	private bool _isGrabbing;
	private float _sideThreshold = 0.5f;
	private float _coordZ;



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
		_jumpTimeoutDelta = _jumpTimeout;
		_fallTimeoutDelta = _fallTimeout;
		_coordZ = transform.position.z;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay(mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				_clickedObject = hit.collider.gameObject;
			}
		}
		
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
			}
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			if (_isGrabbing)
			{
				Debug.Log("Grab Off");
				_targetPosition = transform.position;
				_isGrabbing = false;
				_grabbedObject.layer = LayerMask.NameToLayer("Default");
				_grabbedObject = null;
			}
			else
			{
				if (CanGrab(_clickedObject))
				{
					Debug.Log("Grab On");
					_isGrabbing = true;
					_grabbedObject = _clickedObject;
					_grabbedObject.layer = LayerMask.NameToLayer("Grabbed");
				}
			}
		}

		if (_verticalVelocity != 0.0f)
		{
			_controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}

		float targetSpeed = _isGrabbing ? GrabSpeed : MoveSpeed;
		targetSpeed = _shouldMove ? targetSpeed : 0;
		
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
			if (_isGrabbing)
			{
				float moveDistance = targetSpeed * Time.deltaTime;
				Collider objectCollider = _grabbedObject.GetComponent<Collider>();
				Vector3 rayStart = objectCollider.ClosestPoint(_targetPosition);
				Vector3 rayDirection = targetDirection.normalized;
				
				RaycastHit hit;
				if (Physics.Raycast(rayStart, rayDirection, out hit, moveDistance))
				{
					Debug.Log("DistanceToHit!");
					float distanceToHit = hit.distance;
					if (distanceToHit > 0.01f)
					{
						_controller.Move(rayDirection * distanceToHit);
						_grabbedObject.transform.Translate(rayDirection * distanceToHit);
					}
					_shouldMove = false;
				}
				else
				{
					_controller.Move(rayDirection * moveDistance);
					_grabbedObject.transform.Translate(rayDirection * moveDistance);
				}
			}
			else
			{
				_controller.Move(targetDirection.normalized * (targetSpeed * Time.deltaTime));
			}
			if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
				_shouldMove = false;
		}
		
		_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
		if (_animationBlend < 0.01f) _animationBlend = 0f;
		_animator.SetFloat("Speed", _animationBlend);

		transform.position = new Vector3(transform.position.x, transform.position.y, _coordZ);
	}

	private void GroundedCheck()
	{
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z);

		int combinedLayer = 0;
		foreach (int layer in GroundLayers)
			combinedLayer += layer;
		
		_isGrounded = Physics.CheckSphere(spherePosition, _groundedRadius, combinedLayer, QueryTriggerInteraction.Ignore);
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

			if (Input.GetMouseButtonDown(0))
			{
				if (CanGrab(_clickedObject) == false && _grabbedObject == null)
				{
					if (_jumpTimeoutDelta <= 0.0f)
					{
						_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
						_animator.SetBool("Jump", true);
					}
				}
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

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.CompareTag("Obstacle"))
		{
			Vector3 hitNormal = hit.normal;
			float dotProduct = Vector3.Dot(Vector3.up, hitNormal);
			if (dotProduct > -_sideThreshold && dotProduct < _sideThreshold)
			{
				_targetPosition = transform.position;
			}
		}
	}

	private bool CanGrab(GameObject target)
	{
		if (target != null)
		{
			if (target.GetComponent<MovableObstacle>() != null)
			{
				Collider targetCollider = target.GetComponent<Collider>();
				if (targetCollider != null)
				{
					Vector3 closestPoint = targetCollider.ClosestPoint(transform.position);
					float distance = Vector3.Distance(transform.position, closestPoint);
					if (distance <= GrabRange) return true;
				}
			}
		}

		return false;
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
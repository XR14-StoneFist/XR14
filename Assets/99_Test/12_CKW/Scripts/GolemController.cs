using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class GolemController : MonoBehaviour
{
	[Header("Player")]
	public float MoveSpeed;
	[Range(0.0f, 0.3f)]
	public float RotationSmoothTime = 0.12f;
	
	[Header("Animation")]
	public AudioClip LandingAudioClip;
	public AudioClip[] FootstepAudioClips;
	[Range(0, 1)] public float FootstepAudioVolume = 0.5f;
	
	private bool _shouldMove;
	private Vector3 _targetPosition;
	
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
				_shouldMove = true;
				_targetPosition = new Vector3(hit.point.x, hit.point.y, transform.position.z);
				
				_animator.SetFloat("Speed", 2);
			}
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
			// transform.position = Vector3.MoveTowards(transform.position, _targetPosition, MoveSpeed * Time.deltaTime);
			Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
			_controller.Move(targetDirection.normalized * (MoveSpeed * Time.deltaTime) +
			                 new Vector3(0.0f, 0f, 0.0f) * Time.deltaTime);
			if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
			{
				_shouldMove = false;
				_animator.SetFloat("Speed", 0);
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
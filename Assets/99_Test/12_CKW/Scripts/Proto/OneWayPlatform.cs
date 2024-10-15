using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
	private Collider platformCollider;
	public float bufferDistance = 0.1f;  // 충돌을 허용하기 전의 거리 차이

	void Start()
	{
		platformCollider = GetComponent<Collider>();
	}

	void Update()
	{
		// 플레이어 오브젝트를 지속적으로 감지하고, 플랫폼과 충돌 감지
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null)
		{
			CheckPlatformCollision(player);
		}
	}

	void CheckPlatformCollision(GameObject player)
	{
		// 플레이어의 위치와 플랫폼의 위치
		Vector3 playerPosition = player.transform.position;
		Vector3 platformPosition = transform.position;

		// 플레이어의 속도를 가져오기 위한 CharacterController
		CharacterController characterController = player.GetComponent<CharacterController>();
		if (characterController == null) return;

		// 플레이어가 점프 중이거나 플랫폼 위로 올라가려는 중인지 확인
		if (playerPosition.y > platformPosition.y + bufferDistance && characterController.velocity.y <= 0)
		{
			// 플레이어가 플랫폼보다 위에 있고, 내려가는 중일 때만 충돌 허용
			Physics.IgnoreCollision(player.GetComponent<Collider>(), platformCollider, false);
		}
		else
		{
			// 플레이어가 점프 중이거나 플랫폼보다 아래에 있을 때는 충돌 무시
			Physics.IgnoreCollision(player.GetComponent<Collider>(), platformCollider, true);
		}
	}
}
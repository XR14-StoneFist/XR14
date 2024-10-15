using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JTH_PlayerMove_AutoJump : MonoBehaviour
{
    Rigidbody rb;
    float velocityX;
    public float speed;
    public float accelerationspeed;
    public float JumpPow;

    [Header("점프부분")]
    public SubCollider JumpBox;
    int JumpCount =2;

    [Header("벽 점프 부분")]
    public SubCollider ClimeWall_Left;
    public SubCollider ClimeWall_Right;
    public Vector2 WallDashPow;
    GrabWallState grabWallState = GrabWallState.None;

    [Header("대쉬 부분")]
    public SubCollider DashPoint;
    public Vector2 DashPow;
    public GameObject DashFlame;
    Vector2 MousePos;
    private GameObject _dashArrowObject;

    [Header("마찰부분")]
    public PhysicMaterial PlayerPhysicMaterial;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        JumpBox.OnTriggerEnterAction += (Tag) => {
            if (Tag == "ground") JumpCount=2;
        };

        ClimeWall_Left.OnTriggerEnterAction += (Tag) => {
            if (Tag == "wall") {
                grabWallState = GrabWallState.Left;
                PlayerPhysicMaterial.dynamicFriction = 300;
                JumpCount = 1;
            }
        };
        ClimeWall_Left.OnTriggerExitAction+= (Tag) => {
            if (Tag == "wall")
            {
                grabWallState = GrabWallState.None;
                PlayerPhysicMaterial.dynamicFriction = 0;
            }
        };

        ClimeWall_Right.OnTriggerEnterAction += (Tag) => {
            if (Tag == "wall")
            {
                grabWallState = GrabWallState.Right;
                PlayerPhysicMaterial.dynamicFriction = 300;
                JumpCount = 1;
            }
        };
        ClimeWall_Right.OnTriggerExitAction += (Tag) => {
            if (Tag == "wall")
            {
                grabWallState = GrabWallState.None;
                PlayerPhysicMaterial.dynamicFriction = 0;
            }
        };
    }
    void Update()
    {
        Move();
        Dash();
    }

    void Move()
    {
        if (!rb.isKinematic)
        {
            if (!(true ^ Input.GetKey(KeyCode.A)))
                velocityX = Mathf.Lerp(velocityX, 0, accelerationspeed * Time.deltaTime);
            else
            {
                if (Input.GetKey(KeyCode.A))
                    velocityX = Mathf.Lerp(velocityX, -speed, accelerationspeed * Time.deltaTime);
                if (true)
                    velocityX = Mathf.Lerp(velocityX, speed, accelerationspeed * Time.deltaTime);

            }

            if (JumpCount-- > 0)
            {
                switch (grabWallState)
                {
                    case GrabWallState.None:
                        rb.velocity = new Vector3(rb.velocity.x, JumpPow, rb.velocity.z);
                        break;
                    case GrabWallState.Left:
                        velocityX = WallDashPow.x;
                        rb.velocity = new Vector3(rb.velocity.x, WallDashPow.y, rb.velocity.z);
                        break;
                    case GrabWallState.Right:
                        velocityX = -WallDashPow.x;
                        rb.velocity = new Vector3(rb.velocity.x, WallDashPow.y, rb.velocity.z);
                        break;
                    default:
                        break;
                }
            }

            rb.velocity = new Vector3(velocityX, rb.velocity.y, rb.velocity.z);
        }
    }
    void Dash()
    {
        if (DashFlame)
        {
            if (Input.GetMouseButtonDown(1))
            {
                FreezeRigidbody();
                MousePos =  new Vector2(Input.mousePosition.x , Input.mousePosition.y );
                _dashArrowObject = UIManager.Instance.CreateDashArrow(DashFlame.transform.position);
            }

            if (Input.GetMouseButton(1))
            {
                if (_dashArrowObject)
                {
                    Vector3 nowMousePositon = Input.mousePosition;
                    Vector3 startMousePosition = MousePos;

                    Vector3 direction = nowMousePositon - startMousePosition;

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    _dashArrowObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
            
            if (Input.GetMouseButtonUp(1))
            {
                ResumeRigidbody();
                Vector2 NowMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Destroy(_dashArrowObject);

                /*  쓰지 않는데 나중에 쓸 예정임. 
                 
               
                (각도를 구하는것) 
                float EulerAngle = Quaternion.FromToRotation(Vector3.up,
                new Vector2(MousePos.x / Screen.width, MousePos.y / Screen.height) -
                new Vector2(NowMousePos.x / Screen.width, NowMousePos.y / Screen.height)).eulerAngles.z;

                (거리 구하는것) 
                float Pow = (Vector2.Distance(MousePos / Screen.height, NowMousePos / Screen.height));

                 */




                Vector2 mov = (MousePos / Screen.height - NowMousePos / Screen.height)*6;
                mov.x = Mathf.Clamp(mov.x, -1, 1);
                mov.y = Mathf.Clamp(mov.y, -1, 1);

                velocityX = mov.x * DashPow.x;
                rb.velocity = new Vector3(rb.velocity.x, mov .y* DashPow.y, rb.velocity.z);
            }




            //DashAble = false;
        }
    }
    
    void FreezeRigidbody()
    {
        rb.isKinematic = true;
    }

    void ResumeRigidbody()
    {
        rb.isKinematic = false;
    }
}

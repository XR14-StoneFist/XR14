using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrabWallState { None, Left,Right}

public class JTH_PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    float velocityX;
    public float speed;
    public float accelerationspeed;
    public float JumpPow;

    [Header("점프부분")]
    public SubTriggerColider JumpBox;
    int JumpCount =2;

    [Header("벽 점프 부분")]
    public SubTriggerColider ClimeWall_Left;
    public SubTriggerColider ClimeWall_Right;
    public Vector2 WallDashPow;
    GrabWallState grabWallState = GrabWallState.None;

    [Header("대쉬 부분")]
    public SubTriggerColider DashPoint;
    public Vector2 DashPow;
    Vector2 MousePos;
    bool DashAble =true;

    [Header("마찰부분")]
    public PhysicMaterial PlayerPhysicMaterial;


    public float pow;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        JumpBox.A_OnTriggerEnter += (Tag) => {
            if (Tag == "ground") JumpCount=2;
        };

        ClimeWall_Left.A_OnTriggerEnter += (Tag) => {
            if (Tag == "wall") {
                grabWallState = GrabWallState.Left;
                PlayerPhysicMaterial.dynamicFriction = 300;
                JumpCount = 1;
            }
        };
        ClimeWall_Left.A_OnTriggerExit+= (Tag) => {
            if (Tag == "wall")
            {
                grabWallState = GrabWallState.None;
                PlayerPhysicMaterial.dynamicFriction = 0;
            }
        };

        ClimeWall_Right.A_OnTriggerEnter += (Tag) => {
            if (Tag == "wall")
            {
                grabWallState = GrabWallState.Right;
                PlayerPhysicMaterial.dynamicFriction = 300;
                JumpCount = 1;
            }
        };
        ClimeWall_Right.A_OnTriggerExit += (Tag) => {
            if (Tag == "wall")
            {
                grabWallState = GrabWallState.None;
                PlayerPhysicMaterial.dynamicFriction = 0;
            }
        };

        DashPoint.A_OnTriggerEnter += (Tag) => {
            if (Tag == "DashPoint")
                DashAble = true;
        };
        DashPoint.A_OnTriggerExit += (Tag) => {
            if (Tag == "DashPoint")
                DashAble = false;
        };
    }
    void Update()
    {
        Move();
        Desh();
    }

    void Move()
    {
        if (!(Input.GetKey(KeyCode.D) ^ Input.GetKey(KeyCode.A)))
            velocityX = Mathf.Lerp(velocityX, 0, accelerationspeed * Time.deltaTime);
        else
        {
            if (Input.GetKey(KeyCode.A))
                velocityX = Mathf.Lerp(velocityX, -speed, accelerationspeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.D))
                velocityX = Mathf.Lerp(velocityX, speed, accelerationspeed * Time.deltaTime);

        }

        if (Input.GetKeyDown(KeyCode.Space) && JumpCount-- > 0)
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
    void Desh()
    {
        if(DashAble)
        {
            if (Input.GetMouseButtonDown(1))
            {
                MousePos =  new Vector2(Input.mousePosition.x , Input.mousePosition.y );

            }
            if (Input.GetMouseButtonUp(1))
            {
                Vector2 NowMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

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
}

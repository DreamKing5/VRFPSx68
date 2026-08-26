using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //移動・ジャンプの基礎動作
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpPower = 600f;
    [SerializeField] private float mouseSensitivity = 250f;
    private float verticalVelocity;
    private float cameraPitch;
    //ここまで移動・ジャンプの基礎動作


    

    //プレイヤーステータスの設定
    private PlayerStatus status=new PlayerStatus();
    public PlayerStatus Status =>status;


    void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible=false;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }

    private void Move()//WASD移動
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed=16f;
        }
        else if(Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed=32f;
            status.Turbo();
        }else
        {
            moveSpeed=8f;
        }

        forward.y=0f;
        right.y=0f;

        Vector3 moveDirection =
            forward*z + right*x;

        if (controller.isGrounded)
        {
            verticalVelocity =-2f;
            status.jumpCount = 0;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpPower;
            }
        }else if (Input.GetKeyDown(KeyCode.J))
        {   
            status.jumpCount++;
            if(Input.GetKey(KeyCode.W) ||Input.GetKey(KeyCode.A) ||Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {   
                status.jumpPower = Vector3.Lerp(status.jumpPower, status.jumpPower+forward*Input.GetAxis("Vertical")*1000f + right*Input.GetAxis("Horizontal")*1000f, 10f * Time.deltaTime);
            }
            verticalVelocity = jumpPower*3;
            status.Jump(status.jumpCount);
            
        }

        verticalVelocity += Physics.gravity.y*5.0f*Time.deltaTime;

        Vector3 velocity = moveDirection *moveSpeed;

        velocity.y = verticalVelocity;
        //移動と多段ジャンプの処理
        controller.Move(velocity *Time.deltaTime+status.jumpPower*Time.deltaTime);

        //jumpPowerを自然に収束させる
        status.jumpPower = Vector3.Lerp(status.jumpPower, Vector3.zero, 5f * Time.deltaTime);
        
    }

    public void Look()//カーソルによる視点変更
    {
        float mouseX=Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        float mouseY=Input.GetAxis("Mouse Y")*mouseSensitivity*Time.deltaTime;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch,-80f,80f);
        cameraTransform.localRotation=Quaternion.Euler(cameraPitch,0f,0f);
        transform.Rotate(Vector3.up*mouseX);
    }
}

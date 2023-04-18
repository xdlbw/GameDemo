using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour�ṩ�˿�ܣ������ű����ӵ��༭���е���Ϸ�������нű���Ĭ�������Ը���
public class PlayerController : MonoBehaviour
{
    public float RunSpeed;            //������ɫ�����ٶ�
    public float JumpSpeed;           //������Ծ�ٶ�
    public float ClimbSpeed;

    private bool isGround;            //�жϽ�ɫ�Ƿ��ڵ���
    private bool CanDoubleJump;       //�Ƿ���Զ�����Ծ
    private Rigidbody2D MyRigidbody;  //�����������
    private Animator MyAnim;          //�����������
    private BoxCollider2D MyFeet;     //�����Ų���ײ���

    private bool isClimbing;
    private bool isGroundLadder;
    private bool isLadder;

    private bool isJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;

    private float PlayerGravity;

    //����Ϸ����ʼ����ʱ�����س�����ʵ������Ϸ����ʱ������
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();    //MyRigidbodyΪ��ǰ��Ϸ����ĸ������
        MyAnim = GetComponent<Animator>();            //MyAnimΪ��ǰ��Ϸ����Ķ������
        MyFeet = GetComponent<BoxCollider2D>();       //MyFeetΪ��ǰ��Ϸ����ĺ�����ײ���
        PlayerGravity = MyRigidbody.gravityScale;
    }

    //ÿһ֡���ᱻ����
    void Update()
    {
        Run();
        Flip();
        Jump();
        SwitchState();
        CheckLadder();
        CheckAirStatus();
        Climb();
    }

    //����
    void Run()
    {
        float MoveDir = Input.GetAxis("Horizontal");  //��Ӧ�����ϵ�A����D�� ������͡���������ֵΪ-1��1
        //��Ϸ����x�����ٶ�Ϊ����*�ٶȣ�y�����ٶȲ���
        MyRigidbody.velocity = new Vector2(MoveDir * RunSpeed, MyRigidbody.velocity.y);
        //�ж���Ϸ������x�����Ƿ����ٶ�
        bool PlayerHasXAxisSpeed = Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;
        //�ж���Ϸ������y�����Ƿ����ٶ�
        bool PlayerHasYAxisSpeed = Mathf.Abs(MyRigidbody.velocity.y) > Mathf.Epsilon;
        //Run��������ǲ���վ���뱼�ܶ������жϱ��������丳ֵ
        MyAnim.SetBool("Run", PlayerHasXAxisSpeed);
        MyAnim.SetBool("Fall", PlayerHasYAxisSpeed);
    }

    //���﷭ת
    void Flip()
    {
        //���ж���Ϸ������x�����Ƿ����ٶ�
        bool PlayerHasXAxisSpeed = Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasXAxisSpeed)
        {
            //Quaternion.Euler��z,x,y˳��
            //��������ң��򲻷�ת
            if (MyRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            //�������ˮƽ����ת180��
            if (MyRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    //��Ծ
    void Jump()
    {
        //�жϽŲ���ײ���Ƿ�Ӵ���Ground��㣬����boolֵ
        isGround = MyFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
                || MyFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
                || MyFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));

        if (Input.GetButtonDown("Jump"))                        //������¿ո��
        {
            
            if (isGround)                                       //�ڵ�����
            {
                SoundManager.PlayJumpClip();
                MyAnim.SetBool("Jump", true);                   //һ������jumpΪtrue��Ϊjump״̬������jump����
                MyRigidbody.velocity = Vector2.up * JumpSpeed;  //����һ�����ϵ��ٶ�
                CanDoubleJump = true;                           //���Զ�����
            }
            else                                                //�ڿ���
            {
                if (CanDoubleJump)                              //���Զ�����Ծ
                {
                    SoundManager.PlayJumpClip();
                    MyAnim.SetBool("DoubleJump", true);         //��������doublejumpΪtrue��Ϊdoublejump״̬������doublejump����
                    MyRigidbody.velocity = Vector2.up * JumpSpeed;
                    CanDoubleJump = false;                      //�����Զ�����
                }
            }
        }
    }

    //״̬�ı�
    void SwitchState()
    {
        if (MyAnim.GetBool("Jump"))                          //�������Ծ̬
        {
            //Debug.Log(MyRigidbody.velocity.y);
            if (MyRigidbody.velocity.y < 0.0f)               //�����ֱ�����ϣ�����Ϊ���ϣ����ٶ�С��0����Ҫ������
            {
                MyAnim.SetBool("Jump", false);               
                MyAnim.SetBool("Fall", true);                //Ϊfall״̬������fall����
            }
        }
        else if (isGround)                                   //����ڵ���
        {
            MyAnim.SetBool("Fall", false);                   
            MyAnim.SetBool("Idle", true);                    //Ϊidle״̬������idle����
        }

        if (MyAnim.GetBool("DoubleJump"))                    //����ڶ�����̬
        {
            if (MyRigidbody.velocity.y < 0.0f)               //�����ֱ�����ϣ�����Ϊ���ϣ����ٶ�С��0����Ҫ������
            {
                MyAnim.SetBool("DoubleJump", false);
                MyAnim.SetBool("DoubleFall", true);          //Ϊdoublefall״̬������doublefall����
            }
        }
        else if (isGround)                                   //����ڵ���
        {
            MyAnim.SetBool("DoubleFall", false);             
            MyAnim.SetBool("Idle", true);                    //Ϊidle״̬������idle����
        }

        
    }

    //����
    /*void Attack()
    {
        if (Input.GetButtonDown("Attack"))           
        {
            MyAnim.SetTrigger("Attack");             //һ����⵽Attack�����£��ʹ���attack����
        }
    }*/

    void CheckLadder()
    {
        isLadder = MyFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")) &&
                  !MyFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

        isGroundLadder = MyFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")) &&
                         MyFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Climb()
    {
        if (isGroundLadder)
        {
            float moveY = Input.GetAxis("Vertical");
            if (moveY > 0.1f || moveY < -0.1f)
            {
                MyAnim.SetBool("Climbing", true);
                MyRigidbody.gravityScale = 0.0f;
                MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x, moveY * ClimbSpeed);
            }
            else
            {
                if (isJumping || isFalling || isDoubleJumping || isDoubleFalling)
                {
                    MyAnim.SetBool("Climbing", false);
                }
                else
                {
                    MyAnim.SetBool("Climbing", false);
                    MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x, 0.0f);
                }
            }
        }
        else if (isLadder)
        {
            isFalling = false;
            //MyAnim.SetBool("Climbing", true);
            float moveY = Input.GetAxis("Vertical");

            if (moveY > 0.1f || moveY < -0.1f)
            {
                MyAnim.SetBool("Climbing", true);
                MyRigidbody.gravityScale = 0.0f;
                MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x, moveY * ClimbSpeed);
            }
            else
            {
                if(isJumping || isFalling || isDoubleJumping || isDoubleFalling)
                {
                    MyAnim.SetBool("Climbing", false);
                }
                else
                {
                    Debug.Log("enter");
                    MyAnim.SetBool("Climbing", true);
                    MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            MyAnim.SetBool("Climbing", false);
            MyRigidbody.gravityScale = PlayerGravity;
        }
    }
    

    void CheckAirStatus()
    {
        isJumping = MyAnim.GetBool("Jump");
        isFalling = MyAnim.GetBool("Fall");
        isDoubleJumping = MyAnim.GetBool("DoubleJump");
        isDoubleFalling = MyAnim.GetBool("DoubleFall");
        isClimbing = MyAnim.GetBool("Climbing");

        //Debug.Log("isFalling:" + isFalling);
    }
}

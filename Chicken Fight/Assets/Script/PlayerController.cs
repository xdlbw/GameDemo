using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour提供了框架，允许将脚本附加到编辑器中的游戏对象，所有脚本都默认派生自该类
public class PlayerController : MonoBehaviour
{
    public float RunSpeed;            //声明角色奔跑速度
    public float JumpSpeed;           //声明跳跃速度
    public float ClimbSpeed;

    private bool isGround;            //判断角色是否在地面
    private bool CanDoubleJump;       //是否可以二段跳跃
    private Rigidbody2D MyRigidbody;  //声明刚体组件
    private Animator MyAnim;          //声明动画组件
    private BoxCollider2D MyFeet;     //声明脚部碰撞组件

    private bool isClimbing;
    private bool isGroundLadder;
    private bool isLadder;

    private bool isJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;

    private float PlayerGravity;

    //在游戏对象开始存在时（加载场景或实例化游戏对象时）调用
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();    //MyRigidbody为当前游戏对象的刚体组件
        MyAnim = GetComponent<Animator>();            //MyAnim为当前游戏对象的动画组件
        MyFeet = GetComponent<BoxCollider2D>();       //MyFeet为当前游戏对象的盒体碰撞组件
        PlayerGravity = MyRigidbody.gravityScale;
    }

    //每一帧都会被调用
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

    //奔跑
    void Run()
    {
        float MoveDir = Input.GetAxis("Horizontal");  //对应键盘上的A键和D键 或←键和→键，返回值为-1到1
        //游戏对象x方向速度为方向*速度，y方向速度不变
        MyRigidbody.velocity = new Vector2(MoveDir * RunSpeed, MyRigidbody.velocity.y);
        //判断游戏对象在x方向是否有速度
        bool PlayerHasXAxisSpeed = Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;
        //判断游戏对象在y方向是否有速度
        bool PlayerHasYAxisSpeed = Mathf.Abs(MyRigidbody.velocity.y) > Mathf.Epsilon;
        //Run这个参数是播放站立与奔跑动画的判断变量，将其赋值
        MyAnim.SetBool("Run", PlayerHasXAxisSpeed);
        MyAnim.SetBool("Fall", PlayerHasYAxisSpeed);
    }

    //人物翻转
    void Flip()
    {
        //先判断游戏对象在x方向是否有速度
        bool PlayerHasXAxisSpeed = Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasXAxisSpeed)
        {
            //Quaternion.Euler（z,x,y顺序）
            //如果是向右，则不反转
            if (MyRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            //如果向左，水平方向反转180度
            if (MyRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    //跳跃
    void Jump()
    {
        //判断脚部碰撞体是否接触到Ground这层，返回bool值
        isGround = MyFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
                || MyFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
                || MyFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));

        if (Input.GetButtonDown("Jump"))                        //如果按下空格键
        {
            
            if (isGround)                                       //在地面上
            {
                SoundManager.PlayJumpClip();
                MyAnim.SetBool("Jump", true);                   //一段跳，jump为true，为jump状态，播放jump动画
                MyRigidbody.velocity = Vector2.up * JumpSpeed;  //给坤一个向上的速度
                CanDoubleJump = true;                           //可以二段跳
            }
            else                                                //在空中
            {
                if (CanDoubleJump)                              //可以二段跳跃
                {
                    SoundManager.PlayJumpClip();
                    MyAnim.SetBool("DoubleJump", true);         //二段跳，doublejump为true，为doublejump状态，播放doublejump动画
                    MyRigidbody.velocity = Vector2.up * JumpSpeed;
                    CanDoubleJump = false;                      //不可以二段跳
                }
            }
        }
    }

    //状态改变
    void SwitchState()
    {
        if (MyAnim.GetBool("Jump"))                          //如果在跳跃态
        {
            //Debug.Log(MyRigidbody.velocity.y);
            if (MyRigidbody.velocity.y < 0.0f)               //如果垂直方向上（正向为向上）的速度小于0，就要掉落了
            {
                MyAnim.SetBool("Jump", false);               
                MyAnim.SetBool("Fall", true);                //为fall状态，播放fall动画
            }
        }
        else if (isGround)                                   //如果在地面
        {
            MyAnim.SetBool("Fall", false);                   
            MyAnim.SetBool("Idle", true);                    //为idle状态，播放idle动画
        }

        if (MyAnim.GetBool("DoubleJump"))                    //如果在二段跳态
        {
            if (MyRigidbody.velocity.y < 0.0f)               //如果垂直方向上（正向为向上）的速度小于0，就要掉落了
            {
                MyAnim.SetBool("DoubleJump", false);
                MyAnim.SetBool("DoubleFall", true);          //为doublefall状态，播放doublefall动画
            }
        }
        else if (isGround)                                   //如果在地面
        {
            MyAnim.SetBool("DoubleFall", false);             
            MyAnim.SetBool("Idle", true);                    //为idle状态，播放idle动画
        }

        
    }

    //攻击
    /*void Attack()
    {
        if (Input.GetButtonDown("Attack"))           
        {
            MyAnim.SetTrigger("Attack");             //一旦检测到Attack键按下，就触发attack动画
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

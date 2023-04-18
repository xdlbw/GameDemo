using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;                //玩家血量
    public int blinks;                  //玩家受到攻击闪烁次数
    public float time;                  //玩家收到攻击多长时间闪烁一次
    public float InvincibleTime;        //受伤之后的无敌时间

    private Renderer MyRenderer;        //Renderer 模块的设置决定了粒子的图像或网格如何被其他粒子变换、着色和过度绘制。
    private CapsuleCollider2D cap;      //胶囊体组件
    private Animator MyAnim;
    private ScreenFlash SF;             //屏闪类

    void Start()
    {
        HealthBar.HealthMax = health;                   //把玩家初始状态的血量（满血）赋予给血量条的变量
        HealthBar.HealthCurrent = health;
        MyRenderer = GetComponent<Renderer>();
        cap = GetComponent<CapsuleCollider2D>();
        MyAnim = GetComponent<Animator>();
        SF = GetComponent<ScreenFlash>();
    }


    void Update()
    {
        
    }

    //玩家受伤
    public void DamagePlayer(float damage)
    {
        int number = Random.Range(0, 2);
        if(number == 0)
        {
            SoundManager.PlayJiClip();
        }
        else if(number == 1)
        {
            SoundManager.PlayAiyoClip();
        }

        health -= damage;
        HealthBar.HealthCurrent = health;                   //血量条当前血量更新
        SF.FlashScreen();                                   //只要受伤就调用屏闪
        if (health <= 0)
        {
            health = 0;                                     //当血量小于0时重置为0，防止出现血量负数的bug
            MyAnim.SetTrigger("Death");
            GetComponent<PlayerController>().enabled = false;       //玩家不可移动
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;    //速度置零
            Destroy(gameObject, 10.0f);                     //血量低于0就销毁玩家
        }
        else
        {
            BlinkPlayer(blinks, time);                      //受到攻击血量减少并闪烁
            cap.enabled = false;                            //关闭胶囊体，其实就是关闭了碰到怪物造成伤害的判定，达到无敌的效果
            Invoke("invincibility", InvincibleTime);        //无敌时间过后就再次开启胶囊体
        }
        GameController.MCShake.Shake();                     //镜头抖动
    }

    //受伤之后玩家闪烁
    void BlinkPlayer(int Blinknum, float seconds)
    {
        StartCoroutine(DoBlinks(Blinknum, seconds));    //开启doblinks协程
    }

    //协程
    IEnumerator DoBlinks(int Blinknum, float seconds)
    {
        for(int i = 0; i < Blinknum; i++)
        {
            MyRenderer.enabled = !MyRenderer.enabled;   //关闭玩家renderer，达到消失的效果
            yield return new WaitForSeconds(seconds);   //此协程在每一帧等待seconds后继续执行
        }
        MyRenderer.enabled = true;                      //打开玩家renderer
    }

    //无敌
    void invincibility()
    {
        cap.enabled = true;
    }
}

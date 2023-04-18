using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemy抽象类
public abstract class Enemy : MonoBehaviour
{
    public float health;                            //敌人血量
    public float damage;                            //敌人伤害
    public float ChangeTime;                        //敌人受到伤害颜色改变时间
    public GameObject BloodEffect;                  //受伤特效
    public GameObject DropCoin;                     //爆金币啦
    public GameObject FloatPoint;

    private SpriteRenderer sr;
    private Color OriginalColor;                    //敌人原本的颜色
    private PlayerHealth playerHealth;              //玩家血量

    protected void Start()
    {
        sr = GetComponent<SpriteRenderer>();        //获取SpriteRenderer组件
        OriginalColor = sr.color;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();   //获取playerhealth这个脚本
    }

    //血量低于0就销毁物体
    protected void Update()
    {
        if (health <= 0)
        {
            Instantiate(DropCoin, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    //受伤
    public void GetHurt(float Hurt)
    {
        int number = Random.Range(0, 2);
        if (number == 0)
        {
            SoundManager.PlayNiGanMaClip();
        }
        else if (number == 1)
        {
            SoundManager.PlayLiHaiClip();
        }
        SoundManager.PlayLiHaiClip();
        GameObject gb = Instantiate(FloatPoint, transform.position, Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = Hurt.ToString();
        health -= Hurt;                             //血量-
        ChangeColor(ChangeTime);                    //改变颜色
        Instantiate(BloodEffect, transform.position, Quaternion.identity);  //在gameobject实例化流血这个游戏对象，不旋转
        GameController.MCShake.Shake();             //镜头抖动
    }

    //改变颜色
    void ChangeColor(float time)                    
    {
        sr.color = Color.red;                       //颜色变红
        Invoke("ResetColor", time);                 //经过传入的时间ChangeTime后，调用ResetColor方法
    }

    //恢复颜色
    void ResetColor()
    {
        sr.color = OriginalColor;                   //颜色变回原来的颜色
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //有带有player标签的碰撞盒进入触发器,且碰撞盒的类型为胶囊体类型（玩家的身体）
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth)
            {
                //调用DamagePlayer方法
                playerHealth.DamagePlayer(damage);
            }
        }
    }
}

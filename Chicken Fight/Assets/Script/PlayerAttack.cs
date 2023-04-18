using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackDamage;                                //攻击伤害
    //因为要实现攻击碰撞盒出现消失的功能，故采用协程计时器的方式
    public float StartTime;                                   //协程开始时间 
    public float EndTime;                                     //协程结束时间

    private Animator Anim;
    private PolygonCollider2D MyWeapon;

    void Start()
    {
        Anim = GetComponentInParent<Animator>();
        MyWeapon = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        Attack();
    }

    //攻击
    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            SoundManager.PlayBasketBallClip();
            Anim.SetTrigger("Attack");                       //一旦检测到Attack键按下，就触发attack动画
            StartCoroutine(StartAttack());                   //开启startattack协程
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(StartTime);          //此协程在每一帧等待StartTime后继续执行
        MyWeapon.enabled = true;                             //myweapon的碰撞盒开启
        StartCoroutine(CancelAttack());                      //开启协程cancelattack
    }

    IEnumerator CancelAttack()
    {
        yield return new WaitForSeconds(EndTime);            //此协程在每一帧等待EndTime后继续执行
        MyWeapon.enabled = false;                            //myweapon的碰撞盒关闭
    }

    //检测到碰撞，触发
    void OnTriggerEnter2D(Collider2D collision)
    {
        //有带有enemy标签的碰撞盒进入触发器
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //调用gethurt方法
            collision.GetComponent<Enemy>().GetHurt(AttackDamage);
        }
    }
}

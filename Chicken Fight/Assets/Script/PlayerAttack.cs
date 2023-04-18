using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackDamage;                                //�����˺�
    //��ΪҪʵ�ֹ�����ײ�г�����ʧ�Ĺ��ܣ��ʲ���Э�̼�ʱ���ķ�ʽ
    public float StartTime;                                   //Э�̿�ʼʱ�� 
    public float EndTime;                                     //Э�̽���ʱ��

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

    //����
    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            SoundManager.PlayBasketBallClip();
            Anim.SetTrigger("Attack");                       //һ����⵽Attack�����£��ʹ���attack����
            StartCoroutine(StartAttack());                   //����startattackЭ��
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(StartTime);          //��Э����ÿһ֡�ȴ�StartTime�����ִ��
        MyWeapon.enabled = true;                             //myweapon����ײ�п���
        StartCoroutine(CancelAttack());                      //����Э��cancelattack
    }

    IEnumerator CancelAttack()
    {
        yield return new WaitForSeconds(EndTime);            //��Э����ÿһ֡�ȴ�EndTime�����ִ��
        MyWeapon.enabled = false;                            //myweapon����ײ�йر�
    }

    //��⵽��ײ������
    void OnTriggerEnter2D(Collider2D collision)
    {
        //�д���enemy��ǩ����ײ�н��봥����
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //����gethurt����
            collision.GetComponent<Enemy>().GetHurt(AttackDamage);
        }
    }
}

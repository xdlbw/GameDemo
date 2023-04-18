using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;                //���Ѫ��
    public int blinks;                  //����ܵ�������˸����
    public float time;                  //����յ������೤ʱ����˸һ��
    public float InvincibleTime;        //����֮����޵�ʱ��

    private Renderer MyRenderer;        //Renderer ģ������þ��������ӵ�ͼ���������α��������ӱ任����ɫ�͹��Ȼ��ơ�
    private CapsuleCollider2D cap;      //���������
    private Animator MyAnim;
    private ScreenFlash SF;             //������

    void Start()
    {
        HealthBar.HealthMax = health;                   //����ҳ�ʼ״̬��Ѫ������Ѫ�������Ѫ�����ı���
        HealthBar.HealthCurrent = health;
        MyRenderer = GetComponent<Renderer>();
        cap = GetComponent<CapsuleCollider2D>();
        MyAnim = GetComponent<Animator>();
        SF = GetComponent<ScreenFlash>();
    }


    void Update()
    {
        
    }

    //�������
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
        HealthBar.HealthCurrent = health;                   //Ѫ������ǰѪ������
        SF.FlashScreen();                                   //ֻҪ���˾͵�������
        if (health <= 0)
        {
            health = 0;                                     //��Ѫ��С��0ʱ����Ϊ0����ֹ����Ѫ��������bug
            MyAnim.SetTrigger("Death");
            GetComponent<PlayerController>().enabled = false;       //��Ҳ����ƶ�
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;    //�ٶ�����
            Destroy(gameObject, 10.0f);                     //Ѫ������0���������
        }
        else
        {
            BlinkPlayer(blinks, time);                      //�ܵ�����Ѫ�����ٲ���˸
            cap.enabled = false;                            //�رս����壬��ʵ���ǹر���������������˺����ж����ﵽ�޵е�Ч��
            Invoke("invincibility", InvincibleTime);        //�޵�ʱ�������ٴο���������
        }
        GameController.MCShake.Shake();                     //��ͷ����
    }

    //����֮�������˸
    void BlinkPlayer(int Blinknum, float seconds)
    {
        StartCoroutine(DoBlinks(Blinknum, seconds));    //����doblinksЭ��
    }

    //Э��
    IEnumerator DoBlinks(int Blinknum, float seconds)
    {
        for(int i = 0; i < Blinknum; i++)
        {
            MyRenderer.enabled = !MyRenderer.enabled;   //�ر����renderer���ﵽ��ʧ��Ч��
            yield return new WaitForSeconds(seconds);   //��Э����ÿһ֡�ȴ�seconds�����ִ��
        }
        MyRenderer.enabled = true;                      //�����renderer
    }

    //�޵�
    void invincibility()
    {
        cap.enabled = true;
    }
}

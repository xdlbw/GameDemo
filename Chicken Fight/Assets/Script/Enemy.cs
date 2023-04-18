using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemy������
public abstract class Enemy : MonoBehaviour
{
    public float health;                            //����Ѫ��
    public float damage;                            //�����˺�
    public float ChangeTime;                        //�����ܵ��˺���ɫ�ı�ʱ��
    public GameObject BloodEffect;                  //������Ч
    public GameObject DropCoin;                     //�������
    public GameObject FloatPoint;

    private SpriteRenderer sr;
    private Color OriginalColor;                    //����ԭ������ɫ
    private PlayerHealth playerHealth;              //���Ѫ��

    protected void Start()
    {
        sr = GetComponent<SpriteRenderer>();        //��ȡSpriteRenderer���
        OriginalColor = sr.color;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();   //��ȡplayerhealth����ű�
    }

    //Ѫ������0����������
    protected void Update()
    {
        if (health <= 0)
        {
            Instantiate(DropCoin, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    //����
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
        health -= Hurt;                             //Ѫ��-
        ChangeColor(ChangeTime);                    //�ı���ɫ
        Instantiate(BloodEffect, transform.position, Quaternion.identity);  //��gameobjectʵ������Ѫ�����Ϸ���󣬲���ת
        GameController.MCShake.Shake();             //��ͷ����
    }

    //�ı���ɫ
    void ChangeColor(float time)                    
    {
        sr.color = Color.red;                       //��ɫ���
        Invoke("ResetColor", time);                 //���������ʱ��ChangeTime�󣬵���ResetColor����
    }

    //�ָ���ɫ
    void ResetColor()
    {
        sr.color = OriginalColor;                   //��ɫ���ԭ������ɫ
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //�д���player��ǩ����ײ�н��봥����,����ײ�е�����Ϊ���������ͣ���ҵ����壩
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth)
            {
                //����DamagePlayer����
                playerHealth.DamagePlayer(damage);
            }
        }
    }
}

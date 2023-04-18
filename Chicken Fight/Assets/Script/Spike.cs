using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage;                          //�˺�
    private PlayerHealth playerHealth;          //playerhealth��

    void Start()
    {
        //��gameobject�ҵ���ǩΪplayer��playerhealth������ű���
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���player�Ľ�������ײ�н���spike��ײ�оͻᴥ��
        if(collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            //����������˺���
            playerHealth.DamagePlayer(damage);
        }
    }
}

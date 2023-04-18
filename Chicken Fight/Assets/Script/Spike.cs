using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage;                          //伤害
    private PlayerHealth playerHealth;          //playerhealth类

    void Start()
    {
        //在gameobject找到标签为player的playerhealth组件（脚本）
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如果player的胶囊体碰撞盒进入spike碰撞盒就会触发
        if(collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            //调用玩家受伤函数
            playerHealth.DamagePlayer(damage);
        }
    }
}

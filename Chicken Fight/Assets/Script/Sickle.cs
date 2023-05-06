using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    public float speed;                                 //回旋镖速度
    public float damage;                                //回旋镖伤害
    public float rotateSpeed;                           //回旋镖旋转速度
    public float destroyTime;                           //销毁回旋镖时间

    private Vector2 initSpeed;                          //回旋镖初始速度
    private Rigidbody2D rb;                             //与速度有关，获取刚体组件
    private Transform playerTransform;                  //考虑到回旋镖的回收（销毁），需要回旋镖的位置和玩家的位置
    private Transform sickleTransform;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = gameObject.transform.right * speed;               //给一个初始速度
        initSpeed = rb.velocity;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sickleTransform = GetComponent<Transform>();
    }

    
    void Update()
    {
        //给一个回旋镖旋转速度
        gameObject.transform.Rotate(0, 0, rotateSpeed);
        //回旋镖沿运动方向的速度越来越小，小到0直到反向速度越来越大
        rb.velocity = rb.velocity - initSpeed * Time.deltaTime;
        //如果在这途中，回旋镖位置和玩家位置很近，就直接销毁回旋镖（回收的效果）
        if((Mathf.Abs(sickleTransform.position.x - playerTransform.position.x) < 0.5f) &&
           (Mathf.Abs(sickleTransform.position.y - playerTransform.position.y) < 0.5f))
        {
            Destroy(gameObject);
        }
        //如果在这途中，玩家因为跳跃或者一系列原因接不到回旋镖，回旋镖就沿着原路返回，直到destroytime后被销毁
        else
        {
            Invoke("destroy", destroyTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //回旋镖碰到enemy就造成伤害
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetHurt(damage);
        }
    }

    //销毁回旋镖
    void destroy()
    {
        Destroy(gameObject);
    }
}

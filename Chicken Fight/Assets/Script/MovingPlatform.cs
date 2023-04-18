using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float Speed;                     //移动平台速度
    public float WaitTime;                  //平台等待时间
    public Transform[] MovePos;             //平台移动点数组

    //这里跟蝙蝠的ai其实差不多，但是蝙蝠是在范围内移动的，所以定两个点确定矩形范围
    //但是移动平台其实就是两个点之间来回移动，所以设个数组就可以，当然也可以用点，只不过两点在一条线上效果就是平移了

    private int i;                          //移动点的数组下标
    private Transform PlayerTransform;

    void Start()
    {
        i = 1;                              //首先移动到点1的位置

        //游戏开始前，记录player的父级变换（应该为null，因为player不是任何对象的子物体）
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    
    void Update()
    {
        //类似蝙蝠的移动
        transform.position = Vector2.MoveTowards(transform.position, MovePos[i].position, Speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, MovePos[i].position) < 0.1f)
        {
            if(WaitTime < 0.0f)
            {
                i ^= 1;                     //小处理，i与1异或，如果i为1，那么i变成0，就是下一个要移动到点0的位置
                WaitTime = 1.0f;            //重置等待时间
            }
            else
            {
                WaitTime -= Time.deltaTime; //等待时间随帧数单位时间减少
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //当player的boxcollider与movingplatform触发时
        if(collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            //让movingplatform的变换成为player变换的父层变换（即让player称为movingplatform的子对象）
            //如何获取player，即当前碰撞的游戏物体变换
            collision.gameObject.transform.parent = gameObject.transform;
        }
        //金币
        if (collision.CompareTag("Item") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            //让movingplatform的变换成为player变换的父层变换（即让player称为movingplatform的子对象）
            //如何获取player，即当前碰撞的游戏物体变换
            collision.gameObject.transform.parent.parent = gameObject.transform;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            //让player的父层变换为空
            collision.gameObject.transform.parent = PlayerTransform;
        }
    }
}

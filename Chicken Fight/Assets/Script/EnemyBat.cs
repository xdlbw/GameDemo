using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float speed;                     //飞行速度
    public float StartWaitTime;             //在每一位置的暂停时间
     
    public Transform MovePos;               //应该随机移动的点
    public Transform LeftDownPos;           //左下
    public Transform RightUpPos;            //右上

    private float WaitTime;                 //目前在某一位置的剩余暂停时间

    new protected void Start()
    {
        base.Start();                       //调用父类的Start方法
        WaitTime = StartWaitTime;           //剩余暂停时间为StartWaitTime
        MovePos.position = GetRandomPos();  //随机生成点坐标
    }

    new protected void Update()
    {
        base.Update();

        //从初始位置朝终点位置移动，每一帧移动的距离为speed * Time.deltaTime，返回终点位置
        transform.position = Vector2.MoveTowards(transform.position, MovePos.position, speed * Time.deltaTime);

        //如果靠近MovePos.position了
        if (Vector2.Distance(transform.position, MovePos.position) < 0.1f)
        {
            if(WaitTime <= 0)                               //不能停留
            {
                MovePos.position = GetRandomPos();          //计算下一个随机点
                WaitTime = StartWaitTime;                   //重置停留时间
            }
            else
            {
                WaitTime -= Time.deltaTime;                 
            }
        }
    }

    
    //随机生成点坐标
    Vector2 GetRandomPos()
    {
        //生成点的横坐标范围为左下点的横坐标到右上点的横坐标
        //生成点的纵坐标范围为左下点的纵坐标到右上点的纵坐标
        Vector2 RandomPos = new Vector2(Random.Range(LeftDownPos.position.x, RightUpPos.position.x), Random.Range(LeftDownPos.position.y, RightUpPos.position.y));
        return RandomPos;
    }
}

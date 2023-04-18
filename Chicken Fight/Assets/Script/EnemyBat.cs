using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float speed;                     //�����ٶ�
    public float StartWaitTime;             //��ÿһλ�õ���ͣʱ��
     
    public Transform MovePos;               //Ӧ������ƶ��ĵ�
    public Transform LeftDownPos;           //����
    public Transform RightUpPos;            //����

    private float WaitTime;                 //Ŀǰ��ĳһλ�õ�ʣ����ͣʱ��

    new protected void Start()
    {
        base.Start();                       //���ø����Start����
        WaitTime = StartWaitTime;           //ʣ����ͣʱ��ΪStartWaitTime
        MovePos.position = GetRandomPos();  //������ɵ�����
    }

    new protected void Update()
    {
        base.Update();

        //�ӳ�ʼλ�ó��յ�λ���ƶ���ÿһ֡�ƶ��ľ���Ϊspeed * Time.deltaTime�������յ�λ��
        transform.position = Vector2.MoveTowards(transform.position, MovePos.position, speed * Time.deltaTime);

        //�������MovePos.position��
        if (Vector2.Distance(transform.position, MovePos.position) < 0.1f)
        {
            if(WaitTime <= 0)                               //����ͣ��
            {
                MovePos.position = GetRandomPos();          //������һ�������
                WaitTime = StartWaitTime;                   //����ͣ��ʱ��
            }
            else
            {
                WaitTime -= Time.deltaTime;                 
            }
        }
    }

    
    //������ɵ�����
    Vector2 GetRandomPos()
    {
        //���ɵ�ĺ����귶ΧΪ���µ�ĺ����굽���ϵ�ĺ�����
        //���ɵ�������귶ΧΪ���µ�������굽���ϵ��������
        Vector2 RandomPos = new Vector2(Random.Range(LeftDownPos.position.x, RightUpPos.position.x), Random.Range(LeftDownPos.position.y, RightUpPos.position.y));
        return RandomPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float Speed;                     //�ƶ�ƽ̨�ٶ�
    public float WaitTime;                  //ƽ̨�ȴ�ʱ��
    public Transform[] MovePos;             //ƽ̨�ƶ�������

    //����������ai��ʵ��࣬�����������ڷ�Χ���ƶ��ģ����Զ�������ȷ�����η�Χ
    //�����ƶ�ƽ̨��ʵ����������֮�������ƶ��������������Ϳ��ԣ���ȻҲ�����õ㣬ֻ����������һ������Ч������ƽ����

    private int i;                          //�ƶ���������±�
    private Transform PlayerTransform;

    void Start()
    {
        i = 1;                              //�����ƶ�����1��λ��

        //��Ϸ��ʼǰ����¼player�ĸ����任��Ӧ��Ϊnull����Ϊplayer�����κζ���������壩
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    
    void Update()
    {
        //����������ƶ�
        transform.position = Vector2.MoveTowards(transform.position, MovePos[i].position, Speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, MovePos[i].position) < 0.1f)
        {
            if(WaitTime < 0.0f)
            {
                i ^= 1;                     //С����i��1������iΪ1����ôi���0��������һ��Ҫ�ƶ�����0��λ��
                WaitTime = 1.0f;            //���õȴ�ʱ��
            }
            else
            {
                WaitTime -= Time.deltaTime; //�ȴ�ʱ����֡����λʱ�����
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��player��boxcollider��movingplatform����ʱ
        if(collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            //��movingplatform�ı任��Ϊplayer�任�ĸ���任������player��Ϊmovingplatform���Ӷ���
            //��λ�ȡplayer������ǰ��ײ����Ϸ����任
            collision.gameObject.transform.parent = gameObject.transform;
        }
        //���
        if (collision.CompareTag("Item") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            //��movingplatform�ı任��Ϊplayer�任�ĸ���任������player��Ϊmovingplatform���Ӷ���
            //��λ�ȡplayer������ǰ��ײ����Ϸ����任
            collision.gameObject.transform.parent.parent = gameObject.transform;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            //��player�ĸ���任Ϊ��
            collision.gameObject.transform.parent = PlayerTransform;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    public float speed;                                 //�������ٶ�
    public float damage;                                //�������˺�
    public float rotateSpeed;                           //��������ת�ٶ�
    public float destroyTime;                           //���ٻ�����ʱ��

    private Vector2 initSpeed;                          //�����ڳ�ʼ�ٶ�
    private Rigidbody2D rb;                             //���ٶ��йأ���ȡ�������
    private Transform playerTransform;                  //���ǵ������ڵĻ��գ����٣�����Ҫ�����ڵ�λ�ú���ҵ�λ��
    private Transform sickleTransform;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = gameObject.transform.right * speed;               //��һ����ʼ�ٶ�
        initSpeed = rb.velocity;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sickleTransform = GetComponent<Transform>();
    }

    
    void Update()
    {
        //��һ����������ת�ٶ�
        gameObject.transform.Rotate(0, 0, rotateSpeed);
        //���������˶�������ٶ�Խ��ԽС��С��0ֱ�������ٶ�Խ��Խ��
        rb.velocity = rb.velocity - initSpeed * Time.deltaTime;
        //�������;�У�������λ�ú����λ�úܽ�����ֱ�����ٻ����ڣ����յ�Ч����
        if((Mathf.Abs(sickleTransform.position.x - playerTransform.position.x) < 0.5f) &&
           (Mathf.Abs(sickleTransform.position.y - playerTransform.position.y) < 0.5f))
        {
            Destroy(gameObject);
        }
        //�������;�У������Ϊ��Ծ����һϵ��ԭ��Ӳ��������ڣ������ھ�����ԭ·���أ�ֱ��destroytime������
        else
        {
            Invoke("destroy", destroyTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����������enemy������˺�
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetHurt(damage);
        }
    }

    //���ٻ�����
    void destroy()
    {
        Destroy(gameObject);
    }
}

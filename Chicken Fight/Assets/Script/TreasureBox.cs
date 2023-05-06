using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public float deltaTime;                                 //���俪����Ʒ���ӳ�ʱ�䣨��Ϊ���俪���������ӳ٣�
    public GameObject coin;                                 //Ŀǰֻ�����ɽ��

    private bool canOpen;                                   //�����Ƿ���Դ�
    private bool isOpened;                                  //�����Ƿ��Ѿ��򿪹�
    private Animator animator;              
    

    // Start is called before the first frame update
    void Start()
    {
        canOpen = false;                                    //��ʼʱ���䲻�ɴ�
        isOpened = false;                                   //��ʼʱ����û����
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))                    //�����Ұ���E
        {
            if(canOpen && !isOpened)                        //���ҿ��Դ򿪱����ұ���û�б���
            {
                animator.SetTrigger("Opening");             //����opening����
                isOpened = true;                            //�����Ѿ�����
                Invoke("GenerateCoin", deltaTime);          //deltatime�����ɽ��
            }
        }
    }

    void GenerateCoin()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����߽����䴥������Χ�ڣ��Ϳ��Դ򿪱���
        if(collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
            //Debug.Log("canopen");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //����߳����䴥������Χ�ڣ��Ͳ����Դ򿪱���
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
        }
    }
}

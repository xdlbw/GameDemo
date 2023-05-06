using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public float deltaTime;                                 //宝箱开出物品的延迟时间（因为宝箱开启动画有延迟）
    public GameObject coin;                                 //目前只能生成金币

    private bool canOpen;                                   //宝箱是否可以打开
    private bool isOpened;                                  //宝箱是否已经打开过
    private Animator animator;              
    

    // Start is called before the first frame update
    void Start()
    {
        canOpen = false;                                    //开始时宝箱不可打开
        isOpened = false;                                   //开始时宝箱没被打开
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))                    //如果玩家按了E
        {
            if(canOpen && !isOpened)                        //并且可以打开宝箱且宝箱没有被打开
            {
                animator.SetTrigger("Opening");             //播放opening动画
                isOpened = true;                            //宝箱已经被打开
                Invoke("GenerateCoin", deltaTime);          //deltatime后生成金币
            }
        }
    }

    void GenerateCoin()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //玩家走进宝箱触发器范围内，就可以打开宝箱
        if(collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
            //Debug.Log("canopen");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //玩家走出宝箱触发器范围内，就不可以打开宝箱
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
        }
    }
}

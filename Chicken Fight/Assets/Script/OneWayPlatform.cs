using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private Animator MyAnim;

    void Start()
    {
        MyAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKey(KeyCode.S) && collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<CapsuleCollider2D>().isTrigger = true;
            collision.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        MyAnim.SetBool("Fall", true);
        MyAnim.SetBool("Idle", false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            collision.GetComponent<CapsuleCollider2D>().isTrigger = false;
            collision.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }
}

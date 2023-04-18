using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            SoundManager.PlayCoinPickClip();
            CoinUI.CurrentCoinQuantity += 1;
            //Destroy(gameObject);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponentInChildren<BoxCollider2D>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
    }
}

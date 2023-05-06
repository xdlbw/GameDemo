using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleHit : MonoBehaviour
{
    public GameObject Sickle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //玩家按U就可丢出回旋镖
        //后需要改，不能无限丢
        if (Input.GetKeyDown(KeyCode.U))
        {
            Instantiate(Sickle, transform.position, transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator MCAnim;

    void Start()
    {

    }


    void Update()
    {

    }

    //��ͷ����
    public void Shake()
    {
        MCAnim.SetTrigger("Shake");
    }
}

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
        //��Ұ�U�Ϳɶ���������
        //����Ҫ�ģ��������޶�
        if (Input.GetKeyDown(KeyCode.U))
        {
            Instantiate(Sickle, transform.position, transform.rotation);
        }
    }
}

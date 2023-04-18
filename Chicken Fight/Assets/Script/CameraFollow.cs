using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;                   //����������
    public float SmoothSpeed;                   

    public Vector2 minCameraPos;                //���½�
    public Vector2 maxCameraPos;                //���Ͻ�

    void Start()
    {
        //��ȡ����ǰ����CameraShake��ǩ�����岢��ȡ����CameraShake���
        GameController.MCShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    private void LateUpdate()
    {
        if (target)
        {
            if(gameObject.transform.position != target.transform.position)      //�����ͷλ�ò��������λ��
            {
                Vector3 targetPos = target.transform.position;
                //����targetPos��ֵ��minCameraPos��maxCameraPos֮��
                targetPos.x = Mathf.Clamp(targetPos.x, minCameraPos.x, maxCameraPos.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minCameraPos.y, maxCameraPos.y);
                //���Բ�ֵ������ͷ��λ����SmoothSpeed���ٶȱ仯�����λ��
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, SmoothSpeed);
            }
        }
    }

    public void SetCamPosLimit(Vector2 minPos, Vector2 maxPos)
    {
        minCameraPos = minPos;
        maxCameraPos = maxPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;                   //相机跟随玩家
    public float SmoothSpeed;                   

    public Vector2 minCameraPos;                //左下角
    public Vector2 maxCameraPos;                //右上角

    void Start()
    {
        //获取到当前带有CameraShake标签的物体并获取它的CameraShake组件
        GameController.MCShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    private void LateUpdate()
    {
        if (target)
        {
            if(gameObject.transform.position != target.transform.position)      //如果镜头位置不等于玩家位置
            {
                Vector3 targetPos = target.transform.position;
                //限制targetPos的值在minCameraPos和maxCameraPos之间
                targetPos.x = Mathf.Clamp(targetPos.x, minCameraPos.x, maxCameraPos.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minCameraPos.y, maxCameraPos.y);
                //线性插值，将镜头的位置已SmoothSpeed的速度变化到玩家位置
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

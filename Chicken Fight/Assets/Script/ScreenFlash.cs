using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image img;                       //UI图片（先把透明度设为0）
    public float time;                      //屏闪时间
    public Color FlashColor;                //屏闪颜色

    private Color DefaultColor;             //默认颜色
    void Start()
    {
        DefaultColor = img.color;           //先记录UI图片颜色
    }

    
    void Update()
    {
        
    }

    //屏闪
    public void FlashScreen()
    {
        StartCoroutine(DoFlash());
    }

    //协程
    IEnumerator DoFlash()
    {
        img.color = FlashColor;             //屏闪颜色的透明度不为0
        yield return new WaitForSeconds(time);  //此协程在每一帧等待time后继续执行后面的程序
        img.color = DefaultColor;           //图片颜色恢复
    }
}

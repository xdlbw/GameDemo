using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image img;                       //UIͼƬ���Ȱ�͸������Ϊ0��
    public float time;                      //����ʱ��
    public Color FlashColor;                //������ɫ

    private Color DefaultColor;             //Ĭ����ɫ
    void Start()
    {
        DefaultColor = img.color;           //�ȼ�¼UIͼƬ��ɫ
    }

    
    void Update()
    {
        
    }

    //����
    public void FlashScreen()
    {
        StartCoroutine(DoFlash());
    }

    //Э��
    IEnumerator DoFlash()
    {
        img.color = FlashColor;             //������ɫ��͸���Ȳ�Ϊ0
        yield return new WaitForSeconds(time);  //��Э����ÿһ֡�ȴ�time�����ִ�к���ĳ���
        img.color = DefaultColor;           //ͼƬ��ɫ�ָ�
    }
}

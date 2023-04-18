using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text HealthText;                     //Ѫ��UI�ı�
    public static float HealthCurrent;          //��ǰѪ������̬��������ֱ��ͨ����������
    public static float HealthMax;              //���Ѫ������̬��������ֱ��ͨ����������
        
    private Image healthBar;                    //UIѪ������fllied��ͼƬ
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    
    void Update()
    {
        healthBar.fillAmount = HealthCurrent / HealthMax;   //�����Ϊ��ǰѪ��/���Ѫ��
        HealthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();    //�����ȷ�ı�
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text HealthText;                     //血量UI文本
    public static float HealthCurrent;          //当前血量；静态变量，可直接通过类名调用
    public static float HealthMax;              //最大血量；静态变量，可直接通过类名调用
        
    private Image healthBar;                    //UI血量条，fllied类图片
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    
    void Update()
    {
        healthBar.fillAmount = HealthCurrent / HealthMax;   //填充率为当前血量/最大血量
        HealthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();    //输出正确文本
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public bool isSelect =false;             //关卡是否可选择
    public int starsNum = 0;                 //关卡解锁需要的星星数量
    public GameObject locks;
    public GameObject stars;
    public GameObject panel;
    public GameObject map;
    public Text starsText;
    public int startGride = 1;
    public int endGride = 4;
    private void Start() {
        //PlayerPrefs.DeleteAll();
        if(PlayerPrefs.GetInt("totalStars",0) >= starsNum){   //(获取存储数据)如果存储的星星数量大于所需的
            isSelect = true;
        }
        if(isSelect){
            stars.SetActive(true);                    //星星显示 
            locks.SetActive(false);                   //锁消失

            //每一大关前面的TEXT星星显示
            int count = 0;
            for(int i = startGride ; i<= endGride ; i++){
                count += PlayerPrefs.GetInt("level"+i.ToString(),0);
            }
            starsText.text = count.ToString() + "/12";
        }
    }
    public void canSelected(){
        if(isSelect){                     //如果关卡可选择
            panel.SetActive(true);        //让关卡内小关卡显现
            map.SetActive(false);         //整个关卡界面消失
        }
    }
    public void panelSelect(){
        panel.SetActive(false);        //让关卡内小关卡消失
        map.SetActive(true);         //整个关卡界面显现
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GrideSelect : MonoBehaviour
{
    public bool isSelect = false;
    public Sprite levelbg;
    private Image image;
    public GameObject[] stars;
    private void Awake() {
        image = GetComponent<Image>();
    }
    public void returnMenu(){
        SceneManager.LoadScene(1);
    }
    private void Start() {
        if(transform.parent.GetChild(0).name == gameObject.name){       //显示此关
            isSelect = true;
        }
        else{
            int beforeNum = int.Parse(gameObject.name) -1;      //获取前面一关的关卡数字
            if(PlayerPrefs.GetInt("level" + beforeNum.ToString()) > 0){  //如果前面一个关卡的星星数量大于0
                isSelect = true;
            }
        }
        if(isSelect){
            image.overrideSprite = levelbg;          //可选择的就将图片重写
            transform.Find("Text").gameObject.SetActive(true);  //并显示当前是第几个关卡

            int count = PlayerPrefs.GetInt("level" + gameObject.name);   //获取当前关卡星星的个数
            if(count > 0){
                for(int i=0;i < count;i++){
                    stars[i].SetActive(true);
                }
            }
        }
    }
    public void Selected(){       //当进入此关进行游戏时
        if(isSelect){
            PlayerPrefs.SetString("nowLevel","level" + gameObject.name);     //存储此关的名字：level1，level2....
            SceneManager.LoadScene(2);
        }
    }
}

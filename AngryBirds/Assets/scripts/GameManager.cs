using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Birds> birds;     // 小鸟集合
    public List<pig> pigs;        // 猪集合
    public static GameManager _instance;
    private Vector3 originPos;
    public GameObject lose;
    public GameObject win;
    public GameObject[] stars;
    public int starsNum = 0;
    private int totalLevel = 12;    //一个关卡的小关卡个数
    private void Awake() {
        _instance = this;
        if(birds.Count > 0){      //有第一支小鸟
                originPos = birds[0].transform.position;   //让它的位置作为初始位置
        }
    }
    private void Start() {
        Initialized();            //游戏开始进行一次初始化
    }
    private void Initialized(){         //初始化
        for(int i = 0; i < birds.Count; i++){
            if(i == 0){        //第一只小鸟
                birds[i].transform.position = originPos;  //后面的每只小鸟都是初始小鸟的位置
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
                birds[i].isFlyed = true;
            }
            else{              //其他小鸟组件失活
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].isFlyed = false;
            }
        }
    }

    //赢得游戏的条件是在所有猪死亡的情况下，小鸟的数量大于等于0
    public void is_win(){         //是否赢得游戏
        if(pigs.Count > 0){       //还有猪存活
            if(birds.Count >0){   //还有小鸟
                Initialized();    //初始化
            }
            else{
                lose.SetActive(true);          //出现失败界面
            }
        }
        else{
            SavaData();
            win.SetActive(true);               // 出现成功界面
        }
    }

    public void ShowStars(){
        StartCoroutine("show");
    }

    IEnumerator show(){        //协程，让星星一颗颗显示
        for(; starsNum < birds.Count+1; starsNum++){
            if(starsNum >= stars.Length){
                break;
            }
            stars[starsNum].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void retry(){
        SavaData();
        SceneManager.LoadScene(2);       //重新加载游戏场景
    }
    public void menu(){
        SavaData();
        SceneManager.LoadScene(1);       //加载主菜单场景
    }
    public void SavaData(){      //存储星星，如果有新记录就存储，星星数量低于之前的数量就不存储
        if(starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"))){ //如果新通关的星星数量大于当前的星星数量
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"),starsNum);   //将星星存储到每一关中
        }
        int sum = 0;
        for(int i = 1; i<= totalLevel ;i++){    //所有关卡星星总和存储，显示在map界面且作为开启新关卡依据
            sum += PlayerPrefs.GetInt("level" + i.ToString());          //将星星存储到每一关中
        }
        PlayerPrefs.SetInt("totalStars",sum);       //将数据存储
        //print(PlayerPrefs.GetInt("totalStars"));
    }
}

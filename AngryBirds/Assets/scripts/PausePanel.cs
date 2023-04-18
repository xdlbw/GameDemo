using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PausePanel : MonoBehaviour
{
    //基本思路是对暂停按钮开启关闭的动画演示，当按下暂停按钮时，菜单出现，所有动画暂停，且暂停按钮消失
    //点击继续按钮时，菜单消失，所有动画重新播放，暂停按钮出现
    private Animator anim;
    public GameObject button;
    private void Awake(){
        anim = GetComponent<Animator>();
    }
    public void retry(){
        Time.timeScale = 1;   
        SceneManager.LoadScene(2);       //重新加载游戏场景
    }
    public void pause(){
        button.SetActive(false);                  //一暂停pause button就消失
        anim.SetBool("isPause",true);             //“是”暂停

        //为了解决暂停界面小鸟依然可以操作的问题
        if(GameManager._instance.birds.Count > 0){      //如果还有小鸟
            if(GameManager._instance.birds[0].isReleased == false){    //如果没被释放
                GameManager._instance.birds[0].isFlyed = false;      //不可以操作
            }
        }
    }
    public void menu(){
        Time.timeScale = 1;   
        SceneManager.LoadScene(1);       //加载主菜单场景
    }
    public void resume(){              //继续按钮
        Time.timeScale = 1;            //时间正常
        anim.SetBool("isPause",false);  //“不是”暂停
        if(GameManager._instance.birds.Count > 0){      //如果还有小鸟
            if(GameManager._instance.birds[0].isReleased == false){    //如果没被释放
                GameManager._instance.birds[0].isFlyed = true;      //可以操作
            }
        }
    }

    public void PauseAnimEnd(){        //pause动画播放完成后调用
        Time.timeScale = 0;            //所有动画暂停
    } 
    public void ResumeAnimEnd(){       //resume动画播放完成后调用
        button.SetActive(true);        //完成后出现pause button
    }
}

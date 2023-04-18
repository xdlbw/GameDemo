using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
//表示游戏进行的五种状态：开始、生成水果，水果下落，下落途中，游戏结束，计算成绩
//默认是ready状态，点击开始按钮变为standby生成水果，此时按下鼠标水果跟着移动
//松开鼠标状态变为inprogress，水果总高度大于top线就gameover，最后计算成绩
//其中standby和inprogress两个状态不断循环
public enum GameState{       
    Ready = 0,Standby = 1,InProgress = 2,GameOver = 3,CaculateScore = 4,  
}
public class GameManager : MonoBehaviour
{
    public GameObject[] FruitList;         //水果类型
    public GameObject FruitBornPos;        //出生点
    public GameObject startButton;         //开始按钮
    public float totalScore = 0f;          //当前得分
    public Text TotalScore;
    public Text HighScore;
    public AudioSource combine;
    public AudioSource hit;
    public Vector3 combineScale = new Vector3(0,0,0);
    public static GameManager gameManagerInstance;
    public GameState gameState = GameState.Ready;
    private void Awake() {   //游戏启用前调用
        float highestScore = PlayerPrefs.GetFloat("HighestScore");
        HighScore.text = "历史最高分：" + highestScore;
        gameManagerInstance = this;
    }
    public void StartGame(){


        gameState = GameState.Standby;
        CreateFruit();
        startButton.SetActive(false);     //点击开始隐藏按钮
    }  
    public void InvokeCreateFruit(float Invoketime){
        Invoke("CreateFruit",Invoketime);
    }
    public void CreateFruit(){
        int index = Random.Range(0,4);     //随机生成0-3号水果
        if(FruitList.Length >= index && FruitList[index] != null){
            GameObject fruitObj = FruitList[index];   //生成对象
            var currentFruit = Instantiate(fruitObj,FruitBornPos.transform.position,FruitBornPos.transform.rotation);  //在预定的水果出生点位置
            currentFruit.GetComponent<fruits>().fruitState = FruitState.Standby;   //创建新的水果使得状态变为standby
        }
    }
    //两个同号水果碰撞合成大水果
    public void CombineNewFruit(FruitType CurrentFruitType,Vector3 currentPos,Vector3 collisionPos){
        Vector3 centerPos = (currentPos + collisionPos) / 2;    //当前水果和碰撞水果的中心位置
        int index = (int)CurrentFruitType + 1;                  //大一号水果
        GameObject combineFruitObj = FruitList[index];
        var combineFruit = Instantiate(combineFruitObj,centerPos,combineFruitObj.transform.rotation);  //在此位置生成克隆体
        combineFruit.GetComponent<Rigidbody2D>().gravityScale = 1.0f;  //有重力
        combineFruit.GetComponent<fruits>().fruitState = FruitState.Collision;  //克隆体状态为collision
        combineFruit.transform.localScale = combineScale;      //实现合成水果尺寸有小变大的过程
        combine.Play();
    }
}

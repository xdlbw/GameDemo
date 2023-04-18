using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//游戏结束，会判定当前场景内的水果并进行对应分数的相加
//实现方式是水果触碰到红线，游戏结束，红线下降，碰到对应的水果就加上相应的分数并销毁
public class topLIne : MonoBehaviour
{ 
    public bool isDown = false;           //红线是否可移动           
    public float speed = 0.1f;
    public float limit_y = -4.87f;
    private void Update() {      //游戏结束，红线的移动
        if(isDown){
            if(this.transform.position.y > limit_y){
                this.transform.Translate(Vector3.down * speed);
            }
            else{
                Invoke("ReLoad",4f);      //重新加载场景
            }
        }
    } 
    //碰撞触发
    private void OnTriggerEnter2D(Collider2D collider) {     
        if((int)GameManager.gameManagerInstance.gameState < (int)GameState.GameOver){
            if(collider.gameObject.tag.Contains("fruit")){
                //如果水果是在碰撞状态触碰到的红线，游戏就结束了
                if(collider.gameObject.GetComponent<fruits>().fruitState == FruitState.Collision){
                    GameManager.gameManagerInstance.gameState = GameState.GameOver;
                    Invoke("Change",1.0f);
                }
            }
        }
        //最后分数的计算
        if(GameManager.gameManagerInstance.gameState == GameState.CaculateScore){
            float currentScore = collider.GetComponent<fruits>().fruitScore;
            GameManager.gameManagerInstance.totalScore += currentScore;
            GameManager.gameManagerInstance.TotalScore.text = "当前得分：" + GameManager.gameManagerInstance.totalScore.ToString();
            Destroy(collider.gameObject);
        }
    }
    private void Change(){    //游戏结束红线可移动，游戏状态变成结算分数
        isDown = true;
        GameManager.gameManagerInstance.gameState = GameState.CaculateScore;
    }
    private void ReLoad(){   //游戏结束重新加载场景
        //最高分的存储
        float highestScore =  PlayerPrefs.GetFloat("HighestScore");
        if(highestScore < GameManager.gameManagerInstance.totalScore){
            PlayerPrefs.SetFloat("HighestScore",GameManager.gameManagerInstance.totalScore);
        }
        SceneManager.LoadScene("HCLOL");
    }
}

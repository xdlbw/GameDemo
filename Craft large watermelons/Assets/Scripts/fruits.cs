using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType{     //英雄类型，便于后面英雄合成
    one = 0,two = 1,three = 2,four = 3,five = 4,six = 5,seven = 6,eight = 7,nine = 8,ten = 9,eleven = 10,
}

//表示水果的四种状态：开始，水果在出生点，下落过程，碰撞
//默认ready，水果生成在出生点为standvy，鼠标松开变成dropping，碰撞到其他水果或者地板墙面变成collision
public enum FruitState{   
    Ready = 0,Standby = 1,Dropping = 2,Collision = 3,
}
public class fruits : MonoBehaviour
{
    public FruitType fruitType = FruitType.one;
    public FruitState fruitState = FruitState.Ready;
    private bool isMove = false;
    public float limit_x = 3.0f;
    public float fruitScore = 10.0f;
    public Vector3 originalScale = Vector3.zero;
    public float Scalespeed = 0.5f;
    private void Awake() {
        originalScale = new Vector3(0.5f,0.5f,0.5f);
    }
    //处于出生点的水果位置移动，设置出生点的水果重力为0，当鼠标按下水果跟随鼠标移动，松开设置重力为1，自己下落
    void Update() {  //鼠标按下
        if(GameManager.gameManagerInstance.gameState == GameState.Standby && fruitState == FruitState.Standby){   //只有游戏在生成水果状态并且水果在出生点时才允许下落
            if(Input.GetMouseButtonDown(0)){
            isMove = true;   
            }
            if(Input.GetMouseButtonUp(0) && isMove){ //鼠标抬起
                isMove = false;
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;  //重力变成1从而下落
                fruitState = FruitState.Dropping;   //水果变成下落
                GameManager.gameManagerInstance.gameState = GameState.InProgress;   //游戏变成进行中
                GameManager.gameManagerInstance.InvokeCreateFruit(1.0f);
            }
            if(isMove){   //移动状态则让他跟着鼠标移动，只移动X轴
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //全屏坐标转化为摄像机坐标
                this.gameObject.GetComponent<Transform>().position = new Vector3(mousePos.x,this.gameObject.GetComponent<Transform>().position.y,this.gameObject.GetComponent<Transform>().position.z);
            }
        } 
        //对水果x轴方位进行限制
        if(this.transform.position.x > limit_x){
            this.transform.position = new Vector3(limit_x,this.transform.position.y,this.transform.position.z);
        }
        if(this.transform.position.x < -limit_x){
            this.transform.position = new Vector3(-limit_x,this.transform.position.y,this.transform.position.z);
        }

        //尺寸恢复
        if(this.transform.localScale.x < originalScale.x){
            this.transform.localScale += new Vector3(1,1,1)*Scalespeed;
        }
        if(this.transform.localScale.x >= originalScale.x){
            this.transform.localScale = originalScale;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(fruitState == FruitState.Dropping){
            if(collision.gameObject.tag.Contains("floor")){  //碰撞到floor
                GameManager.gameManagerInstance.gameState = GameState.Standby;
                fruitState = FruitState.Collision;
                GameManager.gameManagerInstance.hit.Play();
            }
            if(collision.gameObject.tag.Contains("fruits")){ //碰撞到fruits
                GameManager.gameManagerInstance.gameState = GameState.Standby;
                fruitState = FruitState.Collision;
            }
        }

        //水果在下落或者碰撞过程中
        if(fruitState == FruitState.Dropping || fruitState == FruitState.Collision){
            if(collision.gameObject.tag.Contains("fruits")){ //如果碰撞到水果且是同号
                if(fruitType == collision.gameObject.GetComponent<fruits>().fruitType && fruitType != FruitType.eleven){
                    //因为当前水果和碰撞水果都会调用方法，故此处采用判定两者位置来使只有其中一个调用CombineNewFruit方法
                    float currentFruitPos = this.transform.position.x + this.transform.position.y;
                    float collisionFruitPos = collision.transform.position.x + collision.transform.position.y;
                    if(currentFruitPos > collisionFruitPos){ //生成大一号水果并把之前的两个水果销毁
                        GameManager.gameManagerInstance.CombineNewFruit(fruitType,this.transform.position,collision.transform.position);
                        GameManager.gameManagerInstance.totalScore += fruitScore;
                        //分数更新
                        GameManager.gameManagerInstance.TotalScore.text = "当前得分：" + GameManager.gameManagerInstance.totalScore.ToString();
                        Destroy(this.gameObject);
                        Destroy(collision.gameObject);
                    }
                }
            }
        }
    } 
}

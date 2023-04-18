using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
   public float maxSpeed = 10f;
   public float minSpeed = 5f;
   private SpriteRenderer render;
   public Sprite hurt;
   public GameObject boom; 
   public GameObject score;           //获取得分组件
   public bool isPig = false;         //区分是猪还是其他东西（木块、石头）
   public AudioClip CollisionClip;
   public AudioClip dead;
   public AudioClip BirdCollision;
   private void Awake() {
       render = GetComponent<SpriteRenderer>();
   }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            AudioPlay(BirdCollision);//播放小鸟碰撞音效
            collision.transform.GetComponent<Birds>().Hurt();
        }

        if(collision.relativeVelocity.magnitude > maxSpeed){   //直接死亡
            Dead();
        }
        else if((collision.relativeVelocity.magnitude <= maxSpeed) && (collision.relativeVelocity.magnitude >= minSpeed)){   //受伤
            render.sprite = hurt;
            AudioPlay(CollisionClip);         //播放受伤音效
        }
    }

   public void Dead(){
       if(isPig){
           GameManager._instance.pigs.Remove(this);
       }
       Destroy(gameObject);        //销毁
       Instantiate(boom,transform.position,Quaternion.identity);  //显示爆炸产生的蘑菇云
       GameObject go = Instantiate(score,transform.position + new Vector3(0,0.75f,0),Quaternion.identity); //显示得分
       Destroy(go,1.5f);           //1.5s后分数消失
       AudioPlay(dead);            //播放死亡音效
   }
    public void AudioPlay(AudioClip clip){    //音乐播放
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }
}

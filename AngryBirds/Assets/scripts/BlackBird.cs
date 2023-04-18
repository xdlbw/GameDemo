using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Birds
{
    public List<pig> blocks = new List<pig>();
    private void OnTriggerEnter2D(Collider2D collision) {        //进入触发区域
        if(collision.gameObject.tag == "Enemy"){
            blocks.Add(collision.gameObject.GetComponent<pig>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {         //离开触发区域
        if(collision.gameObject.tag == "Enemy"){
            blocks.Remove(collision.gameObject.GetComponent<pig>());
        }
    }
    public override void showSkill()
    {
        base.showSkill();
        if(blocks.Count > 0 && blocks != null){
            for(int i = 0; i < blocks.Count; i++){
                blocks[i].Dead();
            }
        }
        clear();
    }
    void clear(){
        rg.velocity = Vector3.zero;
        Instantiate(balckboom,transform.position,Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.ClearTrails();
    }
    protected override void next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);        //发射出去的小鸟的消失
        GameManager._instance.is_win();              //判断是否胜利
    }
}

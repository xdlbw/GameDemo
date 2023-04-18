using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Birds : MonoBehaviour
{
    private bool isClick = false;     
    public float maxDis = 1.3f;       //最大距离
    [HideInInspector]
    public SpringJoint2D sp;          //获取弹簧关节组件
    protected Rigidbody2D rg;           //获取刚体组件
    protected TestMyTrail myTrail;       //获取拖尾
    public LineRenderer right;         //获取划线组件
    public Transform rightPos;      
    public LineRenderer left;
    public Transform leftPos;  
    public GameObject boom;           //获取爆炸动画
    public GameObject balckboom; 
    [HideInInspector]
    public bool isFlyed = false;         //判断是否飞过了，不然出现bug，小鸟未消失之前可以一直拖拽
    private bool isFlying = false;      //鸟是否正在飞，作为发动技能的条件
    public bool isReleased = false;      //鸟是否被释放
    public float smooth = 3.0f;      
    public AudioClip select;
    public AudioClip flyClip;
    protected SpriteRenderer render;
    public Sprite hurt;
    public Sprite skill_change;
    private void Awake() {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown() {      //鼠标按下
        if(isFlyed){
            AudioPlay(select);        //小鸟被选择的音效
            isClick = true;
            rg.isKinematic = true;        //受物理控制
        }
    }
    private void OnMouseUp() {        //鼠标抬起
        if(isFlyed){
            AudioPlay(flyClip);
            isClick = false;
            rg.isKinematic = false;       //不受物理控制，保证速度合理
            Invoke("fly",0.1f);           //fly函数延时用以计算发射速度
            right.enabled = false;        //去掉弹弓划线
            left.enabled = false;
            isFlyed = false;                //表示飞过了
        }
    }
    private void Update() {           //鼠标按下，进行位置的跟随
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        if(isClick){
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0,0,-Camera.main.transform.position.z);  //确定位置
            if(Vector3.Distance(transform.position,rightPos.position) > maxDis){       //保证最大距离
                Vector3 pos = (transform.position - rightPos.position).normalized;
                pos *= maxDis;
                transform.position = pos + rightPos.position; 
            }
            Line();
        }
        //镜头跟随,首先继续小鸟的位置
        //lerp函数使得物体到达另外一个目标物体之间进行平滑过渡运动效果
        //三个参数分别是：首先是相机的位置，再是改动后相机的位置（水平距离设限），最后是平滑效果
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,new Vector3(Mathf.Clamp(posX,-3,15),Camera.main.transform.position.y,Camera.main.transform.position.z),smooth*Time.deltaTime);

        if(isFlying){   //如果正在飞行且按下鼠标左键
            if(Input.GetMouseButtonDown(0)){
                showSkill();
            }
        }
    }
    void fly(){
        isReleased = true;
        isFlying = true;
        sp.enabled = false;           //摆脱弹簧节点
        Invoke("next",4f);            //飞出后4s调用next方法
        myTrail.StartTrails();
    }

    void Line(){                      //弹弓划线
        right.enabled = true;        
        left.enabled = true;
        right.SetPosition(0,rightPos.position);
        right.SetPosition(1,transform.position);
        left.SetPosition(0,leftPos.position);
        left.SetPosition(1,transform.position);
    }

    protected virtual void next(){                //下一只小鸟的飞出
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);        //发射出去的小鸟的消失
        Instantiate(boom,transform.position,Quaternion.identity);  //消失特效
        GameManager._instance.is_win();              //判断是否胜利
        myTrail.ClearTrails();
    }
    private void OnCollisionEnter2D(Collision2D collision){
        isFlying = false;
        myTrail.ClearTrails();
    }
    public void AudioPlay(AudioClip clip){    //音乐播放
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }

    public virtual void showSkill(){         //虚方法，用以每个鸟的飞行发动技能继承
        isFlying = false;
        render.sprite = skill_change;
    }
    public void Hurt(){
        render.sprite = hurt;
    }
}



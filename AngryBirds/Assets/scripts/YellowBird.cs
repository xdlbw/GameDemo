using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Birds
{
    public override void showSkill(){
        base.showSkill();
        rg.velocity *= 2;       //速度加倍
    }
}

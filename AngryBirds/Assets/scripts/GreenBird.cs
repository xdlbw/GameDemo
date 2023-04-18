using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Birds
{
    public override void showSkill()
    {
        base.showSkill();
        Vector3 speed = rg.velocity;
        speed.x *= -1;         //反向
        rg.velocity = speed;
    }
}

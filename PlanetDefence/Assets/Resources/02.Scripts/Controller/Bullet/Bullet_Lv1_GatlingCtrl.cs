using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Lv1_GatlingCtrl : BulletCtrl
{
    private void OnEnable()
    {
        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

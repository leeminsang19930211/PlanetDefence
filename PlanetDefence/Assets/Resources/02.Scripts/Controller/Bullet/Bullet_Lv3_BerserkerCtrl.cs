using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Lv3_BerserkerCtrl : BulletCtrl
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

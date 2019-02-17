using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Lv3_BerserkerCtrl : BulletCtrl
{
    private void OnEnable()
    {
        BulletType = Bullet.Lv3_Berserker;

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

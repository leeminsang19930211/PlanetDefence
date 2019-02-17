using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Lv1_FastCtrl : BulletCtrl
{
    private void OnEnable()
    {
        BulletType = Bullet.Lv1_Fast;

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

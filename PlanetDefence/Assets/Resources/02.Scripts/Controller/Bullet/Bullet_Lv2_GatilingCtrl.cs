using UnityEngine;
using System.Collections;

public class Bullet_Lv2_GatilingCtrl : BulletCtrl
{
    private void OnEnable()
    {
        BulletType = Bullet.Lv2_Gatling;
        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

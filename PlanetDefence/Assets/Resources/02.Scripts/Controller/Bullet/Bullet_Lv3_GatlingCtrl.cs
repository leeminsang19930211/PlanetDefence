using UnityEngine;
using System.Collections;

public class Bullet_Lv3_GatlingCtrl : BulletCtrl
{
    private void OnEnable()
    {
        BulletType = Bullet.Lv3_Gatling;

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

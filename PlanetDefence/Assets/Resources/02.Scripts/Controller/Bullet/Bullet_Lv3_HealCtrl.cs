using UnityEngine;
using System.Collections;

public class Bullet_Lv3_HealCtrl : BulletCtrl
{
    private void OnEnable()
    {
        BulletType = Bullet.Lv3_Heal;

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

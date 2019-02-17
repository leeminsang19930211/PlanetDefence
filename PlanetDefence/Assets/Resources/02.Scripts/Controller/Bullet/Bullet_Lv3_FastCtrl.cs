using UnityEngine;
using System.Collections;

public class Bullet_Lv3_FastCtrl : BulletCtrl
{
    private void OnEnable()
    {
        BulletType = Bullet.Lv3_Fast;

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

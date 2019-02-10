using UnityEngine;
using System.Collections;

public class Bullet_Lv1_MissileCtrl : BulletCtrl
{
    void OnEnable()
    {
        base.Init();
    }

    void Update()
    {
        MoveToTarget();
    }

    protected override void _OnTarget()
    {
        HitTarget();

        gameObject.SetActive(false);
    }
}

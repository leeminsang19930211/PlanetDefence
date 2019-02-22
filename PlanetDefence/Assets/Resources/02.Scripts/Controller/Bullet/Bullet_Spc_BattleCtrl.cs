using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Spc_BattleCtrl : BulletCtrl
{
    private void OnEnable()
    {
        BulletType = Bullet.Spc_Battle;

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Spc_BattleCtrl : BulletCtrl
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

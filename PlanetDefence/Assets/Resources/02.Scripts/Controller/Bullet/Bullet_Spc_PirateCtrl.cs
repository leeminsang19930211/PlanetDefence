﻿using UnityEngine;
using System.Collections;

public class Bullet_Spc_PirateCtrl : BulletCtrl
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

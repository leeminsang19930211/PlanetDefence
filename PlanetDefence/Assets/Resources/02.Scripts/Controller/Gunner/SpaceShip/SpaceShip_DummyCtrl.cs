﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_DummyCtrl : SpaceShipCtrl
{
    void Start()
    {
        m_bulletType = Bullet.End;
        m_bulletPool = BulletPool.End;
        m_effectType = Effect.End;
        m_effectPool = EffectPool.End;

        SpaceShipType = MobType.DummyShip;

        base.Init();
    }

    void Update()
    {
        MoveBody();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_NormalCtrl : SpaceShipCtrl
{
    void Start()
    {
        m_bulletType = Bullet.Spc_Normal;
        m_bulletPool = BulletPool.SpaceShip;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.SpaceShip;

        base.Init();
    }

    void Update()
    {
        MoveBody();
    }
}

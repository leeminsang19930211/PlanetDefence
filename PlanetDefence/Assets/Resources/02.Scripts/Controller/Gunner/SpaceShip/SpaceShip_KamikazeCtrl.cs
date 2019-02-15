using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_KamikazeCtrl: SpaceShipCtrl
{
    void Start()
    {
        m_bulletType = Bullet.End;
        m_bulletPool = BulletPool.End;
        m_effectType = Effect.End;
        m_effectPool = EffectPool.End;

        base.Init();
    }

    void Update()
    {
        MoveBody();
    }
}

using UnityEngine;
using System.Collections;

public class SpaceShip_LittleCtrl : SpaceShipCtrl
{
    void Start()
    {
        m_bulletType = Bullet.Spc_Little;
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

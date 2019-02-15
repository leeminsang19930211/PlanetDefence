using UnityEngine;
using System.Collections;

public class SpaceShip_PirateCtrl : SpaceShipCtrl
{
    void Start()
    {
        m_bulletType = Bullet.Spc_Pirate;
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

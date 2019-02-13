using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Lv1_GatlingCtrl : TurretCtrl
{
    private void Start()
    {
        m_bulletType = Bullet.Lv1_Gatling;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv1_Gatiling;

        base.Init();
    }
   
    void Update()
    {

    }
}

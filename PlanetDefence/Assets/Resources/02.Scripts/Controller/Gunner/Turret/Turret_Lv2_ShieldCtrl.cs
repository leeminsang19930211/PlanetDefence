using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Lv2_ShieldCtrl : TurretCtrl
{
    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.End;
        m_bulletPool = BulletPool.End;
        m_effectType = Effect.End;
        m_effectPool = EffectPool.End;

        m_turretType = Turret.Lv2_Shield;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

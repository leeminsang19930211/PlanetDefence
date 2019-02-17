using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Lv2_SlowCtrl : TurretCtrl
{
    public override ITurretData TurretData
    {
        get
        {
            TurretData_Slow turretData = new TurretData_Slow();

            turretData.MaxHP = m_maxHP;

            return turretData;
        }
        set
        {
            m_maxHP = value.MaxHP;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv2_Slow;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv2_Slow;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

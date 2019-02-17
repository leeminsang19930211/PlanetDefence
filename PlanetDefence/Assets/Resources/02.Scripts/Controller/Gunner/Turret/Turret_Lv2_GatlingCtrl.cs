using UnityEngine;
using System.Collections;

public class Turret_Lv2_GatlingCtrl : TurretCtrl
{
    public override ITurretData TurretData
    {
        get
        {
            TurretData_Gatling turretData = new TurretData_Gatling();

            turretData.MaxHP = m_maxHP;

            return turretData;
        }
        set
        {
            TurretData_Gatling turretData = (TurretData_Gatling)value;

            m_maxHP = turretData.MaxHP;
        }
    }

    private void Start()
    {
        m_bulletType = Bullet.Lv2_Gatling;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv2_Gatling;

        base.Init();
    }

    void Update()
    {

    }
}

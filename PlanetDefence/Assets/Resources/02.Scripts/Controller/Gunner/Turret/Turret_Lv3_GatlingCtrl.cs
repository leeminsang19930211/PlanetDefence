using UnityEngine;
using System.Collections;

public class Turret_Lv3_GatlingCtrl : TurretCtrl
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
        m_bulletType = Bullet.Lv3_Gatling;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv3_Gatling;

        base.Init();
    }

    void Update()
    {

    }
}

using UnityEngine;
using System.Collections;

public class Turret_Lv3_FastCtrl : TurretCtrl
{
    public override ITurretData TurretData
    {
        get
        {
            TurretData_Fast turretData = new TurretData_Fast();

            turretData.MaxHP = m_maxHP;

            return turretData;
        }
        set
        {
            TurretData_Fast turretData = (TurretData_Fast)value;

            m_maxHP = turretData.MaxHP;
        }
    }

    private void Start()
    {
        m_bulletType = Bullet.Lv3_Fast;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv3_Fast;

        base.Init();
    }

    void Update()
    {

    }
}

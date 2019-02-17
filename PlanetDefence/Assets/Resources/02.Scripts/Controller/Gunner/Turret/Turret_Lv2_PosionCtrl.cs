using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Lv2_PosionCtrl : TurretCtrl
{
    public override ITurretData TurretData
    {
        get
        {
            TurretData_Poison turretData = new TurretData_Poison();

            turretData.MaxHP = m_maxHP;

            return turretData;
        }
        set
        {
            TurretData_Poison turretData = (TurretData_Poison)value;

            m_maxHP = turretData.MaxHP;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv2_Poison;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv2_Poison;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

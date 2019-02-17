using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Lv1_MissileCtrl : TurretCtrl
{
    public override ITurretData TurretData
    {
        get
        {
            TurretData_Missile turretData = new TurretData_Missile();

            turretData.MaxHP = m_maxHP;

            return turretData;
        }
        set
        {
            TurretData_Missile turretData = (TurretData_Missile)value;

            m_maxHP = turretData.MaxHP;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv1_Missile;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv1_Missile;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

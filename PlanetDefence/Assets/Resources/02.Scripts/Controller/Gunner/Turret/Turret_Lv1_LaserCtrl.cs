using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Lv1_LaserCtrl : TurretCtrl
{
    public override ITurretData TurretData
    {
        get
        {
            TurretData_Laser turretData = new TurretData_Laser();

            turretData.MaxHP = m_maxHP;

            return turretData;
        }
        set
        {
            TurretData_Laser turretData = (TurretData_Laser)value;

            m_maxHP = turretData.MaxHP;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv1_Laser;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv1_Laser;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

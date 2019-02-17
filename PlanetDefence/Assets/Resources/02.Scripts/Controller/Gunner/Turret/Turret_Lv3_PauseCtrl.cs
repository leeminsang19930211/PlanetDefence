﻿using UnityEngine;
using System.Collections;

public class Turret_Lv3_PauseCtrl : TurretCtrl
{
    public override ITurretData TurretData
    {
        get
        {
            TurretData_Pause turretData = new TurretData_Pause();

            turretData.MaxHP = m_maxHP;

            return turretData;
        }
        set
        {
            TurretData_Pause turretData = (TurretData_Pause)value;

            m_maxHP = turretData.MaxHP;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.Lv3_Pause;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv3_Pause;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}


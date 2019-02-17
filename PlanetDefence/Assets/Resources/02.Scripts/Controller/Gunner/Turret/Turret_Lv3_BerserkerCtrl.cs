using UnityEngine;
using System.Collections;

public class Turret_Lv3_BerserkerCtrl : TurretCtrl
{
    public float m_fireScale_beserker = 1f;
    public float m_beserkerHPRate = 0f;

    public override ITurretData TurretData
    {
        get
        {
            TurretData_Berserker turretData = new TurretData_Berserker();

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
        m_bulletType = Bullet.Lv3_Berserker;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv3_Berserker;

        base.Init();
    }

    private void Update()
    {
        if(CurHP  < m_maxHP * m_beserkerHPRate)
        {
            m_fireDelay = m_fireDelay_ori * (1 /m_fireScale_beserker);
        }
    }
}

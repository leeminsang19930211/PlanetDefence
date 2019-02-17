using UnityEngine;
using System.Collections;

public class Turret_Lv3_SlowCtrl : TurretCtrl
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
        m_bulletType = Bullet.Lv3_Slow;
        m_bulletPool = BulletPool.Turret;
        m_effectType = Effect.Explosion_Bullet0;
        m_effectPool = EffectPool.Turret;

        m_turretType = Turret.Lv3_Slow;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}


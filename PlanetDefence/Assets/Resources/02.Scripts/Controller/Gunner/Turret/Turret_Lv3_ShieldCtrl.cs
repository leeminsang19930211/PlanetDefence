using UnityEngine;
using System.Collections;

public class Turret_Lv3_ShieldCtrl : TurretCtrl
{
    private float m_hitDamageScale = 1f;

    public override ITurretData TurretData
    {
        get
        {
            TurretData_Shield turretData = new TurretData_Shield();

            turretData.MaxHP = m_maxHP;
            turretData.hitDamageScale = m_hitDamageScale;

            return turretData;
        }
        set
        {
            TurretData_Shield turretData = (TurretData_Shield)value;

            m_maxHP = turretData.MaxHP;
            m_hitDamageScale = turretData.hitDamageScale;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.End;
        m_bulletPool = BulletPool.End;
        m_effectType = Effect.End;
        m_effectPool = EffectPool.End;

        m_turretType = Turret.Lv3_Shield;


        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
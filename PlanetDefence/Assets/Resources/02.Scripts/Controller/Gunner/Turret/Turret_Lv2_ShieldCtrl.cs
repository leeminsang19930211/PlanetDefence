using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Lv2_ShieldCtrl : TurretCtrl
{
    private float m_hitDamageScale = 1f;


    public override TurretData TurretData
    {
        get
        {
            TurretData_Shield turretData = new TurretData_Shield();

            turretData.maxHP = m_maxHP;
            turretData.hitDamageScale = m_hitDamageScale;

            return turretData;
        }

        set
        {
            TurretData_Shield turretData = (TurretData_Shield)value;

            m_maxHP = turretData.maxHP;
            m_hitDamageScale = turretData.hitDamageScale;
        }
    }

    public override void Hit(int damage)
    {
        m_curHP -= (int)(damage * m_hitDamageScale);

        _OnHit();

        if (m_curHP < 0)
        {
            m_curHP = 0;
            _OnZeroHP();
        }

        m_unitHPBarCtrl.UpdateHP(m_curHP, m_maxHP);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_bulletType = Bullet.End;
        m_bulletPool = BulletPool.End;

        m_turretType = Turret.Lv2_Shield;

        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Lv3_BerserkerCtrl : BulletCtrl
{
    public override IBulletData BulletData
    {
        get
        {
            BulletData_Berserker bulletData = new BulletData_Berserker();

            bulletData.Damage = m_damage;

            return bulletData;
        }

        set
        {
            BulletData_Berserker bulletData = (BulletData_Berserker)value;

            m_damage = bulletData.Damage;
        }
    }

    private void OnEnable()
    {
        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

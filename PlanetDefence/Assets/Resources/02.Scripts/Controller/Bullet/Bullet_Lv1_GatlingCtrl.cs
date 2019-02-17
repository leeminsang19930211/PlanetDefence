using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Lv1_GatlingCtrl : BulletCtrl
{
    public override IBulletData BulletData
    {
        get
        {
            BulletData_Gatling bulletData = new BulletData_Gatling();

            bulletData.Damage = m_damage;

            return bulletData;
        }

        set
        {
            BulletData_Gatling bulletData = (BulletData_Gatling)value;

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

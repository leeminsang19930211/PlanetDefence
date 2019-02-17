using UnityEngine;
using System.Collections;

public class Bullet_Lv2_FastCtrl : BulletCtrl
{
    public override IBulletData BulletData
    {
        get
        {
            BulletData_Fast bulletData = new BulletData_Fast();

            bulletData.Damage = m_damage;

            return bulletData;
        }

        set
        {
            BulletData_Fast bulletData = (BulletData_Fast)value;

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

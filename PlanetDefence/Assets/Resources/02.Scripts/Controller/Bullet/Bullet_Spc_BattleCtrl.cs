using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Spc_BattleCtrl : BulletCtrl
{
    public override IBulletData BulletData
    {
        get
        {
            BulletData_SpaceShip bulletData = new BulletData_SpaceShip();

            m_damage = bulletData.Damage;

            return bulletData;
        }

        set
        {
            BulletData_SpaceShip bulletData = (BulletData_SpaceShip)value;

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

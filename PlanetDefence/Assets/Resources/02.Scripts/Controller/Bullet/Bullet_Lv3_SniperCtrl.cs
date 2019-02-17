using UnityEngine;
using System.Collections;

public class Bullet_Lv3_SniperCtrl : BulletCtrl
{
    public override IBulletData BulletData
    {
        get
        {
            BulletData_Sniper bulletData = new BulletData_Sniper();

            m_damage = bulletData.Damage;

            return bulletData;
        }

        set
        {
            BulletData_Sniper bulletData = (BulletData_Sniper)value;

            m_damage = bulletData.Damage;
        }
    }

    protected override void _OnTarget(Gunner target, Vector3 hitPos)
    {
        PlayEffect(hitPos);
        target.HitToDie();
        gameObject.SetActive(false);
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

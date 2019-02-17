using UnityEngine;
using System.Collections;

public class Bullet_Lv2_Pause : BulletCtrl
{
    public float m_probability = 0f;
    public float m_duration = 0f;

    public override IBulletData BulletData
    {
        get
        {
            BulletData_Pause bulletData = new BulletData_Pause();

            bulletData.Damage = m_damage;
            bulletData.duration = m_duration;

            return bulletData;
        }

        set
        {
            BulletData_Pause bulletData = (BulletData_Pause)value;

            m_damage = bulletData.Damage;
            m_duration = bulletData.duration;
        }
    }

    protected override void _OnTarget(Gunner target, Vector3 hitPos)
    {
        SpaceShipCtrl spaceShip = target as SpaceShipCtrl;

        if (spaceShip != null)
        {
            float rand = Random.Range(0, 1);

            if (rand <= m_probability)
            {
                target.StopFire(m_duration);
            }

        }

        base._OnTarget(target, hitPos);
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

using UnityEngine;
using System.Collections;

public class Bullet_Lv3_SlowCtrl : BulletCtrl
{
    public float m_probability = 0f;
    public Gunner.SlowMoveInfo m_slowInfo;

    public override BulletData BulletData
    {
        get
        {
            BulletData_Slow bulletData = new BulletData_Slow();

            m_damage = bulletData.damage;
            m_slowInfo.duration = bulletData.duration;

            return bulletData;
        }

        set
        {
            BulletData_Slow bulletData = (BulletData_Slow)value;

            m_damage = bulletData.damage;
            m_slowInfo.duration = bulletData.duration;
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
                target.SlowMove(m_slowInfo);
            }

        }

        base._OnTarget(target, hitPos);
    }

    private void OnEnable()
    {
        BulletType = Bullet.Lv3_Slow;

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

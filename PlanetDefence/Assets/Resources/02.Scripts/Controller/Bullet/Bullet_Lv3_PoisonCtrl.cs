using UnityEngine;
using System.Collections;

public class Bullet_Lv3_PoisonCtrl : BulletCtrl
{
    public float m_probability = 0f;
    public Gunner.DotInfo m_dotInfo;

    public override BulletData BulletData
    {
        get
        {
            BulletData_Poison bulletData = new BulletData_Poison();

            bulletData.damage = m_damage;
            bulletData.dotDamage = m_dotInfo.damage;

            return bulletData;
        }

        set
        {
            BulletData_Poison bulletData = (BulletData_Poison)value;

            m_damage = bulletData.damage;
            m_dotInfo.damage = bulletData.dotDamage;
        }
    }


    protected override void _OnTarget(Gunner target, Vector3 hitPos)
    {
        float rand = Random.Range(0, 1);

        if (rand <= m_probability)
        {
            target.Hit_Dot(m_dotInfo);
        }

        base._OnTarget(target, hitPos);
    }

    private void OnEnable()
    {
        BulletType = Bullet.Lv3_Poison;
        m_effect_explosion = Effect.Poison0;
        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}


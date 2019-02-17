using UnityEngine;
using System.Collections;

public class Bullet_Lv3_MissileCtrl : BulletCtrl
{
    public int m_splashDamage = 0;
    public float m_splashRadius = 0;

    public override IBulletData BulletData
    {
        get
        {
            BulletData_Missile bulletData = new BulletData_Missile();

            bulletData.Damage = m_damage;
            bulletData.splashRange = m_splashRadius;

            return bulletData;
        }

        set
        {
            BulletData_Missile bulletData = (BulletData_Missile)value;

            m_damage = bulletData.Damage;
            m_splashRadius = bulletData.splashRange;
        }
    }

    protected override void _OnTarget(Gunner target, Vector3 hitPos)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(m_trsf.position, m_splashRadius, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "SPACESHIP_NORMAL")
            {
                SpaceShipCtrl ctrl = hit.collider.gameObject.GetComponent<SpaceShipCtrl>();

                ctrl.Hit(m_splashDamage);

                PlayEffect(ctrl.Position);
            }
            else if (hit.collider.tag == "SPACESHIP_DUMMY")
            {
                SpaceShipCtrl ctrl = hit.collider.gameObject.GetComponent<SpaceShipCtrl>();

                ctrl.Hit(m_splashDamage);

                PlayEffect(ctrl.Position);
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



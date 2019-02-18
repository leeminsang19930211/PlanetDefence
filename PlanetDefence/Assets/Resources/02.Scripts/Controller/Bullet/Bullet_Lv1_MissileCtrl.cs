using UnityEngine;
using System.Collections;

public class Bullet_Lv1_MissileCtrl : BulletCtrl
{
    public int m_splashDamage = 0;
    public float m_splashRadius = 0;


    public override BulletData BulletData
    {
        get
        {
            BulletData_Missile bulletData = new BulletData_Missile();

            bulletData.damage = m_damage;
            bulletData.splashRange = m_splashRadius;

            return bulletData;
        }

        set
        {
            BulletData_Missile bulletData = (BulletData_Missile)value;

            m_damage = bulletData.damage;
            m_splashRadius = bulletData.splashRange;
        }
    }


    protected override void _OnTarget(Gunner target, Vector3 hitPos)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(m_trsf.position, m_splashRadius, Vector2.zero);

        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.tag == "SPACESHIP_NORMAL")
            {
                SpaceShipCtrl ctrl = hit.collider.gameObject.GetComponent<SpaceShipCtrl>();

                ctrl.Hit(m_splashDamage);

                PlayEffect(m_effect_explosion ,ctrl.Position);
            }
            else if(hit.collider.tag == "SPACESHIP_DUMMY")
            {
                SpaceShipCtrl ctrl = hit.collider.gameObject.GetComponent<SpaceShipCtrl>();

                ctrl.Hit(m_splashDamage);

                PlayEffect(m_effect_explosion, ctrl.Position);
            }           
        }

        base._OnTarget(target, hitPos);
    }

    private void OnEnable()
    {
        BulletType = Bullet.Lv1_Missile;
        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

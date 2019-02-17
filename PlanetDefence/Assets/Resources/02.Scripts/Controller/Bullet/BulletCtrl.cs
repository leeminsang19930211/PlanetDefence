using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour
{
    public int m_damage = 0;
    public float m_speed = 0;

    protected Transform m_trsf = null;

    private Gunner m_shooter = null;
    private Gunner m_target= null;
 
    public bool Clone { get; set; } = false;

    public Bullet BulletType { get; set; } = Bullet.End;

    public virtual BulletData BulletData
    {
        get
        {
            BulletData bulletData = new BulletData();

            bulletData.damage = m_damage;

            return bulletData;
        }

        set
        {
            m_damage = value.damage;
        }
    }

    public void UpdateBulletData()
    {
        BulletData = Player.Inst.GetBulletData(BulletType);
    }

    public void Fire(Vector3 startPos, Vector3 startAngle, Gunner shooter, Gunner target)
    {
        if (m_trsf == null)
            m_trsf = GetComponent<Transform>();

        m_trsf.position = startPos;
        m_trsf.localEulerAngles = startAngle;
        m_target = target;
        m_shooter = shooter;
    }

    protected void MoveToTarget()
    {
        if (m_target == null)
        {
            gameObject.SetActive(false);
            return;
        }
          
        Vector3 moveDir = m_target.Position - m_trsf.position;

        float moveDist = m_speed * Time.deltaTime;

        if(moveDist > moveDir.magnitude)
        {
            moveDist = moveDir.magnitude;
            _OnTarget(m_target, m_trsf.position);
        }

        m_trsf.position += moveDir.normalized * moveDist;     
    }

    public void RotateToTarget()
    {
        if (m_target == null)
            return;

        Vector3 moveDir = m_target.Position - m_trsf.position;

        float angle = MyMath.LeftAngle180(m_trsf.up, moveDir);

        m_trsf.Rotate(0, 0, angle);
    }

    protected void RayCastTargets()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(m_trsf.position.x, m_trsf.position.y), new Vector3(m_trsf.up.x, m_trsf.up.y));

        if (hits != null)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.tag == "SPACESHIP_NORMAL")
                {
                    Gunner target = hit.collider.gameObject.GetComponent<SpaceShipCtrl>();

                    _OnTargetByRayCast(target, target.Position);
                }
                else if (hit.collider.gameObject.tag == "SPACESHIP_DUMMY")
                {
                    Gunner target = hit.collider.gameObject.GetComponent<SpaceShipCtrl>();

                    _OnTargetByRayCast(target, target.Position);
                }
            }
        }
    }

    protected void PlayEffect(Vector3 pos )
    {
        if(m_shooter == null)
        {
            return;
        }

        EffectMgr.Inst.PlayEffect(m_shooter.EffectPool, m_shooter.BulletPoolIdx, pos);
    }

    protected virtual void _OnTargetByRayCast(Gunner target, Vector3 hitPos)
    {
        PlayEffect(hitPos);
        target.Hit(m_damage);
    }

    protected virtual void _OnTarget(Gunner target, Vector3 hitPos)
    {
        PlayEffect(hitPos);
        target.Hit(m_damage);
        gameObject.SetActive(false);
    }

    protected virtual void _OnShield(Gunner target, Vector3 hitPos)
    {
        PlayEffect(hitPos);
        target.Hit(m_damage);
        gameObject.SetActive(false);
    }

    protected void Init()
    {
        m_trsf = GetComponent<Transform>();

        if(Clone)
        {
            UpdateBulletData();
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SHIELD")
        {
            if (m_target is TurretCtrl)
            {
                Gunner shield = TurretMgr.Inst.FindShieldTurret(m_target.BulletPoolIdx); // TurretCtrl 의 BulletPoolIdx 는 터렛 지지대의 인덱스이다.

                if (shield)
                {
                    m_target = shield;
                    _OnShield(m_target, m_trsf.position);
                }
            }

        }
    }
}

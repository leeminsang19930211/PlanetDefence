using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour
{
    public int m_damage = 0;
    public float m_speed = 0;

    private Transform m_trsf = null;
    private Gunner m_target= null;

    public void Fire(Vector3 startPos, Vector3 startAngle, Gunner target)
    {
        m_trsf.position = startPos;
        m_trsf.localEulerAngles = startAngle;
        m_target = target;
    }

    protected void HitTarget()
    {
        m_target.Hit(m_damage);        
    }

    protected void HitRayCastedTarget()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(m_trsf.position.x, m_trsf.position.y), new Vector3(m_trsf.up.x, m_trsf.up.y));

        if (hits != null)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.tag == "SPACESHIP_NORMAL")
                {
                    hit.collider.gameObject.GetComponent<SpaceShipCtrl>().Hit(m_damage);
                }
                else if (hit.collider.gameObject.tag == "SPACESHIP_DUMMY")
                {
                    hit.collider.gameObject.GetComponent<SpaceShipCtrl>().Hit(m_damage);
                }
            }
        }
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
            _OnTarget();
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

    protected virtual void _OnTarget()
    {

    }

    protected virtual void _OnShield()
    {
        if (m_target is TurretCtrl)
        {
            Gunner shield = TurretMgr.Inst.FindShieldTurret(m_target.BulletPoolIdx); // TurretCtrl 의 BulletPoolIdx 는 터렛 지지대의 인덱스이다.

            if(shield )
            {
                shield.Hit(m_damage);
                gameObject.SetActive(false);
            }
        }
    }

    protected void Init()
    {
        m_trsf = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SHIELD")
        {
            _OnShield();

        }
    }
}

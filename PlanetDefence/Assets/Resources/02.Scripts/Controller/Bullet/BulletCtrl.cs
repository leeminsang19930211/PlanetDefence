using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour
{
    public int m_damage = 0;
    public float m_speed = 0;

    private Transform m_trsf = null;
    private  SpaceShipCtrl m_target= null;

    public void Fire(Vector3 startPos, Vector3 startAngle, SpaceShipCtrl target)
    {
        m_trsf.position = startPos;
        m_trsf.localEulerAngles = startAngle;
        m_target = target;
    }

    protected void MoveToTarget()
    {
        if (m_target == null)
            return;

        Vector3 moveDir = m_target.Pos - m_trsf.position;

        float moveDist = m_speed * Time.deltaTime;

        if(moveDist > moveDir.magnitude)
        {
            moveDist = moveDir.magnitude;
            AlmostOnTarget();
        }

        m_trsf.position += moveDir.normalized * moveDist;     
    }

    public void RotateToTarget()
    {
        if (m_target == null)
            return;

        Vector3 moveDir = m_target.Pos - m_trsf.position;

        float angle = MyMath.LeftAngle180(m_trsf.up, moveDir);

        m_trsf.Rotate(0, 0, angle);
    }

    protected virtual void AlmostOnTarget()
    {

    }

    protected void Init()
    {
        m_trsf = GetComponent<Transform>();
    }
}

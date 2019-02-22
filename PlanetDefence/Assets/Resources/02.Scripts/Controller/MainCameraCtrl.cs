using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraCtrl : MonoBehaviour
{
    public float m_rotSpeed = 0f;

    private Vector3 m_satrtDir = Vector3.up;
    private Vector3 m_curDir = Vector3.up;
    private Vector3 m_endDir = Vector3.up;
    private Transform m_transform = null;

    void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    void Update()
    {
        float leftAngle = MyMath.LeftAngle180(m_curDir, m_endDir);
        float speed = m_rotSpeed * Time.deltaTime;

        // To left
        if (leftAngle > 0)
        {
            if (speed > leftAngle)
                speed = leftAngle;
        }
        // To right
        else if (leftAngle < 0)
        {
            speed *= -1f;

            if (speed < leftAngle)
                speed = leftAngle;
        }
        else
        {
            m_satrtDir = m_curDir;
            speed = 0;
        }
           
        m_transform.RotateAround(new Vector3(0, -1600f, 0), Vector3.forward, speed);

        m_curDir = m_transform.up;

        BattleGameObjectMgr.Inst.RotateMiniPlanetSpotToTarget(m_curDir);
    }

    public void RotateToRight()
    {
        if(m_satrtDir == Vector3.up)
        {
            m_endDir = Vector3.right;
        }
        else if(m_satrtDir == Vector3.right)
        {
            m_endDir = Vector3.down;
        }
        else if(m_satrtDir == Vector3.down)
        {
            m_endDir = Vector3.left;
        }
        else
        {
            m_endDir = Vector3.up;
        }
    }

    public void RotateToLeft()
    {
        if (m_satrtDir == Vector3.up)
        {
            m_endDir = Vector3.left;
        }
        else if (m_satrtDir == Vector3.right)
        {
            m_endDir = Vector3.up;
        }
        else if (m_satrtDir == Vector3.down)
        {
            m_endDir = Vector3.right;
        }
        else
        {
            m_endDir = Vector3.down;
        }
    }

    public void _OnNewBattle()
    {
        m_satrtDir = Vector3.up;
        m_curDir = Vector3.up;
        m_endDir = Vector3.up;

        if (m_transform == null)
            m_transform = GetComponent<Transform>();

        float leftAngle = MyMath.LeftAngle180(m_transform.up, m_endDir);

        m_transform.RotateAround(new Vector3(0, -1600f, 0), Vector3.forward, leftAngle);    
    }
}


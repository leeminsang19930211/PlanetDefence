using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    public float m_distFromTurretSupport = 0;

    protected void Init()
    {
        Transform trsf = GetComponent<Transform>();

        trsf.position += trsf.up.normalized * m_distFromTurretSupport;
    }

}

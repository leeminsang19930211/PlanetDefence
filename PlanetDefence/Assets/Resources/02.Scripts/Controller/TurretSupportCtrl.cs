using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSupportCtrl : MonoBehaviour
{
    public Vector3 m_DeltaFromSetUpPos = Vector3.zero;

    private Transform m_trasf = null;
    private GameObject m_turretInst = null;
    private TurretCtrl m_turretCtrl = null;

    public int Idx { get; set; } = -1;
    public bool Focus { get; set; } = false;
    public GameObject TurretInst
    {
        get
        {
            return m_turretInst;
        }
        set
        {
            m_turretInst = value;

            if(m_turretInst)
            {
                m_turretCtrl = m_turretInst.GetComponent<TurretCtrl>();
            }
        }
    }
    public TurretCtrl TurretCtrl
    {
        get
        {
            return m_turretCtrl;
        }         
    }
    
    public Vector3 SetUpPos
    {
        get
        { 
            if(m_trasf)
            {
                return m_trasf.position;
            }

            return Vector3.zero;
        }
    }

    public void Awake()
    {
        m_trasf = GetComponent<Transform>();
    }

    public void OnClick()
    { 
        TurretMgr.Inst.FocusTurretSupportByIdx(Idx);
    }
}

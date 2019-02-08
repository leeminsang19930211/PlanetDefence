using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSupportCtrl : MonoBehaviour
{
    private Transform m_trasf = null;
    private TurretCtrl m_turretCtrl = null;

    public int Idx { get; set; } = -1;
    public bool Focus { get; set; } = false;

    public TurretCtrl TurretCtrl
    {
        get
        {
            return m_turretCtrl;
        }      
        
        set
        {
            m_turretCtrl = value;
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

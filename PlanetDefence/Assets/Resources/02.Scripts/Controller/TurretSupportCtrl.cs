using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSupportCtrl : MonoBehaviour
{
    public Vector3 m_DeltaFromSetUpPos = Vector3.zero;

    private Transform m_trasf = null;

    public int Idx { get; set; } = -1;
    public bool Focus { get; set; } = false;
    public GameObject TurretInst { get; set; } = null;

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

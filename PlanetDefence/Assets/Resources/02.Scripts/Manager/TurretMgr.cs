﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMgr : MonoBehaviour
{
    private static TurretMgr m_inst = null;

    public static TurretMgr Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "TurretMgr";
                m_inst = container.AddComponent<TurretMgr>() as TurretMgr;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }

    private int m_focusedTurretSupportIdx = -1; // 클릭하여 현재 포커싱 된 터렛 지지대의 인덱스이다

    private List<TurretSupportCtrl> m_turretSupportCtrs = new List<TurretSupportCtrl>();
    private Dictionary<string, GameObject> m_turrets = new Dictionary<string, GameObject>();

    public void Init()
    {
        AddTurret("NormalTurret");

        GameObject[] turretSupports = GameObject.FindGameObjectsWithTag("TURRET_SUPPORT");

        for(int i=0; i< turretSupports.Length; ++i)
        {
            TurretSupportCtrl ctrl = turretSupports[i].GetComponent<TurretSupportCtrl>();

            if (ctrl == null)
            {
                Debug.LogError("Finding turretSupportsCtrl failed");
                continue;
            }

            ctrl.Idx = i;

            m_turretSupportCtrs.Add(ctrl);
        }
    }

    public bool CreateTurretOnTurretSupport(string turretName)
    {
        if(m_focusedTurretSupportIdx < 0 || m_focusedTurretSupportIdx >= m_turretSupportCtrs.Count)
        {
            Debug.Log("the focus idx is out of the range");
            return false;
        }
        
        if(m_turretSupportCtrs[m_focusedTurretSupportIdx].Focus == false)
        {
            Debug.Log("the turret support is not focused");
            return false;
        }

        GameObject turret = null;
            
        if(false ==  m_turrets.TryGetValue(turretName, out turret))
        {
            Debug.Log("Finding turret in the Dictionary failed");
            return false;
        }

        Instantiate<GameObject>(turret, m_turretSupportCtrs[m_focusedTurretSupportIdx].SetUpPos, Quaternion.Euler(0,0,0));

        return false;
    }

    // 터렛 지지대가 눌릴때 호출해야 하는 함수이다. 호출해서 자신이 포커싱 된것을 알려줘야한다.
    public bool FocusTurretSupportByIdx(int idx)
    {
        if (idx < 0 || idx >= m_turretSupportCtrs.Count)
        {
            Debug.Log("the focus idx is out of the range");
            return false;
        }

        foreach(TurretSupportCtrl ctrl in m_turretSupportCtrs)
        {
            ctrl.Focus = false;
        }

        m_turretSupportCtrs[idx].Focus = true;

        m_focusedTurretSupportIdx = idx;

        return true;
    }

    private bool AddTurret(string name)
    {
        GameObject turret = null;

        turret = GameObject.Find(name);
        if (turret == null)
        {
            Debug.LogError("Finding "+ name + " failed");
            return false;
        }
           
        m_turrets.Add(name, turret);
        return true;
    }
}
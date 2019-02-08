using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Lv1_MissileCtrl : TurretCtrl
{
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void CreateBullet() 
    {
        SpaceShipCtrl ctrl =  SpaceShipMgr.Inst.FindFirstTargetInFan(m_minAngleForTargeting, m_maxAngleForTargeting);


    }
}

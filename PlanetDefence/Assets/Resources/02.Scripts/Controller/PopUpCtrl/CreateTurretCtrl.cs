using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTurretCtrl : MonoBehaviour
{
    public void OnClick()
    {
        TurretMgr.Inst.CreateTurretOnTurretSupport("NormalTurret");
    }
}

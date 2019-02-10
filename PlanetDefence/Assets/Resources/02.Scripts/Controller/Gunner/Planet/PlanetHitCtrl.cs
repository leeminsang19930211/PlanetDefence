using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHitCtrl : Gunner
{
    public PlanetCtrl m_planetCtrl = null;

    public override void Hit(int damage)
    {
        m_planetCtrl.Hit(damage);
    }

    private void Start()
    {
        base.Init();
    }
}

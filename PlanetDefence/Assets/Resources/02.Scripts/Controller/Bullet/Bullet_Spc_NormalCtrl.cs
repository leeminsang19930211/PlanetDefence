using UnityEngine;
using System.Collections;

public class Bullet_Spc_NormalCtrl : BulletCtrl
{


    private void OnEnable()
    {
        BulletType = Bullet.Spc_Normal;


        if( m_damage > 0)
        {

        }

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

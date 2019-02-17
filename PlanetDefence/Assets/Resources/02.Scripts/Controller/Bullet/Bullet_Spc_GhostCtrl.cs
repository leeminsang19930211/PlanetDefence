using UnityEngine;
using System.Collections;

public class Bullet_Spc_GhostCtrl : BulletCtrl
{
 
    private void OnEnable()
    {
        BulletType = Bullet.Spc_Ghost;


        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

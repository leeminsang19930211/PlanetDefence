using UnityEngine;
using System.Collections;

public class Bullet_Spc_PirateCtrl : BulletCtrl
{
    private void OnEnable()
    {
        BulletType = Bullet.Spc_Pirate;

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

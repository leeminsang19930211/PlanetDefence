using UnityEngine;
using System.Collections;

public class Bullet_Spc_LittleCtrl : BulletCtrl
{
  
    private void OnEnable()
    {
        BulletType = Bullet.Spc_Little;


        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

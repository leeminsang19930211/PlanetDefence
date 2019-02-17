using UnityEngine;
using System.Collections;

public class Bullet_Spc_ZombieCtrl: BulletCtrl
{
    private void OnEnable()
    {
        BulletType = Bullet.Spc_Zombie;

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

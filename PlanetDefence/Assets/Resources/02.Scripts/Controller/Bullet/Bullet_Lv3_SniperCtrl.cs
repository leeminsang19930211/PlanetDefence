using UnityEngine;
using System.Collections;

public class Bullet_Lv3_SniperCtrl : BulletCtrl
{
 
    //protected override void _OnTarget(Gunner target, Vector3 hitPos)
    //{
    //    PlayEffect(m_effect_explosion, hitPos);
    //    target.HitToDie();
    //    gameObject.SetActive(false);
    //}

    private void OnEnable()
    {
        BulletType = Bullet.Lv3_Sniper;

        m_effect_explosion = Effect.Explosion1;



        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

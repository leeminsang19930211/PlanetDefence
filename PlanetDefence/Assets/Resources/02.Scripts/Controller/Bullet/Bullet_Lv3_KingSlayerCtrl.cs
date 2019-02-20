using UnityEngine;
using System.Collections;

public class Bullet_Lv3_KingSlayerCtrl : BulletCtrl
{ 
    public int m_damageScaleToKing = 4;

    protected override void _OnTarget(Gunner target, Vector3 hitPos)
    {
        SpaceShipCtrl _target = target as SpaceShipCtrl;

        if(_target != null)
        {
            if(_target.SpaceShipType == MobType.ZombieShip || _target.SpaceShipType == MobType.BattleShip || _target.SpaceShipType == MobType.GhostShip)
            {
                PlayEffect(m_effect_explosion, hitPos);
                target.Hit(m_damage * m_damageScaleToKing);
                gameObject.SetActive(false);
            }
            else
            {
                base._OnTarget(target, hitPos);
            }         
        }
        else
        {
            base._OnTarget(target, hitPos);
        }
    }

    private void OnEnable()
    {
        BulletType = Bullet.Lv1_Fast;

        base.Init();
    }

    private void Update()
    {
        MoveToTarget();
    }
}

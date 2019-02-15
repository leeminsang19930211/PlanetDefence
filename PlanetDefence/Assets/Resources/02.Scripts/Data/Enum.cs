public enum Turret
{
    Lv1_Missile,
    Lv1_Laser,
    Lv1_Gatiling,
    Lv2_Poison, 
    Lv2_Shield,
    Lv2_Slow,
    Lv2_Pause,
    Lv3_Sniper,
    Lv3_Heal,
    Lv3_Berserker,
    dummy03,
    dummy04,
    dummy05,
    dummy06,
    dummy07,
    dummy08,
    dummy09,
    dummy10,
    dummy11,
    dummy12,
    dummy13,
    dummy14,
    dummy15,
    dummy16,
    End,
}

public enum Lab
{
    // Inc : Increasing
    IncTurretSupports,
    IncTurretHelth,
    IncTurretDamage,
    dummy07,
    dummy08,
    dummy09,
    dummy10,
    dummy11,
    dummy12,
    dummy13,
    dummy14,
    dummy15,
    dummy16,
    dummy17,
    dummy18,
    dummy19,
    End,
}

public enum SpaceShipPart
{
    _0,
    _1,
    _2,
    _3,
    _4,
    End,
}

public enum BuyErr
{
   NoError,
   NoBP,                 // 도면 (BluePrint) 없음
   AlreadySetUp,         // 터렛이 이미 설치되있는 경우
   NotEnoughRsrc,        // 자원이 모자른 경우
   End,
}

public enum LabErr
{
    NoError,
    NoBP,
    Max,                 
    NotEnoughRsrc,
    End,
}

public enum RepairErr
{
    NoError,
    NoBP,
    AlreadyRepaired,
    NotEnoughRsrc,
    End,
}

public enum Bullet
{
    Lv1_Missile,
    Lv1_Laser,
    Lv1_Gatling,
    Lv2_Posion,
    Lv2_Slow,
    Lv2_Pause,
    Lv3_Sniper,
    Lv3_Berserker,
    Spc_Normal,
    Spc_Pirate,
    Spc_Little,
    Spc_Zombie, 
    Spc_Ghost,
    Spc_Battle,
    End,
}

public enum Effect
{
    Explosion_Bullet0,
    End,
}

public enum BulletPool
{
    Turret,
    SpaceShip,
    End,
}

public enum EffectPool
{
    Turret,
    SpaceShip,
    End,
}

public enum PlanetArea
{
    Up,
    Left,
    Down,
    Right,
    End,
}

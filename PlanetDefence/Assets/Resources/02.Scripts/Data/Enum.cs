public enum Turret
{
    Lv1_Missile,
    Lv1_Laser, 
    Lv2_Shield,
    // 추가2
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
    dummy17,
    dummy18,
    dummy19,
    dummy20,
    dummy21,
    dummy22,
    dummy23,
    End,
}

public enum Lab
{
    // Inc : Increasing
    IncTurretSupports,
    IncTurretHelth,
    IncTurretDamage,
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

public enum Bullet
{
    Lv1_Missile,
    Lv1_Laser,
    TestBullet,
    End,
}

public enum BulletPool
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

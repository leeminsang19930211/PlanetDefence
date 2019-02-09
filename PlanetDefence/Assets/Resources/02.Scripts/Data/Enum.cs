public enum Turret
{
    Lv1_Missile,
    Lv1_Laser,
    Lv2_Shield,
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
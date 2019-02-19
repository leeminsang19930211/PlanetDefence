public enum Turret
{
    Lv1_Gatling,
    Lv1_Fast,
    Lv1_Missile,
    Lv1_Laser,
    Lv2_Gatling,
    Lv2_Fast,
    Lv2_Missile,
    Lv2_Laser,
    Lv2_Shield,
    Lv2_Poison, 
    Lv2_Slow,
    Lv2_Pause,
    Lv3_Gatling,
    Lv3_Fast,
    Lv3_Laser,
    Lv3_Missile,
    Lv3_Shield,
    Lv3_Poison,
    Lv3_Slow,
    Lv3_Pause,
    Lv3_Sniper,
    Lv3_Berserker,
    Lv3_Heal, 
    dummy14, // 킹슬레어 자리. 아직 안만듬
    End,
}

public enum Lab
{
    /* 전체 */
    IncTurretSupports, // 포탑 개수 증가 
    IncTurretHelth, // 포탑 체력 증가
    IncPlanetHelth, // 행성 체력 증가 
    DecSpaceShipHelth, // 적 체력 감소 
    IncJunkDrops, // 잡동사니  획득량 증가 
    IncCoinDrops, // 코인 획득량 증가 
    DecJunkConsumtion, // 잡동사니 소모량 감소
    ReturnTurretRsrc, // 포탑 철거시 자원 반환

    /* 개별 */
    IncGatlingDamage, 
    IncFastFireSpeed, 
    IncSplashRange,   
    IncLaserDuration, 
    DecShieldHitDamage,  // 쉴드 포탑 피격 데미지 감소                            
    IncPoisonDotDamage,  // 독 포탑 도트 데미지 증가
    IncSlowDuration,
    IncPauseDuration,
    IncSniperFireSpeed,
    IncHealAmount,        // 힐량 증가 
    incBerserkerDamage,   // 분노포탑 공격력 증가
    IncKingSlayerFireSpeed,
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
   DoubleShield,         // 쉴드 터렛이 두개이상인 경우
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
    Lv1_Fast,
    Lv2_Gatling,
    Lv2_Fast,
    Lv2_Missile,
    Lv2_Laser,
    Lv2_Poison,
    Lv2_Slow,
    Lv2_Pause,
    Lv3_Gatling,
    Lv3_Fast,
    Lv3_Poison,
    Lv3_Missile,
    Lv3_Laser,
    Lv3_Sniper,
    Lv3_Berserker,
    Lv3_Slow,
    Lv3_Heal,
    Lv3_Pause,
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
    Explosion0,
    Explosion1,

    ShieldHit0,
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


enum TurretType
{
    Gatling,
    Fast,
    Missile,
    Laser,
    Shield,
    Poison,
    Slow,
    Pause,
    Sniper,
    Heal,
    Berserker,
    End,
}
public struct TurretInfo
{
    public bool _lock;
}

public struct LabInfo
{
    public bool _lock;
    public int stacks;  // 얼마만큼 누적됬는가에 대한 수치 
}

public struct SpaceShipPartInfo
{
    public bool _lock;

    // 추가
    public bool _repaired;
}

public struct GlobalData
{
    public int ori_turretSupoorts;
    public int ori_planetHP;
}

public struct SpaceShipData
{
    public int maxHP;
    public int junkDrops;
    public int coinDrops;
}

public interface IBulletData
{
    int Damage { get; set; }
}

public struct BulletData_Gatling : IBulletData
{
    public int Damage { get; set; }
}

public struct BulletData_Fast : IBulletData
{ 
    public int Damage { get; set; }
}

public struct BulletData_Missile : IBulletData
{
    public int Damage { get; set; }

    public float splashRange;
}

public struct BulletData_Laser : IBulletData
{
    public int Damage { get; set; }

    public float duration;
}

public struct BulletData_Poison : IBulletData
{
    public int Damage { get; set; }

    public int dotDamage;
}

public struct BulletData_Slow: IBulletData
{
    public int Damage { get; set; }

    public float duration;
}

public struct BulletData_Pause : IBulletData
{
    public int Damage { get; set; }

    public float duration;
}

public struct BulletData_Sniper : IBulletData
{
    public int Damage { get; set; }
}

public struct BulletData_Berserker : IBulletData
{
    public int Damage { get; set; }
}

public struct BulletData_SpaceShip : IBulletData
{
    public int Damage { get; set; }
}

public interface ITurretData
{
    int MaxHP { get; set; }
}

public struct TurretData_Gatling : ITurretData
{
    public int MaxHP { get; set; }
}

public struct TurretData_Fast : ITurretData
{
    public int MaxHP { get; set; }
}

public struct TurretData_Missile : ITurretData
{
    public int MaxHP { get; set; }
}

public struct TurretData_Laser : ITurretData
{
    public int MaxHP { get; set; }
}

public struct TurretData_Shield : ITurretData
{
    public int MaxHP { get; set; }

    public float hitDamageScale;
}

public struct TurretData_Poison : ITurretData
{
    public int MaxHP { get; set; }
}

public struct TurretData_Slow : ITurretData
{
    public int MaxHP { get; set; }
}

public struct TurretData_Pause : ITurretData
{
    public int MaxHP { get; set; }
}

public struct TurretData_Sniper : ITurretData
{
    public int MaxHP { get; set; }
}

public struct TurretData_Heal : ITurretData
{
    public int MaxHP { get; set; }

    public float healAmount;
}

public struct TurretData_Berserker : ITurretData
{
    public int MaxHP { get; set; }
}





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
    public int turretSupoorts;
    public int planetHP;
}

public class SpaceShipData
{
    public int maxHP;
   public int junkDrops;
    public int coinDrops;
}


public class BulletData
{
    public int damage;
}

public class BulletData_Missile : BulletData
{
    public float splashRange;
}

public class BulletData_Laser : BulletData
{
    public float duration;
}

public class BulletData_Poison : BulletData
{
    public int dotDamage;
}

public class BulletData_Slow : BulletData
{
    public float duration;
}

public class BulletData_Pause : BulletData
{
    public float duration;
}

public class TurretData
{
    public int maxHP;
}

public class TurretData_Heal : TurretData
{
    public bool fire;
}

public class TurretData_Sniper :TurretData
{
    public float fireDelay;
}

public class TurretData_Fast : TurretData
{
    public float fireDelay;
}

public class TurretData_Shield : TurretData
{
    public float hitDamageScale;
}




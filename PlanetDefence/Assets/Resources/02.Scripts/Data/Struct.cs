using System;

[Serializable]
public struct TurretInfo
{
    public bool _lock;
}

[Serializable]
public struct LabInfo
{
    public bool _lock;
    public int stacks;  // 얼마만큼 누적됬는가에 대한 수치 
}

[Serializable]
public struct SpaceShipPartInfo
{
    public bool _lock;

    // 추가
    public bool _repaired;
}

[Serializable]
public struct GlobalData
{
    public int planetHP;
}

[Serializable]
public class SpaceShipData
{
    public int maxHP;
   public int junkDrops;
    public int coinDrops;
}

[Serializable]
public class BulletData
{
    public int damage;
}

[Serializable]
public class BulletData_Missile : BulletData
{
    public float splashRange;
}

[Serializable]
public class BulletData_Laser : BulletData
{
    public float duration;
}

[Serializable]
public class BulletData_Poison : BulletData
{
    public int dotDamage;
}

[Serializable]
public class BulletData_Slow : BulletData
{
    public float duration;
}

[Serializable]
public class BulletData_Pause : BulletData
{
    public float duration;
}

[Serializable]
public class TurretData
{
    public int maxHP;
}

[Serializable]
public class TurretData_Heal : TurretData
{
    public bool fire;
}

[Serializable]
public class TurretData_Sniper :TurretData
{
    public float fireDelay;
}

[Serializable]
public class TurretData_Fast : TurretData
{
    public float fireDelay;
}

[Serializable]
public class TurretData_Shield : TurretData
{
    public float hitDamageScale;
}

[Serializable]
public class TurretData_KingSlayer : TurretData
{
    public float fireDelay;
}


[Serializable]
public struct TurretBlueprintDropInfo
{
    public float probability;
    public Turret turret;
}


[Serializable]
public struct SpaceShipBlueprintDropInfo
{
    public float probability;
    public SpaceShipPart spaceShip;
}



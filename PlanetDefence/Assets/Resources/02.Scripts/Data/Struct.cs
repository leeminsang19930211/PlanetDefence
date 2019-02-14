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
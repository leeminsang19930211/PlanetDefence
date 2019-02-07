using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private static Player m_inst = null;
    private int m_junk = 0;
    private int m_eleCircuit = 0;
    private int m_coin = 0;

    public static Player Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "Player";
                m_inst = container.AddComponent<Player>() as Player;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }

    /* 정보를 반환하는 함수들. 해금 여부, 비용등을 알수가 있다. */
    public TurretInfo FindTurretInfo(Turret turret)
    {
        return new TurretInfo();
    }

    public LabInfo FindLabInfo(Lab lab)
    {
        return new LabInfo();
    }

    public SpaceShipPartInfo FindSpaceShipPartInfo(SpaceShipPart spcPart)
    {
        return new SpaceShipPartInfo();
    }

    // 터렛을 구매하고 생성한다. 포탑이 설치되어있는지 부터 체크한다.
    public BuyErr BuyTurret(Turret turret)
    {
        return new BuyErr();
    }

    // 터렛을 판매하고 삭제한다. 삭제할 터렛이 없을경우 false 리턴한다.
    public bool SellTurret()
    {
        return false;
    }

}
